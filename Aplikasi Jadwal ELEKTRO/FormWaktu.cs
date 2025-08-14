
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aplikasi_Jadwal_ELEKTRO.Models;
using Aplikasi_Jadwal_ELEKTRO.Repositories;
using static System.Net.Mime.MediaTypeNames;

namespace Aplikasi_Jadwal_ELEKTRO
{
    public partial class FormWaktu : Form
    {


        public FormWaktu()
        {
            InitializeComponent();
        }

        private void FormWaktu_Load(object sender, EventArgs e)
        {
            if (new WaktuRepository().GetAll().Count == 0)
                LoadConfigView();
            else
                LoadData();
        }

        private void LoadData()
        {
            var repo = new WaktuRepository();
            var data = repo.GetAll();

            dgvWaktu.DataSource = data;

            if (dgvWaktu.Columns["jam_mulai"] != null)
                dgvWaktu.Columns["jam_mulai"].DefaultCellStyle.Format = @"hh\:mm";

            if (dgvWaktu.Columns["jam_selesai"] != null)
                dgvWaktu.Columns["jam_selesai"].DefaultCellStyle.Format = @"hh\:mm";

            // Tambahkan ini
            TampilkanWaktuPerKelas();

            if (data.Count > 0)
            {
                dgvWaktu.Visible = false;
                tabControlKelas.Visible = true;
            }
            else
            {
                dgvWaktu.Visible = true;
                tabControlKelas.Visible = false;
            }

            SetStatusLabel("Waktu Per Kelas");


        }

        private void LoadConfigView()
        {
            var configRepo = new WaktuSlotConfigRepository();
            var configs = configRepo.GetAll();

            var tampilData = configs.Select(c => new
            {
                Tipe_Kelas = c.tipe_kelas,
                Jam_Mulai = c.jam_mulai.ToString(@"hh\:mm"),
                Jam_Selesai = c.jam_selesai.ToString(@"hh\:mm"),
                Durasi = c.durasi_per_sks,
                Istirahat = c.istirahat_setelah_sesi
            }).ToList();

            dgvWaktu.DataSource = tampilData;

            // ✅ Tambahkan ini agar kembali tampil ke mode konfigurasi awal
            dgvWaktu.Visible = true;
            tabControlKelas.Visible = false;
            SetStatusLabel("Konfigurasi Awal");

        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            var form = new FormWaktu_TambahData();
            form.ShowDialog();

            if (new WaktuRepository().GetAll().Count == 0)
                LoadConfigView();
            else
                LoadData();
        }

        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Yakin ingin generate ulang waktu per kelas?\nSemua data lama akan dihapus.",
                                "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                var waktuRepo = new WaktuRepository();
                var kelasRepo = new KelasRepository();
                var configRepo = new WaktuSlotConfigRepository();

                waktuRepo.HapusSemua();

                var semuaKelas = kelasRepo.GetAll();
                var configs = configRepo.GetAll();

                int totalKelas = semuaKelas.Count;
                progressBar1.Maximum = totalKelas;
                progressBar1.Value = 0;
                progressBar1.Visible = true;
                btnGenerate.Enabled = false;
                SetStatusLabel("⏳ Memproses slot waktu...");

                var progress = new Progress<int>(value =>
                {
                    progressBar1.Value = value;
                });

                await Task.Run(() =>
                {
                    List<Waktu> semuaWaktu = new List<Waktu>();
                    string[] hariKuliah = { "Senin", "Selasa", "Rabu", "Kamis", "Jumat" };

                    int kelasKe = 0;

                    foreach (var kelas in semuaKelas)
                    {
                        var config = configs.FirstOrDefault(c =>
                            c.tipe_kelas.Trim().ToLower() == kelas.tipe_kelas.Trim().ToLower());

                        if (config == null) continue;

                        List<int> istirahatSetelah = config.istirahat_setelah_sesi
                            .Split(',')
                            .Select(s => int.TryParse(s.Trim(), out int x) ? x : -1)
                            .Where(x => x != -1)
                            .ToList();

                        TimeSpan durasiSKS = TimeSpan.FromMinutes(config.durasi_per_sks);
                        TimeSpan durasiIstirahat = TimeSpan.FromMinutes(30);

                        foreach (string hari in hariKuliah)
                        {
                            TimeSpan waktuSekarang = config.jam_mulai;
                            int sesiCounter = 1;
                            int totalSlot = 0;

                            while (totalSlot < 10)
                            {
                                // ⏸️ Sesi Istirahat (tetap diberi kode sesi P3, P6, dst)
                                if (istirahatSetelah.Contains(sesiCounter))
                                {
                                    string kodeSesi = kelas.tipe_kelas.ToLower() == "pagi"
                                        ? $"P{sesiCounter}"
                                        : $"M{sesiCounter}";

                                    semuaWaktu.Add(new Waktu
                                    {
                                        id_kelas = kelas.id_kelas,
                                        hari = hari,
                                        tipe_kelas = kelas.tipe_kelas,
                                        sesi = kodeSesi,
                                        jam_mulai = waktuSekarang,
                                        jam_selesai = waktuSekarang + durasiIstirahat,
                                        keterangan = "Istirahat"
                                    });

                                    waktuSekarang += durasiIstirahat;
                                    sesiCounter++;
                                    totalSlot++;
                                }

                                if (totalSlot >= 10) break;

                                string sesiKuliah = kelas.tipe_kelas.ToLower() == "pagi"
                                    ? $"P{sesiCounter}"
                                    : $"M{sesiCounter}";

                                semuaWaktu.Add(new Waktu
                                {
                                    id_kelas = kelas.id_kelas,
                                    hari = hari,
                                    tipe_kelas = kelas.tipe_kelas,
                                    sesi = sesiKuliah,
                                    jam_mulai = waktuSekarang,
                                    jam_selesai = waktuSekarang + durasiSKS,
                                    keterangan = "Kuliah"
                                });

                                waktuSekarang += durasiSKS;
                                sesiCounter++;
                                totalSlot++;
                            }
                        }

                        kelasKe++;
                        ((IProgress<int>)progress).Report(kelasKe);
                    }

                    waktuRepo.InsertBatch(semuaWaktu);
                });

