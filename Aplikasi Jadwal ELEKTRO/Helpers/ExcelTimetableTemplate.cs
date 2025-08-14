using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplikasi_Jadwal_ELEKTRO.Models;
using ClosedXML.Excel;

namespace Aplikasi_Jadwal_ELEKTRO.Helpers
{
    public static class ExcelTimetableTemplate
    {
        // Buat sheet 1 halaman seperti contoh (grid kiri + panel kanan)
        public static void BuildTemplate(string pathXlsx, string prodi, string semesterTeks, string jurusan,
            List<string> kolomKelas, List<(TimeSpan start, TimeSpan end)> slotJam)
        {
            using (var wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("JADWAL");
                ws.Style.Font.FontName = "Arial Narrow";
                ws.Style.Font.FontSize = 10;

                // HEADER ATAS
                ws.Cell("B4").Value = $"JADWAL KULIAH SEMESTER {semesterTeks} TAHUN AKADEMIK {DateTime.Now.Year}/{DateTime.Now.Year + 1}";
                ws.Range("B4:AB4").Merge().Style
                  .Font.SetBold().Font.SetFontSize(12)
                  .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                ws.Cell("B5").Value = prodi.ToUpper();
                ws.Range("B5:AB5").Merge().Style
                  .Font.SetBold().Font.SetFontSize(11)
                  .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                ws.Cell("B6").Value = jurusan.ToUpper();
                ws.Range("B6:AB6").Merge().Style
                  .Font.SetBold()
                  .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                // Koordinat area (kamu bisa tweak)
                int startRow = 9;
                int colHari = 2;         // kolom B
                int colJam = 3;         // kolom C
                int colKelasStart = 4;   // kolom D
                int rightPanelStart = colKelasStart + kolomKelas.Count * 2 + 1; // spasi 1 kolom
                int rightPanelWidth = 10; // sampai kolom AB kira2

                // Header baris "PELAKSANAAN" & "SEMESTER"
                ws.Range(startRow - 2, colHari, startRow - 2, rightPanelStart + rightPanelWidth).Merge().Value = "PELAKSANAAN";
                ws.Range(startRow - 2, colHari, startRow - 2, rightPanelStart + rightPanelWidth).Style
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                    .Font.SetBold();

                ws.Range(startRow - 1, colHari, startRow - 1, rightPanelStart + rightPanelWidth).Merge().Value = $"SEMESTER - {semesterTeks}";
                ws.Range(startRow - 1, colHari, startRow - 1, rightPanelStart + rightPanelWidth).Style
                    .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                    .Font.SetBold();

                // Header kolom kiri
                ws.Cell(startRow, colHari).Value = "HARI";
                ws.Cell(startRow, colJam).Value = "JAM";
                ws.Range(startRow, colHari, startRow + 1, colHari).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                ws.Range(startRow, colJam, startRow + 1, colJam).Merge().Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Alignment.SetVertical(XLAlignmentVerticalValues.Center);

                // Header kelas (2 kolom per kelas: KODE MK & RUANG)
                for (int i = 0; i < kolomKelas.Count; i++)
                {
                    int c = colKelasStart + i * 2;
                    ws.Cell(startRow, c).Value = kolomKelas[i];
                    ws.Range(startRow, c, startRow, c + 1).Merge();
                    ws.Range(startRow, c, startRow, c + 1).Style
                        .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                        .Font.SetBold();

                    ws.Cell(startRow + 1, c).Value = "KODE MK";
                    ws.Cell(startRow + 1, c + 1).Value = "RUANG";
                    ws.Range(startRow + 1, c, startRow + 1, c + 1).Style
                        .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                }

                // Panel kanan header
                int rpRow = startRow;
                ws.Range(rpRow, rightPanelStart, rpRow, rightPanelStart + rightPanelWidth - 1).Merge().Value = "MATA KULIAH";
                ws.Range(rpRow, rightPanelStart, rpRow, rightPanelStart + rightPanelWidth - 1).Style
                    .Font.SetBold().Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                // (Nanti isi list MK & dosen di sini)

                // Isi grid: 5 hari x slotJam.Count
                string[] hari = { "SENIN", "SELASA", "RABU", "KAMIS", "JUMAT" };
                int row = startRow + 2;
                foreach (var h in hari)
                {
                    // merge sel HARI vertikal sepanjang jumlah sesi hari itu
                    ws.Range(row, colHari, row + slotJam.Count - 1, colHari).Merge().Value = h;
                    ws.Range(row, colHari, row + slotJam.Count - 1, colHari).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center)
                        .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center).Font.SetBold();

                    for (int s = 0; s < slotJam.Count; s++)
                    {
                        ws.Cell(row + s, colJam).Value = $"{slotJam[s].start:hh\\:mm} - {slotJam[s].end:hh\\:mm}";
                        ws.Cell(row + s, colJam).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        // kolom kelas kosong dulu (nanti diisi)
                        for (int i = 0; i < kolomKelas.Count; i++)
                        {
                            int c = colKelasStart + i * 2;
                            ws.Cell(row + s, c).Value = "";       // KODE MK
                            ws.Cell(row + s, c + 1).Value = "";   // RUANG
                        }
                    }
                    row += slotJam.Count;
                }

                // Style border kotak besar
                var lastRow = row - 1;
                var leftTable = ws.Range(startRow, colHari, lastRow, colKelasStart + kolomKelas.Count * 2 - 1);
                leftTable.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                leftTable.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                // Column widths
                ws.Column(colHari).Width = 10; // HARI
                ws.Column(colJam).Width = 12; // JAM
                for (int i = 0; i < kolomKelas.Count * 2; i++)
                    ws.Column(colKelasStart + i).Width = (i % 2 == 0) ? 10 : 6;

