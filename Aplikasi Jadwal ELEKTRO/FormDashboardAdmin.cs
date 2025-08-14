
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aplikasi_Jadwal_ELEKTRO.Constants;
using Aplikasi_Jadwal_ELEKTRO.Models;
using Aplikasi_Jadwal_ELEKTRO.Models.Genetic;
using Aplikasi_Jadwal_ELEKTRO.Repositories;
using Aplikasi_Jadwal_ELEKTRO.Repositories.Genetic;
using Aplikasi_Jadwal_ELEKTRO.Services;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using Aplikasi_Jadwal_ELEKTRO.Helpers;
using static Aplikasi_Jadwal_ELEKTRO.Helpers.ExcelTimetableTemplate;
using DrawingColor = System.Drawing.Color;




namespace Aplikasi_Jadwal_ELEKTRO
{
    public partial class FormDashboardAdmin : Form
    {
        private Dictionary<int, List<JadwalFinal>> hasilJadwalPerKelas = new Dictionary<int, List<JadwalFinal>>();

        // di dalam class FormDashboardAdmin
        private CancellationTokenSource _cts;


        public FormDashboardAdmin()
        {
            InitializeComponent();
        }

        private void FormDashboardAdmin_Load(object sender, EventArgs e)
        {
            LoadJadwalDariDatabase();

            // hide progress UI initially
            progressBar1.Visible = false;
            lblProgress.Visible = false;        // if you have a label
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Blocks; // or Continuous
        }


        private void SetBusyUI(bool isBusy, string msg = "")
        {
            btnGenerate.Enabled = !isBusy;
            btnCancel.Enabled = isBusy;
            Cursor = isBusy ? Cursors.WaitCursor : Cursors.Default;

            progressBar1.Visible = isBusy;
            lblProgress.Visible = isBusy;
            if (isBusy)
            {
                // reset when starting
                progressBar1.Style = ProgressBarStyle.Blocks; // switch to Marquee if you prefer
                progressBar1.Value = 0;
                lblProgress.Text = string.IsNullOrWhiteSpace(msg) ? "Menyiapkan..." : msg;
            }
            else
            {
                // clear when done
                progressBar1.Value = 0;
                lblProgress.Text = "";
            }
        }



