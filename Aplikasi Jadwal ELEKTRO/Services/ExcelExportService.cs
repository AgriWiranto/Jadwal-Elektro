using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplikasi_Jadwal_ELEKTRO.Helpers;
using Aplikasi_Jadwal_ELEKTRO.Models;
using ClosedXML.Excel;

namespace Aplikasi_Jadwal_ELEKTRO.Services
{
    public static class ExcelExportService
    {
        // ===== A. Per KELAS (1 sheet per kelas) =====
        // Data yang dipakai: Dictionary<string, DataTable> sama persis seperti PDF per kelas
        // Kolom DataTable: Hari, Sesi, Jam, Mata Kuliah/MataKuliah, Dosen, Ruangan
        public static void ExportPerKelasXlsx(
    Dictionary<string, DataTable> dataPerKelas,
    string filePath,
    string institusi,
    string subTitle)
        {
            if (dataPerKelas == null || dataPerKelas.Count == 0)
                throw new ArgumentException("dataPerKelas kosong.");

            using (var wb = new XLWorkbook())
            {
                foreach (var kv in dataPerKelas)
                {
                    var ws = wb.Worksheets.Add(SheetNameSafe(kv.Key));
                    int r = 1;
                    ws.Cell(r, 1).Value = institusi;
                    ws.Range(r, 1, r, 6).Merge().Style
                        .Font.SetBold().Font.SetFontSize(14)
                        .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    r++;

                    ws.Cell(r, 1).Value = subTitle;
                    ws.Range(r, 1, r, 6).Merge().Style
                        .Font.SetBold().Font.SetFontSize(11)
                        .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    r++;

                    ws.Cell(r, 1).Value = "KELAS: " + kv.Key;
                    ws.Range(r, 1, r, 6).Merge().Style
                        .Font.SetBold()
                        .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    r += 2;

                    string[] headers = { "Hari", "Sesi", "Jam", "Mata Kuliah", "Dosen", "Ruangan" };
                    for (int c = 0; c < headers.Length; c++)
                    {
                        ws.Cell(r, c + 1).Value = headers[c];
                        ws.Cell(r, c + 1).Style.Font.SetBold();
                        ws.Cell(r, c + 1).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                        ws.Cell(r, c + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    }
                    r++;

                    var dt = kv.Value;
                    string Col(string name)
                    {
                        if (dt.Columns.Contains(name)) return name;
                        var alt = name.Replace(" ", "");
                        if (dt.Columns.Contains(alt)) return alt;
                        var found = dt.Columns.Cast<DataColumn>().FirstOrDefault(x =>
                            x.ColumnName.Equals(name, StringComparison.OrdinalIgnoreCase) ||
                            x.ColumnName.Equals(alt, StringComparison.OrdinalIgnoreCase));
                        return found?.ColumnName;
                    }
                    string colHari = Col("Hari");
                    string colSesi = Col("Sesi");
                    string colJam = Col("Jam");
                    string colMK = Col("Mata Kuliah") ?? Col("MataKuliah");
                    string colDsn = Col("Dosen");
                    string colRg = Col("Ruangan");

                    foreach (DataRow dr in dt.Rows)
                    {
                        var vals = new string[]
                        {
                    colHari==null?"":Convert.ToString(dr[colHari]),
                    colSesi==null?"":Convert.ToString(dr[colSesi]),
                    colJam ==null?"":Convert.ToString(dr[colJam]),
                    colMK  ==null?"":Convert.ToString(dr[colMK]),
                    colDsn ==null?"":Convert.ToString(dr[colDsn]),
                    colRg  ==null?"":Convert.ToString(dr[colRg]),
                        };

                        bool separator = vals.All(v => string.IsNullOrWhiteSpace(v));
                        for (int c = 0; c < headers.Length; c++)
                        {
                            ws.Cell(r, c + 1).Value = vals[c];
                            ws.Cell(r, c + 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            if (separator)
                                ws.Cell(r, c + 1).Style.Fill.SetBackgroundColor(XLColor.LightGray);
                        }
                        r++;
                    }

                    ws.Columns(1, 6).AdjustToContents();
                    ws.SheetView.FreezeRows(4);
                    ws.PageSetup.PageOrientation = XLPageOrientation.Landscape;
                    ws.PageSetup.FitToPages(1, 0);
                }

                wb.SaveAs(filePath);
            }
        }


        // ===== B. Per PRODI & SEMESTER -> sheet per (Prodi, Semester, chunk kelas) =====
        // Pakai layout grid seperti PDF (HARI | JAM | [KODE MK | RUANG]*), tapi Excel
        public static void ExportPerGrupProdiSemesterXlsx(
    List<ProdiSemesterGroup> groups,
    List<JadwalFinal> jadwal,
    List<Waktu> waktus,
    List<Matkul> matkuls,
    List<Ruangan> ruangans,
    string filePath,
    Dictionary<string, DataTable> dataTabs = null,
    bool landscape = true)
        {
            using (var wb = new XLWorkbook())
            {
                // ... (isi method tetap sama seperti sebelumnya)
                // tidak ada "using var" di dalamnya
                wb.SaveAs(filePath);
            }
        }


        // ==== helpers ====
        private static string SheetNameSafe(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return "Sheet";
            var invalid = new[] { "\\", "/", "*", "[", "]", ":", "?" };
            foreach (var ch in invalid) name = name.Replace(ch, "");
            return name.Length > 31 ? name.Substring(0, 31) : name;
        }

        private static Dictionary<string, List<string>> BuildJamPerHariFromWaktu(IEnumerable<int> kelasIds, List<Waktu> semua)
        {
            var days = new[] { "Senin", "Selasa", "Rabu", "Kamis", "Jumat" };
            var map = days.ToDictionary(d => d, d => new List<string>(), StringComparer.OrdinalIgnoreCase);

            var q = semua.Where(w => kelasIds.Contains(w.id_kelas) && !string.Equals(w.keterangan, "istirahat", StringComparison.OrdinalIgnoreCase))
                         .GroupBy(w => w.hari, StringComparer.OrdinalIgnoreCase);
            foreach (var g in q)
                map[g.Key] = g.OrderBy(w => w.jam_mulai).Select(w => $"{w.jam_mulai:hh\\:mm} - {w.jam_selesai:hh\\:mm}").Distinct().ToList();

            return map;
        }

        private static bool TryFillFromTabsOrDbExcel(
            string hari, string jamLabel, string namaKelas,
            Dictionary<string, DataTable> dataTabs,
            List<JadwalFinal> jadwal,
            List<Waktu> waktus,
            List<Matkul> matkuls,
            List<Ruangan> ruangans,
            Dictionary<int, string> namaKelasById,
            Dictionary<int, string> labelByIdWaktu,
            out string kodeMk, out string ruang)
        {
            kodeMk = ""; ruang = "";

            // dari tabs
            if (dataTabs != null && dataTabs.TryGetValue(namaKelas, out var dt) && dt != null)
            {
                string Col(string n)
                {
                    if (dt.Columns.Contains(n)) return n;
                    var alt = n.Replace(" ", "");
                    if (dt.Columns.Contains(alt)) return alt;
                    var c = dt.Columns.Cast<DataColumn>().FirstOrDefault(x =>
                        x.ColumnName.Equals(n, StringComparison.OrdinalIgnoreCase) ||
                        x.ColumnName.Equals(alt, StringComparison.OrdinalIgnoreCase));
                    return c?.ColumnName;
                }

                var cHari = Col("Hari");
                var cJam = Col("Jam");
                var cMK = Col("Mata Kuliah") ?? Col("MataKuliah");
                var cRg = Col("Ruangan");

                if (cHari != null && cJam != null && cMK != null && cRg != null)
                {
                    var dr = dt.AsEnumerable().FirstOrDefault(r =>
                        string.Equals(Convert.ToString(r[cHari]).Trim(), hari, StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(Convert.ToString(r[cJam]).Trim(), jamLabel, StringComparison.OrdinalIgnoreCase));
                    if (dr != null)
                    {
                        var mkNama = Convert.ToString(dr[cMK]).Trim();
                        ruang = Convert.ToString(dr[cRg]).Trim();
                        var m = matkuls.FirstOrDefault(x => string.Equals((x.nama_matkul ?? "").Trim(), mkNama, StringComparison.OrdinalIgnoreCase));
                        kodeMk = m != null ? Convert.ToString(m.kode_matkul) : "";
                        return true;
                    }
                }
            }

            // fallback DB
            var match = jadwal.FirstOrDefault(j =>
            {
                if (!namaKelasById.TryGetValue(j.id_kelas, out var nk) || !string.Equals(nk, namaKelas, StringComparison.Ordinal)) return false;
                if (!labelByIdWaktu.TryGetValue(j.id_waktu, out var lbl)) return false;
                var w = waktus.FirstOrDefault(x => x.id_waktu == j.id_waktu);
                if (w == null || !string.Equals(w.hari, hari, StringComparison.OrdinalIgnoreCase)) return false;
                return string.Equals(lbl, jamLabel, StringComparison.Ordinal);
            });

            if (match != null)
            {
                var m = matkuls.FirstOrDefault(x => x.kode_matkul == match.kode_matkul);
                var r = ruangans.FirstOrDefault(x => x.kode_ruangan == match.kode_ruangan);
                kodeMk = m != null ? Convert.ToString(m.kode_matkul) : "";
                ruang = r != null ? r.nama : "";
                return true;
            }

            return false;
        }
    }
}
