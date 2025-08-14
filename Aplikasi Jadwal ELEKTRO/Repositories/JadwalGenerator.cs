using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aplikasi_Jadwal_ELEKTRO.Models;
using Aplikasi_Jadwal_ELEKTRO.Models.Genetic;
using Aplikasi_Jadwal_ELEKTRO.Repositories.Genetic;

namespace Aplikasi_Jadwal_ELEKTRO.Repositories
{
    public class JadwalGenerator
    {
        private readonly List<Dosen> dosens;
        private readonly List<Matkul> matkuls;
        private readonly List<DosenMatkul> dosenMatkuls;
        private readonly List<Kelas> kelass;
        private readonly List<KelasMatkul> kelasMatkuls;
        private readonly List<Waktu> waktus;
        private readonly List<Ruangan> ruangans;

        private List<Gene> AllGeneratedGenes;
        private readonly Random random = new Random();

        public JadwalGenerator(
            List<Dosen> dosens,
            List<Kelas> kelass,
            List<Matkul> matkuls,
            List<DosenMatkul> dosenMatkuls,
            List<KelasMatkul> kelasMatkuls,
            List<Waktu> waktus,
            List<Ruangan> ruangans)
        {
            this.dosens = dosens;
            this.kelass = kelass;
            this.matkuls = matkuls;
            this.dosenMatkuls = dosenMatkuls;
            this.kelasMatkuls = kelasMatkuls;
            this.waktus = waktus;
            this.ruangans = ruangans;
        }

        public (List<JadwalFinal> jadwals, List<Gene> gagalGenes) Generate(
    IProgress<GaProgress> progress = null,
    CancellationToken ct = default)
        {
            progress?.Report(new GaProgress { Percent = 0, Message = "Membangun daftar kandidat gene..." });

            var allGenes = new List<Gene>();
            var ruanganTetap = new Dictionary<string, int>();

            int totalKelasMatkul = kelasMatkuls.Count;
            int processed = 0;

            foreach (var km in kelasMatkuls)
            {
                ct.ThrowIfCancellationRequested();

                var kelas = kelass.FirstOrDefault(k => k.id_kelas == km.id_kelas);
                var matkul = matkuls.FirstOrDefault(m => m.kode_matkul == km.kode_matkul);
                var dosenList = dosenMatkuls.Where(dm => dm.kode_matkul == km.kode_matkul).ToList();

                if (kelas == null || matkul == null || dosenList.Count == 0)
                {
                    processed++;
                    continue;
                }

                var waktuKelas = waktus
                    .Where(w => w.tipe_kelas.Equals(kelas.tipe_kelas, StringComparison.OrdinalIgnoreCase)
                             && w.keterangan?.ToLower() != "istirahat")
                    .ToList();

                var dosen = dosenList.First();
                string key = $"{kelas.id_kelas}-{matkul.kode_matkul}-{dosen.kode_dosen}";

                if (!ruanganTetap.ContainsKey(key))
                {
                    if (int.TryParse(matkul.ruangan_preferensi, out int ruanganPref) && ruangans.Any(r => r.kode_ruangan == ruanganPref))
                        ruanganTetap[key] = ruanganPref;
                    else
                        ruanganTetap[key] = ruangans[random.Next(ruangans.Count)].kode_ruangan;
                }

                int ruangan = ruanganTetap[key];

                foreach (var waktu in waktuKelas)
                {
                    allGenes.Add(new Gene
                    {
                        kode_dosen = dosen.kode_dosen,
                        kode_matkul = matkul.kode_matkul,
                        id_kelas = kelas.id_kelas,
                        id_waktu = waktu.id_waktu,
                        kode_ruangan = ruangan,
                        sks = matkul.sks,
                        preferensi_ruangan = matkul.ruangan_preferensi,
                        tipe_kelas = waktu.tipe_kelas
                    });
                }

                processed++;
                int percent = (int)Math.Round(20.0 * processed / Math.Max(1, totalKelasMatkul)); // 0–20%
                progress?.Report(new GaProgress { Percent = percent, Message = $"Membangun gene {processed}/{totalKelasMatkul}" });
            }

            this.AllGeneratedGenes = allGenes;

            var populationManager = new PopulationManager(allGenes, matkuls, waktus);
            var initialPopulation = populationManager.GenerateInitialPopulation(30, progress, ct);
            var bestChromosome = populationManager.Evolve(initialPopulation, 100, 0.05, false, progress, ct);

            var jadwalFinals = bestChromosome.Genes.Select(g => new JadwalFinal
            {
                kode_dosen = g.kode_dosen,
                kode_matkul = g.kode_matkul,
                id_kelas = g.id_kelas,
                id_waktu = g.id_waktu,
                kode_ruangan = g.kode_ruangan
            }).ToList();

            new GeneticLogRepository().InsertLog(new GeneticLog
            {
                generation = 50,
                fitness = bestChromosome.Fitness,
                total_genes = bestChromosome.Genes.Count,
                timestamp = DateTime.Now
            });

            var jadwalKeys = new HashSet<string>(jadwalFinals.Select(g => $"{g.kode_dosen}-{g.kode_matkul}-{g.id_kelas}-{g.id_waktu}-{g.kode_ruangan}"));
            var gagalGenes = AllGeneratedGenes.Where(g => !jadwalKeys.Contains(g.GetKey())).ToList();

            return (jadwalFinals, gagalGenes);
        }

    }

}
