using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplikasi_Jadwal_ELEKTRO.Models.Genetic;
using Aplikasi_Jadwal_ELEKTRO.Models;
using System.Threading;

namespace Aplikasi_Jadwal_ELEKTRO.Repositories.Genetic
{
    public class PopulationManager
    {
        private readonly List<Matkul> matkuls;
        private readonly List<Waktu> waktus;
        private readonly List<Gene> allPossibleGenes;
        private readonly Random random = new Random();
        private readonly int maxSKSPatut = 40;

        public PopulationManager(List<Gene> allPossibleGenes, List<Matkul> matkuls, List<Waktu> waktus)
        {
            this.allPossibleGenes = allPossibleGenes;
            this.matkuls = matkuls;
            this.waktus = waktus;
        }

        // ================= Helpers =================

        private static string KKey(int id_kelas, int id_waktu) => $"{id_kelas}#{id_waktu}";
        private static string DKey(int kode_dosen, int id_waktu) => $"{kode_dosen}#{id_waktu}";
        private static string RKey(int kode_ruangan, int id_waktu) => $"{kode_ruangan}#{id_waktu}";

        private sealed class Usage
        {
            public HashSet<string> KelasTime = new HashSet<string>();     // (id_kelas,id_waktu)
            public HashSet<string> DosenTime = new HashSet<string>();     // (kode_dosen,id_waktu)
            public HashSet<string> RoomTime = new HashSet<string>();     // (kode_ruangan,id_waktu)
            public Dictionary<(int id_kelas, int kode_matkul, int kode_dosen), int> FixedRoom
                = new Dictionary<(int, int, int), int>();
        }

        private static Usage BuildUsage(IEnumerable<Gene> genes)
        {
            var u = new Usage();
            foreach (var g in genes)
            {
                u.KelasTime.Add(KKey(g.id_kelas, g.id_waktu));
                u.DosenTime.Add(DKey(g.kode_dosen, g.id_waktu));
                u.RoomTime.Add(RKey(g.kode_ruangan, g.id_waktu));
                var triplet = (g.id_kelas, g.kode_matkul, g.kode_dosen);
                if (!u.FixedRoom.ContainsKey(triplet))
                    u.FixedRoom[triplet] = g.kode_ruangan;
            }
            return u;
        }

        private static bool IsFeasible(Gene g, Usage u)
        {
            if (u.KelasTime.Contains(KKey(g.id_kelas, g.id_waktu))) return false;
            if (u.DosenTime.Contains(DKey(g.kode_dosen, g.id_waktu))) return false;
            if (u.RoomTime.Contains(RKey(g.kode_ruangan, g.id_waktu))) return false;
            return true;
        }

        // Pilih block sesi untuk (kelas,matkul,dosen) yang feasible + konsisten ruangan
        private IEnumerable<Gene> PickFeasibleBlock(
            (int id_kelas, int kode_matkul, int kode_dosen) key,
            int requiredSks,
            Usage usage,
            int? preferStart = null)
        {
            // semua kandidat triplet ini
            var pool = allPossibleGenes
                .Where(g => g.id_kelas == key.id_kelas
                         && g.kode_matkul == key.kode_matkul
                         && g.kode_dosen == key.kode_dosen)
                .OrderBy(g => g.id_waktu)
                .ToList();

            // ruangan tetap
            if (!usage.FixedRoom.TryGetValue(key, out int fixedRoom))
            {
                fixedRoom = pool.GroupBy(p => p.kode_ruangan)
                                .OrderByDescending(g => g.Count())
                                .Select(g => g.Key)
                                .FirstOrDefault();
                usage.FixedRoom[key] = fixedRoom;
            }
            pool = pool.Where(p => p.kode_ruangan == fixedRoom).ToList();

            // dekatkan ke anchor kalau ada
            if (preferStart.HasValue)
                pool = pool.OrderBy(p => Math.Abs(p.id_waktu - preferStart.Value))
                           .ThenBy(p => p.id_waktu).ToList();

            var chosen = new List<Gene>(requiredSks);
            int? last = null;

            // coba ambil berurutan
            foreach (var cand in pool)
            {
                if (!IsFeasible(cand, usage)) continue;
                if (last.HasValue && cand.id_waktu != last.Value + 1) continue;

                chosen.Add(cand);
                usage.KelasTime.Add(KKey(cand.id_kelas, cand.id_waktu));
                usage.DosenTime.Add(DKey(cand.kode_dosen, cand.id_waktu));
                usage.RoomTime.Add(RKey(cand.kode_ruangan, cand.id_waktu));
                last = cand.id_waktu;

                if (chosen.Count == requiredSks) return chosen;
            }

            // kalau belum cukup, relaksasi urut tapi tetap no conflict
            foreach (var cand in pool)
            {
                if (chosen.Any(c => c.id_waktu == cand.id_waktu)) continue;
                if (!IsFeasible(cand, usage)) continue;

                chosen.Add(cand);
                usage.KelasTime.Add(KKey(cand.id_kelas, cand.id_waktu));
                usage.DosenTime.Add(DKey(cand.kode_dosen, cand.id_waktu));
                usage.RoomTime.Add(RKey(cand.kode_ruangan, cand.id_waktu));
                if (chosen.Count == requiredSks) break;
            }

            return chosen;
        }