                progressBar1.Visible = false;
                btnGenerate.Enabled = true;
                LoadData();
                MessageBox.Show("Slot waktu berhasil digenerate per kelas.");
                SetStatusLabel("TAMPILAN: Per Kelas");
            }
        }




        private void btnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Yakin ingin reset semua data waktu?", "Konfirmasi", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                new WaktuRepository().HapusSemua(); // hapus isi tabel Waktu
                LoadConfigView();                   // tampilkan data konfigurasi
                MessageBox.Show("Data waktu berhasil di-reset. Menampilkan konfigurasi awal.");
            }

            SetStatusLabel("Konfigurasi Awal");


        }


        private void btnHapusConfig_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Yakin ingin menghapus semua konfigurasi waktu?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                new WaktuSlotConfigRepository().HapusSemua();
                LoadConfigView();
                MessageBox.Show("Semua konfigurasi berhasil dihapus.");
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void TampilkanWaktuPerKelas()
        {
            tabControlKelas.TabPages.Clear();

            var kelasRepo = new KelasRepository();
            var waktuRepo = new WaktuRepository();

            var semuaKelas = kelasRepo.GetAll();
            var semuaWaktu = waktuRepo.GetAll();

            foreach (var kelas in semuaKelas)
            {
                var waktuKelas = semuaWaktu
                    .Where(w => w.id_kelas == kelas.id_kelas)
                    .OrderBy(w => w.hari)
                    .ThenBy(w => w.jam_mulai)
                    .ToList();

                // Buat list tampilan dengan pemisah antar hari
                var tampilanList = new List<dynamic>();
                string lastHari = null;

                foreach (var waktu in waktuKelas)
                {
                    if (lastHari != null && lastHari != waktu.hari)
                    {
                        // Tambahkan baris kosong sebagai pemisah hari
                        tampilanList.Add(new
                        {
                            Hari = "",
                            Sesi = "",
                            Tipe = "",
                            JamMulai = "",
                            JamSelesai = "",
                            Keterangan = ""
                        });
                    }

                    tampilanList.Add(new
                    {
                        Hari = waktu.hari,
                        Sesi = waktu.sesi,
                        Tipe = waktu.tipe_kelas,
                        JamMulai = waktu.keterangan?.ToLower() == "istirahat"
                                    ? waktu.jam_mulai.ToString(@"hh\:mm")
                                    : (waktu.jam_mulai != TimeSpan.MinValue ? waktu.jam_mulai.ToString(@"hh\:mm") : ""),
                        JamSelesai = waktu.keterangan?.ToLower() == "istirahat"
                                    ? waktu.jam_selesai.ToString(@"hh\:mm")
                                    : (waktu.jam_selesai != TimeSpan.MinValue ? waktu.jam_selesai.ToString(@"hh\:mm") : ""),
                        Keterangan = waktu.keterangan
                    });

                    lastHari = waktu.hari;
                }

                var dgv = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    DataSource = tampilanList
                };

                // 🎨 Pewarnaan baris istirahat dan baris kosong
                dgv.DataBindingComplete += (s, e) =>
                {
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        string keterangan = row.Cells["Keterangan"].Value?.ToString().Trim().ToLower();

                        if (keterangan == "istirahat")
                        {
                            row.DefaultCellStyle.BackColor = Color.LightGray;
                            row.DefaultCellStyle.ForeColor = Color.Black;
                            row.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
                        }
                        else if (string.IsNullOrWhiteSpace(keterangan))
                        {
                            row.DefaultCellStyle.BackColor = Color.LightBlue;
                        }
                    }
                };

                var tabPage = new TabPage(kelas.namaKelas);
                tabPage.Controls.Add(dgv);
                tabControlKelas.TabPages.Add(tabPage);
            }
        }



        private void SetStatusLabel(string teks)
        {
            lblStatus.Text = $"TAMPILAN: {teks}";
            lblStatus.Text = teks;
            lblStatus.Visible = true;
        }


    }
}
