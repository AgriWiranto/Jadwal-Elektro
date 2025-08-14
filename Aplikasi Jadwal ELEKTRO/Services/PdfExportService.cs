using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Aplikasi_Jadwal_ELEKTRO.Helpers;
using Aplikasi_Jadwal_ELEKTRO.Models;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using static Aplikasi_Jadwal_ELEKTRO.Helpers.ExcelTimetableTemplate;

namespace Aplikasi_Jadwal_ELEKTRO.Services
{
    public static class PdfExportService
    {
        /// <summary>
        /// Cetak PDF per KELAS (dikelompokkan per Prodi & Semester).
        /// Utamakan data dari TabControl (DGV) agar hasilnya persis seperti tampilan.
        /// Fallback ke data DB (Jadwal/Waktu/Matkul/Dosen/Ruangan) jika kolom DGV tidak lengkap.
        /// </summary>
        public static void ExportPerSemesterPerKelas(
            List<ProdiSemesterGroup> groups,
            List<JadwalFinal> jadwal,
            List<Waktu> waktus,
            List<Matkul> matkuls,
            List<Dosen> dosens,
            List<Ruangan> ruangans,
            string logoPath,
            string jurusanHeader,
            string outputPath,
            Dictionary<string, DataTable> dataTabs = null,
            bool landscape = true,
            PageFormat format = PageFormat.A3)
        {
            if (groups == null || groups.Count == 0)
                throw new ArgumentException("groups kosong.");
            if (string.IsNullOrWhiteSpace(outputPath))
                throw new ArgumentException("outputPath wajib diisi.");

            var doc = CreateBaseDocument("Jadwal Perkuliahan (Per Kelas)");

            // lookup cepat untuk fallback DB
            var mkByKode = matkuls.ToDictionary(m => m.kode_matkul);
            var dsnByKode = dosens.ToDictionary(d => d.kode_dosen);
            var rngByKode = ruangans.ToDictionary(r => r.kode_ruangan);

            // urutan hari rapi
            var dayOrder = new[] { "Senin", "Selasa", "Rabu", "Kamis", "Jumat" }
                           .Select((h, i) => new { h, i })
                           .ToDictionary(x => x.h, x => x.i, StringComparer.OrdinalIgnoreCase);

            int SesiNum(string sesi)
            {
                if (string.IsNullOrEmpty(sesi)) return int.MaxValue;
                var digits = new string(sesi.Where(char.IsDigit).ToArray());
                return int.TryParse(digits, out var n) ? n : int.MaxValue;
            }

            foreach (var g in groups)
            {
                foreach (var kelas in g.KelasList.OrderBy(k => k.namaKelas))
                {
                    var sec = AddSection(doc, landscape, format);

                    // Header + judul kelas
                    AddHeader(
                        section: sec,
                        logoPath: logoPath,
                        institusi: g.ProdiHeader,
                        subTitle: $"{g.SemesterHeader}\n{(string.IsNullOrWhiteSpace(jurusanHeader) ? "JURUSAN" : jurusanHeader)}"
                    );
                    AddClassTitle(sec, kelas.namaKelas);

                    // Siapkan DataTable yang akan dirender
                    var dt = new DataTable();
                    dt.Columns.Add("Hari");
                    dt.Columns.Add("Sesi");
                    dt.Columns.Add("Jam");
                    dt.Columns.Add("Mata Kuliah");
                    dt.Columns.Add("Dosen");
                    dt.Columns.Add("Ruangan");

                    bool filledFromTabs = false;

                    // 1) UTAMA: dari TabControl (DGV)
                    if (dataTabs != null && dataTabs.TryGetValue(kelas.namaKelas, out var fromTabs) && fromTabs != null)
                    {
                        string cH = ResolveCol(fromTabs, "Hari");
                        string cS = ResolveCol(fromTabs, "Sesi");
                        string cJ = ResolveCol(fromTabs, "Jam");
                        string cM = ResolveCol(fromTabs, "Mata Kuliah") ?? ResolveCol(fromTabs, "MataKuliah");
                        string cD = ResolveCol(fromTabs, "Dosen");
                        string cR = ResolveCol(fromTabs, "Ruangan");

                        if (cH != null && cS != null && cJ != null && cM != null && cR != null)
                        {
                            foreach (DataRow r in fromTabs.Rows)
                            {
                                dt.Rows.Add(
                                    Convert.ToString(r[cH]),
                                    Convert.ToString(r[cS]),
                                    Convert.ToString(r[cJ]),
                                    Convert.ToString(r[cM]),
                                    Convert.ToString(r[cD]),
                                    Convert.ToString(r[cR])
                                );
                            }
                            filledFromTabs = true;
                        }
                    }

                    // 2) FALLBACK: dari DB (jika data DGV tidak ada / tidak lengkap)
                    if (!filledFromTabs)
                    {
                        var rows = from j in jadwal.Where(x => x.id_kelas == kelas.id_kelas)
                                   join w in waktus on j.id_waktu equals w.id_waktu
                                   where !string.Equals(w.keterangan, "istirahat", StringComparison.OrdinalIgnoreCase)
                                   let jam = $"{w.jam_mulai:hh\\:mm} - {w.jam_selesai:hh\\:mm}"
                                   let mk = mkByKode.TryGetValue(j.kode_matkul, out var m) ? m.nama_matkul : ""
                                   let ds = dsnByKode.TryGetValue(j.kode_dosen, out var d) ? d.nama_dosen : ""
                                   let rg = rngByKode.TryGetValue(j.kode_ruangan, out var r) ? r.nama : ""
                                   orderby (dayOrder.ContainsKey(w.hari) ? dayOrder[w.hari] : 999),
                                           SesiNum(w.sesi)
                                   select new { w.hari, w.sesi, jam, mk, ds, rg };

                        foreach (var x in rows)
                            dt.Rows.Add(x.hari, x.sesi, x.jam, x.mk, x.ds, x.rg);
                    }

                    // Render tabel jadwal
                    BuildScheduleTableFromDT(sec, dt);

                    // Footer
                    AddFooter(sec);
                }
            }

            // Render & save
            var pdf = new PdfDocumentRenderer();
            pdf.Document = doc;
            pdf.RenderDocument();
            pdf.PdfDocument.Save(outputPath);
        }