        private async void button1_Click(object sender, EventArgs e)
        {
            // UI state
            btnGenerate.Enabled = false;
            btnCancel.Enabled = true;
            progressBar1.Value = 0;
            SetBusyUI(true, "Menyiapkan..."); // tampilkan progress bar
            Cursor = Cursors.WaitCursor;

            _cts = new CancellationTokenSource();

            try
            {
                // Ambil data referensi (cepat, di UI thread)
                var dosens = new DosenRepository().GetAll();
                var kelass = new KelasRepository().GetAll();
                var matkuls = new MatkulRepository().GetAll();
                var dosenMatkuls = new DosenMatkulRepository().GetAll();
                var kelasMatkuls = new KelasMatkulRepository().GetAll();
                var waktus = new WaktuRepository().GetAll();
                var ruangans = new RuanganRepository().GetAll();

                // Reporter progress GA (GaProgress -> ProgressBar + Label)
                var reporter = new Progress<GaProgress>(p =>
                {
                    var percent = Math.Max(progressBar1.Minimum, Math.Min(p.Percent, progressBar1.Maximum));
                    progressBar1.Value = Math.Max(progressBar1.Minimum,
                                         Math.Min(p.Percent, progressBar1.Maximum));
                    lblProgress.Text = p.Message ?? "";
                });

                List<JadwalFinal> hasilJadwal = null;
                List<Gene> gagalGenes = null;

                // Jalankan GA di thread pool
                await Task.Run(() =>
                {
                    _cts.Token.ThrowIfCancellationRequested();

                    var generator = new JadwalGenerator(
                        dosens, kelass, matkuls, dosenMatkuls, kelasMatkuls, waktus, ruangans
                    );

                    // <-- PENTING: pakai overload dengan progress + ct
                    var (jadwals, gagal) = generator.Generate(reporter, _cts.Token);
                    hasilJadwal = jadwals;
                    gagalGenes = gagal;

                    // Persist ke DB (laporkan sedikit progress manual)
                    ((IProgress<GaProgress>)reporter)?.Report(new GaProgress { Percent = 95, Message = "Menyimpan ke database..." });

                    var jadwalRepo = new JadwalKuliahRepository();
                    jadwalRepo.Reset();
                    foreach (var j in hasilJadwal)
                    {
                        _cts.Token.ThrowIfCancellationRequested();
                        jadwalRepo.Insert(j);
                    }

                    ((IProgress<GaProgress>)reporter)?.Report(new GaProgress { Percent = 98, Message = "Finalisasi..." });
                }, _cts.Token);

                // Selesai: 100%
                progressBar1.Value = 100;
                lblProgress.Text = "Selesai.";

                // Tampilkan ke tab
                LoadTemplateWaktuKeTab(kelass, dosens, matkuls, waktus, ruangans, hasilJadwal);

                // Peringatan gagal (kalau ada)
                if (gagalGenes != null && gagalGenes.Any())
                {
                    var pesan = "Beberapa jadwal tidak bisa dijadwalkan:\n\n" + string.Join("\n",
                        gagalGenes
                            .GroupBy(g => new { g.kode_dosen, g.kode_matkul, g.id_kelas })
                            .Select(g =>
                            {
                                var d = dosens.FirstOrDefault(x => x.kode_dosen == g.Key.kode_dosen)?.nama_dosen ?? "Dosen";
                                var m = matkuls.FirstOrDefault(x => x.kode_matkul == g.Key.kode_matkul)?.nama_matkul ?? "Matkul";
                                var k = kelass.FirstOrDefault(x => x.id_kelas == g.Key.id_kelas)?.namaKelas ?? $"Kelas {g.Key.id_kelas}";
                                return $"[!] {m} ({k}) – {d} gagal dijadwalkan.";
                            }));
                    MessageBox.Show(pesan, "Peringatan Jadwal Tidak Terjadwal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // Cek overload SKS (opsional)
                var matkulDict = matkuls.ToDictionary(m => m.kode_matkul);
                var sksDiampu = hasilJadwal
                    .GroupBy(j => j.kode_dosen)
                    .ToDictionary(g => g.Key, g => g.Sum(j => matkulDict.TryGetValue(j.kode_matkul, out var m) ? m.sks : 0));

                var dosenOverload = sksDiampu
                    .Where(p => p.Value > AppConstants.MAX_SKS_PER_DOSEN)
                    .Select(p => $"- {dosens.FirstOrDefault(d => d.kode_dosen == p.Key)?.nama_dosen ?? $"Dosen {p.Key}"} ({p.Value} SKS)")
                    .ToList();

                if (dosenOverload.Any())
                {
                    string pesan = $"Ditemukan dosen dengan beban mengajar melebihi {AppConstants.MAX_SKS_PER_DOSEN} SKS:\n\n" +
                                   string.Join("\n", dosenOverload);
                    MessageBox.Show(pesan, "Peringatan Beban Dosen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                MessageBox.Show("Jadwal berhasil digenerate dan ditampilkan!");
            }
            catch (OperationCanceledException)
            {
                lblProgress.Text = "Dibatalkan.";
                MessageBox.Show("Generate dibatalkan.", "Dibatalkan", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                lblProgress.Text = "Gagal.";
                MessageBox.Show("Terjadi error saat generate: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
                btnGenerate.Enabled = true;
                btnCancel.Enabled = false;
                SetBusyUI(false); // sembunyikan progress bar
                _cts?.Dispose();
                _cts = null;
            }
        }

        private void LoadJadwalDariDatabase()
        {
            var dosens = new DosenRepository().GetAll();
            var kelass = new KelasRepository().GetAll();
            var matkuls = new MatkulRepository().GetAll();
            var waktus = new WaktuRepository().GetAll();
            var ruangans = new RuanganRepository().GetAll();
            var hasilJadwal = new JadwalKuliahRepository().GetAll();

            LoadTemplateWaktuKeTab(kelass, dosens, matkuls, waktus, ruangans, hasilJadwal);
        }

        private int AmbilAngkaSesi(string sesi)
        {
            if (string.IsNullOrWhiteSpace(sesi)) return int.MaxValue;
            var angka = new string(sesi.Where(char.IsDigit).ToArray());
            return int.TryParse(angka, out int hasil) ? hasil : int.MaxValue;
        }

        private void LoadJadwalKeTab(
            List<Kelas> kelass,
            List<Dosen> dosens,
            List<Matkul> matkuls,
            List<Waktu> waktus,
            List<Ruangan> ruangans,
            List<JadwalFinal> hasilJadwal)
        {
            tabControlKelas.TabPages.Clear();

            var groupedByKelas = hasilJadwal
                .GroupBy(j => j.id_kelas)
                .OrderBy(g => g.Key)
                .ToDictionary(g => g.Key, g => g.ToList());

            foreach (var entry in groupedByKelas)
            {
                string namaKelas = kelass.FirstOrDefault(k => k.id_kelas == entry.Key)?.namaKelas ?? $"Kelas {entry.Key}";

                var tabPage = new TabPage(namaKelas);
                var dt = new DataTable();
                dt.Columns.Add("Hari");
                dt.Columns.Add("Sesi");
                dt.Columns.Add("Jam");
                dt.Columns.Add("MataKuliah");
                dt.Columns.Add("Dosen");
                dt.Columns.Add("Ruangan");

                string lastHari = "";

                var rows = entry.Value
                    .Select(j =>
                    {
                        var waktu = waktus.FirstOrDefault(w => w.id_waktu == j.id_waktu);
                        if (waktu == null || waktu.keterangan?.Trim().ToLower() == "istirahat") return null;

                        var matkul = matkuls.FirstOrDefault(m => m.kode_matkul == j.kode_matkul)?.nama_matkul;
                        var dosen = dosens.FirstOrDefault(d => d.kode_dosen == j.kode_dosen)?.nama_dosen;
                        var ruangan = ruangans.FirstOrDefault(r => r.kode_ruangan == j.kode_ruangan)?.nama;

                        return new
                        {
                            Hari = waktu.hari,
                            Sesi = waktu.sesi,
                            Jam = $"{waktu.jam_mulai:hh\\:mm} - {waktu.jam_selesai:hh\\:mm}",
                            MataKuliah = matkul,
                            Dosen = dosen,
                            Ruangan = ruangan
                        };
                    })
                    .Where(r => r != null)
                    .OrderBy(r => r.Hari)
                    .ThenBy(r => AmbilAngkaSesi(r.Sesi))
                    .ToList();

                foreach (var row in rows)
                {
                    if (row.Hari != lastHari && lastHari != "")
                        dt.Rows.Add("", "", "", "", "", "");

                    lastHari = row.Hari;
                    dt.Rows.Add(row.Hari, row.Sesi, row.Jam, row.MataKuliah, row.Dosen, row.Ruangan);
                }

                var dgv = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    DataSource = dt
                };

                dgv.RowPrePaint += (s, e) =>
                {
                    var grid = s as DataGridView;
                    var row = grid.Rows[e.RowIndex];

                    bool isKosong = row.Cells.Cast<DataGridViewCell>().All(c => string.IsNullOrWhiteSpace(c.Value?.ToString()));
                    if (isKosong)
                    {
                        row.DefaultCellStyle.BackColor = DrawingColor.LightGray;   // <--
                        return;
                    }

                    var mkCell = grid.Columns.Contains("MataKuliah") ? row.Cells["MataKuliah"] : null;
                    string matkul = mkCell?.Value?.ToString();

                    if (string.IsNullOrWhiteSpace(matkul))
                    {
                        row.DefaultCellStyle.BackColor = DrawingColor.LightCoral;  // <--
                        row.DefaultCellStyle.ForeColor = DrawingColor.Black;      // <--
                        row.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
                    }
                };

                tabPage.Controls.Add(dgv);
                tabControlKelas.TabPages.Add(tabPage);
            }
        }

        private void button_Click(object sender, EventArgs e) => MessageBox.Show("Simulasi simpan data.");

        private void btnUpdate_Click(object sender, EventArgs e) => MessageBox.Show("Simulasi update data.");

        private void DASHBOARD_Click(object sender, EventArgs e) { }

        private void btnCekKonsistensi_Click(object sender, EventArgs e) => CekKonsistensiJadwal();

        private void CekKonsistensiJadwal()
        {
            var repo = new JadwalKuliahRepository();
            var jadwals = repo.GetAll();

            var dosens = new DosenRepository().GetAll().Select(d => d.kode_dosen).ToHashSet();
            var matkuls = new MatkulRepository().GetAll().Select(m => m.kode_matkul).ToHashSet();
            var kelass = new KelasRepository().GetAll().Select(k => k.id_kelas).ToHashSet();
            var waktus = new WaktuRepository().GetAll().Select(w => w.id_waktu).ToHashSet();
            var ruangans = new RuanganRepository().GetAll().Select(r => r.kode_ruangan).ToHashSet();

            var errorList = jadwals
                .Where(j => !dosens.Contains(j.kode_dosen) || !matkuls.Contains(j.kode_matkul) ||
                            !kelass.Contains(j.id_kelas) || !waktus.Contains(j.id_waktu) ||
                            !ruangans.Contains(j.kode_ruangan))
                .Select(j => $"[!] Jadwal ID {j.id_jadwal} memiliki referensi tidak valid.")
                .ToList();

            MessageBox.Show(errorList.Count == 0 ? "Semua jadwal valid dan konsisten!" : string.Join("\n", errorList),
                "Cek Konsistensi", MessageBoxButtons.OK,
                errorList.Count == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
        }

        private void btnPerbaikiOtomatis_Click(object sender, EventArgs e) => PerbaikiOtomatis();

        private void PerbaikiOtomatis()
        {
            var repo = new JadwalKuliahRepository();
            var semuaJadwal = repo.GetAll();

            var dosens = new DosenRepository().GetAll().Select(d => d.kode_dosen).ToHashSet();
            var matkuls = new MatkulRepository().GetAll().Select(m => m.kode_matkul).ToHashSet();
            var kelass = new KelasRepository().GetAll().Select(k => k.id_kelas).ToHashSet();
            var waktus = new WaktuRepository().GetAll().Select(w => w.id_waktu).ToHashSet();
            var ruangans = new RuanganRepository().GetAll().Select(r => r.kode_ruangan).ToHashSet();

            var tidakValid = semuaJadwal.Where(j =>
                !dosens.Contains(j.kode_dosen) || !matkuls.Contains(j.kode_matkul) ||
                !kelass.Contains(j.id_kelas) || !waktus.Contains(j.id_waktu) ||
                !ruangans.Contains(j.kode_ruangan)).ToList();

            if (!tidakValid.Any())
            {
                MessageBox.Show("Tidak ada entri jadwal yang rusak.");
                return;
            }

            if (MessageBox.Show($"Ditemukan {tidakValid.Count} entri rusak. Hapus?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                tidakValid.ForEach(j => repo.Delete(j.id_jadwal));
                MessageBox.Show("Data rusak berhasil dihapus.");
                LoadJadwalDariDatabase();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _cts?.Cancel();
        }

        //Bagian Kodingan Eksport Data ke Excel dan PDF

        private Dictionary<string, DataTable> AmbilDataTiapKelasDariTabs()
        {
            var result = new Dictionary<string, DataTable>();

            foreach (TabPage tp in tabControlKelas.TabPages)
            {
                var kelas = tp.Text;
                var grid = tp.Controls.OfType<DataGridView>().FirstOrDefault();
                if (grid == null) continue;

                var dt = grid.DataSource as DataTable;
                if (dt == null)
                {
                    dt = new DataTable();
                    foreach (DataGridViewColumn col in grid.Columns)
                        dt.Columns.Add(col.HeaderText);

                    foreach (DataGridViewRow row in grid.Rows)
                    {
                        if (row.IsNewRow) continue;
                        dt.Rows.Add(row.Cells.Cast<DataGridViewCell>()
                            .Select(c => c.Value?.ToString() ?? "").ToArray());
                    }
                }

                result[kelas] = dt.Copy();
            }
            return result;
        }


        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                var dataTabs = AmbilDataTiapKelasDariTabs();
                if (dataTabs == null || dataTabs.Count == 0)
                {
                    MessageBox.Show("Tidak ada data di TabControl.");
                    return;
                }

                using (var sfd = new SaveFileDialog
                {
                    Filter = "Excel Workbook|*.xlsx",
                    FileName = "Jadwal_PerKelas.xlsx"
                })
                {
                    if (sfd.ShowDialog() != DialogResult.OK) return;

                    ExcelExportService.ExportPerKelasXlsx(
                        dataPerKelas: dataTabs,
                        filePath: sfd.FileName,
                        institusi: "JURUSAN TEKNIK ELEKTRO",
                        subTitle: "JADWAL PERKULIAHAN"
                    );
                }

                MessageBox.Show("Export Excel per kelas berhasil.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal export: " + ex.Message);
            }

        }

        private void btnExportPdf_Click(object sender, EventArgs e)
        {
            try
            {
                var kelass = new KelasRepository().GetAll();
                var waktus = new WaktuRepository().GetAll();
                var matkuls = new MatkulRepository().GetAll();
                var dosens = new DosenRepository().GetAll();
                var ruangans = new RuanganRepository().GetAll();
                var jadwal = new JadwalKuliahRepository().GetAll();

                var groups = KelasGroupingHelper.BuildGroups(kelass);
                if (groups.Count == 0) { MessageBox.Show("Tidak ada kelas."); return; }

                // Data dari TabControl (supaya sama persis dengan DGV)
                var dataTabs = AmbilDataTiapKelasDariTabs();

                using (var sfd = new SaveFileDialog
                {
                    Filter = "PDF|*.pdf",
                    FileName = "Jadwal_PerKelas_PerSemester.pdf"
                })
                {
                    if (sfd.ShowDialog() != DialogResult.OK) return;

                    string logo = Path.Combine(Application.StartupPath, "Assets", "logo.png");
                    if (!File.Exists(logo)) logo = null;

                    PdfExportService.ExportPerSemesterPerKelas(
                        groups: groups,
                        jadwal: jadwal,
                        waktus: waktus,
                        matkuls: matkuls,
                        dosens: dosens,
                        ruangans: ruangans,
                        logoPath: logo,
                        jurusanHeader: "JURUSAN TEKNIK ELEKTRO",
                        outputPath: sfd.FileName,
                        dataTabs: dataTabs,      // supaya sinkron dengan tampilan
                        landscape: true,
                        format: MigraDoc.DocumentObjectModel.PageFormat.A3   // pakai A3 biar lebar; ganti A4 bila perlu
                    );

                    MessageBox.Show("Export PDF per kelas per semester berhasil.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal export PDF: " + ex.Message);
            }
        }

        private void TampilkanWaktuKeTabs()
        {
            var kelasRepo = new KelasRepository();
            var waktuRepo = new WaktuRepository();

            var semuaKelas = kelasRepo.GetAll();
            var semuaWaktu = waktuRepo.GetAll();

            if (semuaWaktu.Count == 0)
            {
                MessageBox.Show("Belum ada data di menu Waktu. Silakan generate dulu di menu WAKTU.");
                return;
            }

            tabControlKelas.TabPages.Clear();

            // urutan hari rapi
            var dayOrder = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            { ["Senin"] = 1, ["Selasa"] = 2, ["Rabu"] = 3, ["Kamis"] = 4, ["Jumat"] = 5 };

            foreach (var kls in semuaKelas)
            {
                var waktuKelas = semuaWaktu
                    .Where(w => w.id_kelas == kls.id_kelas)
                    .OrderBy(w => dayOrder.TryGetValue(w.hari, out var idx) ? idx : 99)
                    .ThenBy(w => AmbilAngkaSesi(w.sesi))     // pakai helper yang sudah ada
                    .ToList();

                // list anonim untuk langsung di-bind ke DataGridView
                var rows = new List<dynamic>();
                string lastHari = null;

                foreach (var w in waktuKelas)
                {
                    // baris pemisah antar hari
                    if (lastHari != null && !string.Equals(lastHari, w.hari, StringComparison.OrdinalIgnoreCase))
                        rows.Add(new { Hari = "", Sesi = "", Jam = "", MataKuliah = "", Dosen = "", Ruangan = "", Keterangan = "" });

                    rows.Add(new
                    {
                        Hari = w.hari,
                        Sesi = w.sesi,
                        Jam = $"{w.jam_mulai:hh\\:mm} - {w.jam_selesai:hh\\:mm}",
                        MataKuliah = "",   // dikosongkan supaya kompatibel dengan exporter
                        Dosen = "",
                        Ruangan = "",
                        Keterangan = w.keterangan
                    });

                    lastHari = w.hari;
                }

                var dgv = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    DataSource = rows
                };

                // pewarnaan baris istirahat & pemisah
                dgv.DataBindingComplete += (s, e) =>
                {
                    foreach (DataGridViewRow r in dgv.Rows)
                    {
                        bool pemisah = string.IsNullOrWhiteSpace(Convert.ToString(r.Cells["Hari"].Value)) &&
                                       string.IsNullOrWhiteSpace(Convert.ToString(r.Cells["Sesi"].Value)) &&
                                       string.IsNullOrWhiteSpace(Convert.ToString(r.Cells["Jam"].Value));
                        if (pemisah)
                        {
                            r.DefaultCellStyle.BackColor = Color.LightGray;
                            continue;
                        }

                        var ket = Convert.ToString(r.Cells["Keterangan"].Value)?.Trim().ToLower();
                        if (ket == "istirahat")
                        {
                            r.DefaultCellStyle.BackColor = Color.Gainsboro;
                            r.DefaultCellStyle.ForeColor = Color.Black;
                            r.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
                        }
                    }
                };

                // (opsional) sembunyikan kolom Keterangan
                if (dgv.Columns["Keterangan"] != null)
                    dgv.Columns["Keterangan"].Visible = false;

                var tab = new TabPage(kls.namaKelas);
                tab.Controls.Add(dgv);
                tabControlKelas.TabPages.Add(tab);
            }
        }

        //Button Generate Waktu
        private void button1_Click_1(object sender, EventArgs e)
        {
            TampilkanWaktuKeTabs();
            MessageBox.Show("Waktu per kelas ditampilkan di tab.");
        }


        // Tampilkan kerangka waktu per kelas (jam & istirahat), tanpa jadwal.
        private void LoadTemplateWaktuKeTab(List<Kelas> kelass, List<Waktu> waktus, bool separatorAkhirHari = true)
        {
            tabControlKelas.TabPages.Clear();

            foreach (var kls in kelass.OrderBy(k => k.namaKelas))
            {
                var wKelas = waktus
                    .Where(w => w.id_kelas == kls.id_kelas)
                    .OrderBy(w => new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
                    { ["Senin"] = 1, ["Selasa"] = 2, ["Rabu"] = 3, ["Kamis"] = 4, ["Jumat"] = 5 }
        .TryGetValue(w.hari, out var idx) ? idx : 99)
                    .ThenBy(w => AmbilAngkaSesi(w.sesi))
                    .ToList();

                var dt = new DataTable();
                dt.Columns.Add("Hari");
                dt.Columns.Add("Sesi");
                dt.Columns.Add("Jam");
                dt.Columns.Add("MataKuliah");
                dt.Columns.Add("Dosen");
                dt.Columns.Add("Ruangan");
                dt.Columns.Add("Keterangan");

                string lastHari = null;
                foreach (var w in wKelas)
                {
                    // boleh pakai separatorAkhirHari di SINI
                    if (lastHari != null && lastHari != w.hari && separatorAkhirHari)
                        dt.Rows.Add("", "", "", "", "", "", "");

                    dt.Rows.Add(
                        w.hari,
                        w.sesi,
                        $"{w.jam_mulai:hh\\:mm} - {w.jam_selesai:hh\\:mm}",
                        "", "", "",
                        w.keterangan
                    );

                    lastHari = w.hari;
                }

                var dgv = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    DataSource = dt
                };

                dgv.DataBindingComplete += (s, e) =>
                {
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        var ket = row.Cells["Keterangan"].Value?.ToString()?.Trim().ToLower();
                        bool separator = row.Cells.Cast<DataGridViewCell>().All(c => string.IsNullOrWhiteSpace(c.Value?.ToString()));
                        if (separator)
                        {
                            row.DefaultCellStyle.BackColor = Color.LightGray;
                        }
                        else if (ket == "istirahat")
                        {
                            row.DefaultCellStyle.BackColor = Color.Gainsboro;
                            row.DefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Italic);
                        }
                    }
                };

                if (dgv.Columns["Keterangan"] != null)
                    dgv.Columns["Keterangan"].Visible = false;

                var page = new TabPage(kls.namaKelas);
                page.Controls.Add(dgv);
                tabControlKelas.TabPages.Add(page);
            }
        }

        // 2) WRAPPER — panggil template-only, lalu isi hasil jadwal
        private void LoadTemplateWaktuKeTab(
            List<Kelas> kelass,
            List<Dosen> dosens,
            List<Matkul> matkuls,
            List<Waktu> waktus,
            List<Ruangan> ruangans,
            List<JadwalFinal> hasilJadwal)
        {
            // render kerangka waktu
            LoadTemplateWaktuKeTab(kelass, waktus, separatorAkhirHari: true);

            // isi hasil generate (jika ada)
            if (hasilJadwal != null && hasilJadwal.Count > 0)
            {
                FillJadwalKeTemplateTabs(
                    hasil: hasilJadwal,
                    kelass: kelass,
                    matkuls: matkuls,
                    dosens: dosens,
                    waktus: waktus,
                    ruangans: ruangans
                );
            }
        }

        private void FillJadwalKeTemplateTabs(
    List<JadwalFinal> hasil,
    List<Kelas> kelass,
    List<Matkul> matkuls,
    List<Dosen> dosens,
    List<Waktu> waktus,
    List<Ruangan> ruangans)
        {
            var mBy = matkuls.ToDictionary(m => m.kode_matkul);
            var dBy = dosens.ToDictionary(d => d.kode_dosen);
            var rBy = ruangans.ToDictionary(r => r.kode_ruangan);
            var jamById = waktus.ToDictionary(w => w.id_waktu, w => $"{w.jam_mulai:hh\\:mm} - {w.jam_selesai:hh\\:mm}");

            foreach (TabPage tp in tabControlKelas.TabPages)
            {
                var k = kelass.FirstOrDefault(x => x.namaKelas == tp.Text);
                if (k == null) continue;

                var dgv = tp.Controls.OfType<DataGridView>().FirstOrDefault();
                var dt = dgv?.DataSource as DataTable;
                if (dt == null) continue;

                var barisKelas = hasil.Where(h => h.id_kelas == k.id_kelas).ToList();

                foreach (var j in barisKelas)
                {
                    var w = waktus.FirstOrDefault(x => x.id_waktu == j.id_waktu);
                    if (w == null) continue;

                    var jamLbl = jamById[j.id_waktu];
                    // cari baris template yang match Hari + Jam (baris "istirahat" & baris kosong otomatis terlewati)
                    var rows = dt.AsEnumerable().Where(r =>
                        string.Equals(Convert.ToString(r["Hari"]).Trim(), w.hari, StringComparison.OrdinalIgnoreCase) &&
                        string.Equals(Convert.ToString(r["Jam"]).Trim(), jamLbl, StringComparison.Ordinal));

                    foreach (var r in rows)
                    {
                        r["MataKuliah"] = mBy.TryGetValue(j.kode_matkul, out var m) ? m.nama_matkul : "";
                        r["Dosen"] = dBy.TryGetValue(j.kode_dosen, out var d) ? d.nama_dosen : "";
                        r["Ruangan"] = rBy.TryGetValue(j.kode_ruangan, out var rg) ? rg.nama : "";
                    }
                }

                dgv?.Refresh();
            }
        }



    }
}