        private void Repair(Chromosome chromo)
        {
            var usage = new Usage();
            var rebuilt = new List<Gene>(chromo.Genes.Count);

            foreach (var grp in chromo.Genes
                         .GroupBy(g => (g.id_kelas, g.kode_matkul, g.kode_dosen))
                         .OrderBy(g => g.Key.id_kelas).ThenBy(g => g.Key.kode_matkul))
            {
                int sks = grp.First().sks;

                int? preferStart = usage.KelasTime
                    .Where(k => k.StartsWith($"{grp.Key.id_kelas}#"))
                    .Select(k => int.Parse(k.Split('#')[1]))
                    .DefaultIfEmpty()
                    .OrderBy(x => x)
                    .FirstOrDefault();

                var picked = PickFeasibleBlock(grp.Key, sks, usage,
                    preferStart == 0 ? (int?)null : preferStart);

                rebuilt.AddRange(picked);
            }

            chromo.Genes = rebuilt;
            chromo.Fitness = FitnessFunction.HitungFitness(chromo, matkuls, waktus);
        }

        // ================= GA core =================

        public List<Chromosome> GenerateInitialPopulation(
            int populationSize,
            IProgress<GaProgress> progress = null,
            CancellationToken ct = default)
        {
            var population = new List<Chromosome>(populationSize);

            // Progres range: 20%–60%
            for (int i = 0; i < populationSize; i++)
            {
                ct.ThrowIfCancellationRequested();
                progress?.Report(new GaProgress
                {
                    Percent = 20 + (int)Math.Round(40.0 * i / Math.Max(1, populationSize)),
                    Message = $"Menyusun populasi awal {i + 1}/{populationSize}…"
                });

                var chromo = new Chromosome();
                var usage = new Usage();
                var sksPerDosen = new Dictionary<int, int>();

                var groups = allPossibleGenes
                    .GroupBy(g => (g.id_kelas, g.kode_matkul, g.kode_dosen))
                    .OrderBy(g => g.Key.id_kelas).ThenBy(g => g.Key.kode_matkul);

                foreach (var g in groups)
                {
                    int sks = g.First().sks;

                    int cur = sksPerDosen.TryGetValue(g.Key.kode_dosen, out var val) ? val : 0;
                    if (cur + sks > maxSKSPatut) continue;

                    var picked = PickFeasibleBlock(g.Key, sks, usage);
                    if (picked.Count() == sks)
                    {
                        chromo.Genes.AddRange(picked);
                        sksPerDosen[g.Key.kode_dosen] = cur + sks;
                    }
                }

                Repair(chromo);
                population.Add(chromo);
            }

            return population;
        }

        public Chromosome SelectParent(List<Chromosome> population)
        {
            const int tournamentSize = 3;
            int bestIdx = random.Next(population.Count);

            for (int i = 1; i < tournamentSize; i++)
            {
                int idx = random.Next(population.Count);
                if (population[idx].Fitness > population[bestIdx].Fitness)
                    bestIdx = idx;
            }
            return population[bestIdx];
        }