        // ===================== Layout helpers =====================

        private static Document CreateBaseDocument(string title)
        {
            var doc = new Document();
            doc.Info.Title = title ?? "Document";

            var normal = doc.Styles["Normal"];
            normal.Font.Name = "Segoe UI";
            normal.Font.Size = 9; // turunkan ke 8 kalau masih mepet

            var titleStyle = doc.Styles.AddStyle("HeaderTitle", "Normal");
            titleStyle.Font.Size = 12;
            titleStyle.Font.Bold = true;

            var subStyle = doc.Styles.AddStyle("SubTitle", "Normal");
            subStyle.Font.Size = 9;
            subStyle.Font.Bold = true;

            return doc;
        }

        private static Section AddSection(Document doc, bool landscape, PageFormat format)
        {
            var sec = doc.AddSection();
            sec.PageSetup.PageFormat = format;
            sec.PageSetup.Orientation = landscape ? Orientation.Landscape : Orientation.Portrait;
            sec.PageSetup.TopMargin = Unit.FromCentimeter(2.0);
            sec.PageSetup.BottomMargin = Unit.FromCentimeter(1.5);
            sec.PageSetup.LeftMargin = Unit.FromCentimeter(1.5);
            sec.PageSetup.RightMargin = Unit.FromCentimeter(1.5);
            return sec;
        }

        private static void AddHeader(Section section, string logoPath, string institusi, string subTitle)
        {
            // hitung lebar area tulis (agar kolom teks adaptif A4/A3)
            var ps = section.PageSetup;
            var usableWidth = ps.PageWidth - ps.LeftMargin - ps.RightMargin;
            var logoCol = Unit.FromCentimeter(2.2);
            var textCol = usableWidth - logoCol;

            var t = section.Headers.Primary.AddTable();
            t.Borders.Visible = false;
            t.AddColumn(logoCol);
            t.AddColumn(textCol);

            var r = t.AddRow();
            if (!string.IsNullOrWhiteSpace(logoPath) && File.Exists(logoPath))
            {
                try
                {
                    var img = r.Cells[0].AddImage(logoPath);
                    img.LockAspectRatio = true;
                    img.Height = Unit.FromCentimeter(ps.PageFormat == PageFormat.A3 ? 1.8 : 1.5);
                }
                catch { }
            }

            var p1 = r.Cells[1].AddParagraph(string.IsNullOrWhiteSpace(institusi) ? "INSTITUSI / JURUSAN" : institusi);
            p1.Style = "HeaderTitle";
            p1.Format.Alignment = ParagraphAlignment.Center;

            var p2 = r.Cells[1].AddParagraph(string.IsNullOrWhiteSpace(subTitle) ? "Jadwal Perkuliahan" : subTitle);
            p2.Style = "SubTitle";
            p2.Format.Alignment = ParagraphAlignment.Center;

            var sep = section.AddParagraph();
            sep.Format.SpaceBefore = Unit.FromPoint(6);
            sep.Format.SpaceAfter = Unit.FromPoint(8);
            sep.Format.Borders.Bottom.Width = 1;
        }

