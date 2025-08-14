using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplikasi_Jadwal_ELEKTRO.Constants;

namespace Aplikasi_Jadwal_ELEKTRO.Models.Genetic
{
    public class FitnessFunction
    {
        public static int HitungFitness(
    Chromosome kromosom,
    List<Matkul> matkuls,
    List<Waktu> waktus,
    List<Gene> genesGagal = null)
        {
            int fitness = 1000;
            var genes = kromosom.Genes;
            if (genes == null || genes.Count == 0) return fitness;

            // Lookup cepat
            var mkBy = matkuls.ToDictionary(m => m.kode_matkul);
            var wBy = waktus.ToDictionary(w => w.id_waktu);

            // Struktur sekali-lewat
            var seenDosenTime = new HashSet<(int dosen, int waktu)>();
            var seenKelasTime = new HashSet<(int kelas, int waktu)>();
            var seenRuangTime = new HashSet<(int ruang, int waktu)>();
            int cDosen = 0, cKelas = 0, cRuang = 0;

            var sksPerDosen = new Dictionary<int, int>();

            // Cek konsistensi ruangan per (dosen, mk, kelas)
            var roomByTriplet = new Dictionary<(int dosen, int mk, int kelas), int>();
            var roomInconsistentFlag = new HashSet<(int dosen, int mk, int kelas)>(); // agar penalti dihitung sekali

            // Kumpulkan sesi per (kelas, mk) untuk cek SKS & urutan
            var sesiByKelasMk = new Dictionary<(int kelas, int mk), List<int>>();
            var sksRequired = new Dictionary<(int kelas, int mk), int>();

            foreach (var g in genes)
            {
                // Konflik waktu (hitung sekali)
                if (!seenDosenTime.Add((g.kode_dosen, g.id_waktu))) cDosen++;
                if (!seenKelasTime.Add((g.id_kelas, g.id_waktu))) cKelas++;
                if (!seenRuangTime.Add((g.kode_ruangan, g.id_waktu))) cRuang++;

                // Preferensi ruangan & tipe kelas
                Matkul mk;
                if (mkBy.TryGetValue(g.kode_matkul, out mk))
                {
                    int pref;
                    if (!string.IsNullOrWhiteSpace(mk.ruangan_preferensi) &&
                        int.TryParse(mk.ruangan_preferensi, out pref))
                    {
                        fitness += (pref == g.kode_ruangan) ? 2 : -2;
                    }
                }

                Waktu w;
                if (wBy.TryGetValue(g.id_waktu, out w) &&
                    !string.Equals(w.tipe_kelas, g.tipe_kelas, StringComparison.OrdinalIgnoreCase))
                {
                    fitness -= 5;
                }

                // Akumulasi SKS per dosen
                int total;
                if (sksPerDosen.TryGetValue(g.kode_dosen, out total))
                    sksPerDosen[g.kode_dosen] = total + g.sks;
                else
                    sksPerDosen[g.kode_dosen] = g.sks;

                // Konsistensi ruangan (penalti sekali per triplet jika berbeda)
                var trip = (g.kode_dosen, g.kode_matkul, g.id_kelas);
                int fixedRoom;
                if (roomByTriplet.TryGetValue(trip, out fixedRoom))
                {
                    if (fixedRoom != g.kode_ruangan && roomInconsistentFlag.Add(trip))
                        fitness -= 25;
                }
                else
                {
                    roomByTriplet[trip] = g.kode_ruangan;
                }

                // Kumpulkan sesi untuk cek SKS & urutan
                var keyKm = (g.id_kelas, g.kode_matkul);
                List<int> list;
                if (!sesiByKelasMk.TryGetValue(keyKm, out list))
                {
                    list = new List<int>(g.sks);
                    sesiByKelasMk[keyKm] = list;
                    sksRequired[keyKm] = g.sks; // g.sks sama untuk semua gene dalam (kelas, mk)
                }
                list.Add(g.id_waktu);
            }

            // Penalti konflik (pakai hitungan yang sudah jadi)
            fitness -= 10 * (cDosen + cKelas + cRuang);

            // Penalti overload SKS dosen
            foreach (var kv in sksPerDosen)
            {
                int overload = kv.Value - AppConstants.MAX_SKS_PER_DOSEN;
                if (overload > 0) fitness -= overload * 5;
            }

            // Validasi SKS & urutan sesi tanpa GroupBy
            foreach (var kv in sesiByKelasMk)
            {
                var key = kv.Key;
                var sesiList = kv.Value;
                sesiList.Sort();

                int required = sksRequired[key];
                if (sesiList.Count != required)
                {
                    fitness -= 10;
                    if (genesGagal != null)
                    {
                        // Tambahkan hanya saat gagal (jumlahnya biasanya kecil)
                        foreach (var g in genes)
                            if (g.id_kelas == key.kelas && g.kode_matkul == key.mk)
                                genesGagal.Add(g);
                    }
                }
                else
                {
                    for (int i = 1; i < sesiList.Count; i++)
                    {
                        if (sesiList[i] != sesiList[i - 1] + 1)
                        {
                            fitness -= 5;
                            break;
                        }
                    }
                }
            }

            // Bonus no-conflict
            if (cDosen == 0 && cKelas == 0 && cRuang == 0) fitness += 50;

            return fitness;
        }



        private static int HitungKonflik(IEnumerable<Gene> genes, Func<Gene, object> selector)
        {
            return genes.GroupBy(selector).Count(g => g.Count() > 1);
        }
    }
}