        public Chromosome Crossover(Chromosome p1, Chromosome p2)
        {
            if (p1.Genes.Count == 0) return new Chromosome { Genes = new List<Gene>(), Fitness = 0 };
            int cut = random.Next(1, p1.Genes.Count);

            var child = new Chromosome
            {
                Genes = p1.Genes.Take(cut).Concat(p2.Genes.Skip(cut)).ToList()
            };

            Repair(child);
            return child;
        }

        public void Mutate(Chromosome chromo, double mutationRate)
        {
            if (chromo.Genes.Count == 0) return;

            for (int i = 0; i < chromo.Genes.Count; i++)
            {
                if (random.NextDouble() >= mutationRate) continue;

                var old = chromo.Genes[i];
                var usage = BuildUsage(chromo.Genes.Where((_, idx) => idx != i));

                var key = (old.id_kelas, old.kode_matkul, old.kode_dosen);
                int fixedRoom = usage.FixedRoom.TryGetValue(key, out var fr) ? fr : old.kode_ruangan;

                var candidates = allPossibleGenes
                    .Where(g => g.id_kelas == key.id_kelas
                             && g.kode_matkul == key.kode_matkul
                             && g.kode_dosen == key.kode_dosen
                             && g.kode_ruangan == fixedRoom
                             && g.id_waktu != old.id_waktu)
                    .OrderBy(g => g.id_waktu)
                    .ToList();

                var neighborTimes = chromo.Genes
                    .Where(g => g.id_kelas == key.id_kelas && g.kode_matkul == key.kode_matkul && g.kode_dosen == key.kode_dosen)
                    .Select(g => g.id_waktu).ToList();

                int? anchor = neighborTimes.Count > 0 ? (int?)neighborTimes.OrderBy(x => x).First() : null;
                if (anchor.HasValue)
                    candidates = candidates.OrderBy(c => Math.Abs(c.id_waktu - anchor.Value))
                                           .ThenBy(c => c.id_waktu).ToList();

                foreach (var cand in candidates)
                {
                    if (!IsFeasible(cand, usage)) continue;
                    chromo.Genes[i] = cand;
                    break;
                }
            }

            Repair(chromo);
        }

        public Chromosome Evolve(
    List<Chromosome> population,
    int generations,
    double mutationRate,
    bool debug = false,
    IProgress<GaProgress> progress = null,
    CancellationToken ct = default)
        {
            // urutkan sekali di awal
            var current = population.OrderByDescending(c => c.Fitness).ToList();

            int eliteCount = Math.Max(1, current.Count / 10);       // 10% elit
            int stallLimit = 20;                                     // early stop
            int stall = 0;
            int bestSoFar = current[0].Fitness;

            for (int gen = 0; gen < generations; gen++)
            {
                ct.ThrowIfCancellationRequested();
                progress?.Report(new GaProgress
                {
                    Percent = 60 + (int)Math.Round(40.0 * gen / Math.Max(1, generations)),
                    Message = $"Evolve generasi {gen + 1}/{generations}…"
                });

                var next = new Chromosome[current.Count];

                // bawa elit apa adanya (tanpa mutate/repair)
                for (int i = 0; i < eliteCount; i++)
                    next[i] = current[i].Clone();

                // bangun anak-anak secara paralel
                var po = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount, CancellationToken = ct };
                Parallel.For(eliteCount, current.Count, po, i =>
                {
                    var p1 = SelectParent(current);
                    var p2 = SelectParent(current);

                    // Crossover yang sedikit lebih "ramah" blok (lihat poin 3),
                    // jika belum sempat implement, boleh pakai Crossover yang lama.
                    var child = CrossoverByBlock(p1, p2);

                    // Mutasi ringan (lihat poin 2)
                    MutateLight(child, mutationRate);

                    // Mutate/Repair sudah mengisi Fitness, jadi TIDAK perlu hitung lagi
                    next[i] = child;
                });

                current = next.OrderByDescending(c => c.Fitness).ToList();

                if (current[0].Fitness > bestSoFar) { bestSoFar = current[0].Fitness; stall = 0; }
                else stall++;

                if (debug && gen % 10 == 0)
                    System.Diagnostics.Debug.WriteLine($"Generasi {gen}: Fitness terbaik = {current[0].Fitness}");

                if (stall >= stallLimit) break; // early stop
            }