        private static void AddClassTitle(Section section, string className)
        {
            var judul = section.AddParagraph("KELAS: " + (string.IsNullOrWhiteSpace(className) ? "-" : className));
            judul.Format.SpaceBefore = Unit.FromPoint(6);
            judul.Format.SpaceAfter = Unit.FromPoint(6);
            judul.Format.Font.Bold = true;
            judul.Format.Font.Size = 10;
        }

        private static void AddFooter(Section section)
        {
            var footer = section.Footers.Primary.AddParagraph();
            footer.Format.Alignment = ParagraphAlignment.Center;
            footer.Format.SpaceBefore = Unit.FromPoint(5);
            footer.AddText("Halaman ");
            footer.AddPageField();
            footer.AddText(" dari ");
            footer.AddNumPagesField();
        }

        private static void BuildScheduleTableFromDT(Section section, DataTable dt)
        {
            if (dt == null || dt.Columns.Count == 0) return;

            string[] headers = { "Hari", "Sesi", "Jam", "Mata Kuliah", "Dosen", "Ruangan" };

            // A3/landscape lebar sedikit lebih longgar
            bool wide = (section.PageSetup.Orientation == Orientation.Landscape) || (section.PageSetup.PageFormat == PageFormat.A3);
            double[] widths = wide
                ? new[] { 3.2, 2.2, 4.2, 11.5, 7.2, 3.8 }
                : new[] { 2.5, 1.8, 3.5, 9.0, 6.0, 3.0 };

            var table = section.AddTable();
            table.Borders.Width = 0.5;
            table.Borders.Color = Colors.Gray;
            table.Format.Font.Size = 9;
            table.LeftPadding = Unit.FromPoint(2);
            table.RightPadding = Unit.FromPoint(2);
            table.TopPadding = Unit.FromPoint(1);
            table.BottomPadding = Unit.FromPoint(1);
            table.Format.SpaceBefore = Unit.FromCentimeter(0.35);
            table.Format.SpaceAfter = Unit.FromCentimeter(0.25);

            for (int i = 0; i < headers.Length; i++)
                table.AddColumn(Unit.FromCentimeter(widths[i]));

            var h = table.AddRow();
            h.HeadingFormat = true;
            h.Format.Font.Bold = true;
            h.Shading.Color = Colors.LightGray;
            for (int i = 0; i < headers.Length; i++)
                h.Cells[i].AddParagraph(headers[i]);

            var colMap = headers.ToDictionary(x => x, x => ResolveColumn(dt, x));
            foreach (DataRow dr in dt.Rows)
            {
                bool separator = true;
                for (int i = 0; i < headers.Length; i++)
                {
                    string cn = colMap[headers[i]];
                    string val = cn == null ? "" : (dr[cn] == DBNull.Value ? "" : Convert.ToString(dr[cn]));
                    if (!string.IsNullOrWhiteSpace(val)) { separator = false; break; }
                }

                var row = table.AddRow();
                if (separator)
                {
                    for (int i = 0; i < headers.Length; i++)
                        row.Cells[i].Shading.Color = Colors.LightGray;
                    continue;
                }

                for (int i = 0; i < headers.Length; i++)
                {
                    string cn = colMap[headers[i]];
                    string val = cn == null ? "" : (dr[cn] == DBNull.Value ? "" : Convert.ToString(dr[cn]));
                    row.Cells[i].AddParagraph(val ?? "");
                }
            }
        }

        // ===================== Utilities =====================

        private static string ResolveColumn(DataTable dt, string header)
        {
            if (dt.Columns.Contains(header)) return header;
            var alt = header.Replace(" ", "");
            if (dt.Columns.Contains(alt)) return alt;

            foreach (DataColumn c in dt.Columns)
            {
                if (string.Equals(c.ColumnName, header, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(c.ColumnName, alt, StringComparison.OrdinalIgnoreCase))
                    return c.ColumnName;
            }
            return null;
        }

        private static string ResolveCol(DataTable dt, string name)
        {
            if (dt.Columns.Contains(name)) return name;
            string alt = name.Replace(" ", "");
            if (dt.Columns.Contains(alt)) return alt;

            foreach (DataColumn c in dt.Columns)
            {
                if (string.Equals(c.ColumnName, name, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(c.ColumnName, alt, StringComparison.OrdinalIgnoreCase))
                    return c.ColumnName;
            }
            return null;
        }
    }

}