                ws.Columns().AdjustToContents(3, 60);

                wb.SaveAs(pathXlsx);
            }
        }

        // Isi grid berdasarkan data jadwal -> value KODE MK & RUANG
        // tambahkan parameter baru (map id_kelas -> namaKelas)
        public static void FillFromData(string pathXlsx, List<JadwalFinal> jadwal,
            List<Waktu> waktus, List<Matkul> matkuls, List<Ruangan> ruangans,
            Dictionary<string, int> mapKelasKeIndex,
            Dictionary<int, int> mapSesiIndex,
            Dictionary<int, string> namaKelasById = null)
        {
            using (var wb = new XLWorkbook(pathXlsx))
            {
                var ws = wb.Worksheet("JADWAL");
                int startRow = 9, colJam = 3, colKelasStart = 4;

                // untuk hitung baris: slotsPerDay = max index + 1
                int slotsPerDay = mapSesiIndex.Values.Any() ? (mapSesiIndex.Values.Max() + 1) : 10;
                var dayIndex = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        { {"Senin",0}, {"Selasa",1}, {"Rabu",2}, {"Kamis",3}, {"Jumat",4} };

                foreach (var j in jadwal)
                {
                    var w = waktus.First(x => x.id_waktu == j.id_waktu);
                    if (!dayIndex.ContainsKey(w.hari)) continue;

                    int sesiIndex;
                    if (!mapSesiIndex.TryGetValue(w.id_waktu, out sesiIndex)) continue;

                    int baseRow = (startRow + 2) + dayIndex[w.hari] * slotsPerDay;
                    int row = baseRow + sesiIndex;

                    // namaKelas → index kolom
                    string namaKelas = namaKelasById != null && namaKelasById.ContainsKey(j.id_kelas)
                        ? namaKelasById[j.id_kelas] : null;
                    if (string.IsNullOrEmpty(namaKelas)) continue;
                    if (!mapKelasKeIndex.ContainsKey(namaKelas)) continue;

                    int kelasIndex = mapKelasKeIndex[namaKelas];
                    int c = colKelasStart + kelasIndex * 2;

                    var m = matkuls.First(x => x.kode_matkul == j.kode_matkul);
                    var r = ruangans.First(x => x.kode_ruangan == j.kode_ruangan);

                    ws.Cell(row, c).Value = m.kode_matkul;
                    ws.Cell(row, c + 1).Value = r.nama;
                }
                wb.Save();
            }
        }


        // Tambahkan di ExcelTimetableTemplate (atau file helper baru)
        public static class WaktuMapping
        {
            static int ParseSesi(string sesi)
            {
                if (string.IsNullOrWhiteSpace(sesi)) return int.MaxValue;
                var digits = new string(sesi.Where(char.IsDigit).ToArray());
                int n; return int.TryParse(digits, out n) ? n : int.MaxValue;
            }

            public static (List<(TimeSpan start, TimeSpan end)> slotJam,
                           Dictionary<int, int> mapSesiIndex,
                           int slotsPerDay,
                           Dictionary<string, int> dayIndex)
                Build(List<Waktu> waktus, string tipeKelas) // "Pagi" / "Sore" jika dipakai
            {
                var dayOrder = new[] { "Senin", "Selasa", "Rabu", "Kamis", "Jumat" };
                var dayIndex = dayOrder.Select((d, i) => new { d, i })
                                       .ToDictionary(x => x.d, x => x.i, StringComparer.OrdinalIgnoreCase);

                var wFiltered = waktus
                    .Where(w => (w.keterangan ?? "").ToLower() != "istirahat"
                             && (string.IsNullOrEmpty(tipeKelas) || string.Equals(w.tipe_kelas, tipeKelas, StringComparison.OrdinalIgnoreCase)))
                    .ToList();

                // ambil urutan sesi unik (berdasarkan teks "P1","P2",...)
                var sesiOrdered = wFiltered
                    .Where(w => dayIndex.ContainsKey(w.hari))
                    .GroupBy(w => new { w.hari, idx = ParseSesi(w.sesi) })
                    .OrderBy(g => dayIndex[g.Key.hari]).ThenBy(g => g.Key.idx)
                    .Select(g => g.First()) // representative per (hari,sesi)
                    .ToList();

                // slots per day = jumlah sesi berbeda pada satu hari (anggap semua hari sama)
                int slotsPerDay = wFiltered.Where(w => string.Equals(w.hari, "Senin", StringComparison.OrdinalIgnoreCase))
                                           .Select(w => ParseSesi(w.sesi)).Distinct().Count();

                if (slotsPerDay == 0) slotsPerDay = sesiOrdered.Select(x => ParseSesi(x.sesi)).Distinct().Count();

                // list jam urut (pakai hari Senin sebagai referensi)
                var slotJam = wFiltered.Where(w => string.Equals(w.hari, "Senin", StringComparison.OrdinalIgnoreCase))
                                       .OrderBy(w => ParseSesi(w.sesi))
                                       .Select(w => (w.jam_mulai, w.jam_selesai))
                                       .Distinct()
                                       .ToList();

                // map id_waktu -> index sesi (0..slotsPerDay-1) per harinya
                var mapSesiIndex = new Dictionary<int, int>();
                foreach (var d in dayOrder)
                {
                    var sesiDiHari = wFiltered.Where(w => string.Equals(w.hari, d, StringComparison.OrdinalIgnoreCase))
                                              .OrderBy(w => ParseSesi(w.sesi))
                                              .ToList();
                    for (int i = 0; i < sesiDiHari.Count; i++)
                        mapSesiIndex[sesiDiHari[i].id_waktu] = i;
                }

                return (slotJam, mapSesiIndex, slotsPerDay, dayIndex);
            }
        }


    }

}