            progress?.Report(new GaProgress { Percent = 100, Message = "Selesai." });
            return current[0];
        }


        // Tambahkan helper Random yang thread-safe
        static class ThreadSafeRandom
        {
            private static int _seed = Environment.TickCount;

            // 1 RNG per thread
            [ThreadStatic] private static Random _local;

            public static Random Rng
            {
                get
                {
                    var r = _local;
                    if (r == null)
                    {
                        r = new Random(Interlocked.Increment(ref _seed));
                        _local = r;
                    }
                    return r;
                }
            }
        }

        public void MutateLight(Chromosome chromo, double mutationRate)
        {
            if (chromo.Genes.Count == 0) return;

            var usage = BuildUsage(chromo.Genes);                  // sekali saja

            for (int i = 0; i < chromo.Genes.Count; i++)
            {
                if (ThreadSafeRandom.Rng.NextDouble() >= mutationRate) continue;

                var old = chromo.Genes[i];

                // kosongkan jejak lama
                usage.KelasTime.Remove(KKey(old.id_kelas, old.id_waktu));
                usage.DosenTime.Remove(DKey(old.kode_dosen, old.id_waktu));
                usage.RoomTime.Remove(RKey(old.kode_ruangan, old.id_waktu));

                var key = (old.id_kelas, old.kode_matkul, old.kode_dosen);
                int fixedRoom = usage.FixedRoom.TryGetValue(key, out var fr) ? fr : old.kode_ruangan;

                // kandidat di-urut berdasarkan kedekatan waktu supaya tetap rapi
                var candidates = allPossibleGenes
                    .Where(g => g.id_kelas == key.id_kelas
                             && g.kode_matkul == key.kode_matkul
                             && g.kode_dosen == key.kode_dosen
                             && g.kode_ruangan == fixedRoom
                             && g.id_waktu != old.id_waktu)
                    .OrderBy(g => g.id_waktu);

                Gene picked = null;
                foreach (var cand in candidates)
                {
                    if (!IsFeasible(cand, usage)) continue;
                    picked = cand; break;
                }

                if (picked != null)
                {
                    chromo.Genes[i] = picked;
                    usage.KelasTime.Add(KKey(picked.id_kelas, picked.id_waktu));
                    usage.DosenTime.Add(DKey(picked.kode_dosen, picked.id_waktu));
                    usage.RoomTime.Add(RKey(picked.kode_ruangan, picked.id_waktu));
                }
                else
                {
                    // gagal cari pengganti → kembalikan ke lama
                    usage.KelasTime.Add(KKey(old.id_kelas, old.id_waktu));
                    usage.DosenTime.Add(DKey(old.kode_dosen, old.id_waktu));
                    usage.RoomTime.Add(RKey(old.kode_ruangan, old.id_waktu));
                }
            }

            // Rapikan keseluruhan sekali saja
            Repair(chromo);
            // Repair() Anda sudah memanggil HitungFitness() → selesai
        }

        public Chromosome CrossoverByBlock(Chromosome p1, Chromosome p2)
        {
            // kelompokkan per (kelas, matkul, dosen)
            var g1 = p1.Genes.GroupBy(g => (g.id_kelas, g.kode_matkul, g.kode_dosen))
                             .ToDictionary(x => x.Key, x => x.ToList());
            var g2 = p2.Genes.GroupBy(g => (g.id_kelas, g.kode_matkul, g.kode_dosen))
                             .ToDictionary(x => x.Key, x => x.ToList());

            var keys = g1.Keys.Union(g2.Keys).ToList();
            var childGenes = new List<Gene>(Math.Max(p1.Genes.Count, p2.Genes.Count));

            foreach (var key in keys)
            {
                List<Gene> from =
                    (ThreadSafeRandom.Rng.NextDouble() < 0.5 ? g1 : g2).TryGetValue(key, out var list) ? list : null;

                if (from == null && g1.TryGetValue(key, out var l1)) from = l1;
                if (from == null && g2.TryGetValue(key, out var l2)) from = l2;

                if (from != null) childGenes.AddRange(from);
            }

            var child = new Chromosome { Genes = childGenes };
            Repair(child);               // tetap rapikan sekali
            return child;
        }


    }

}
