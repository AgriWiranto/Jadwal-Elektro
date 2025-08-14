using System;
using System.Windows.Forms;
using Aplikasi_Jadwal_ELEKTRO.Models;
using Aplikasi_Jadwal_ELEKTRO.Repositories;

namespace Aplikasi_Jadwal_ELEKTRO
{
    public partial class FormWaktu_TambahData : Form
    {
        public FormWaktu_TambahData()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
        }

        private void FormWaktu_TambahData_Load(object sender, EventArgs e)
        {
            cbTipeKelas.Items.Add("Pagi");
            cbTipeKelas.Items.Add("Malam");
            cbTipeKelas.SelectedIndex = 0;

            tbJamMulai.Format = DateTimePickerFormat.Custom;
            tbJamMulai.CustomFormat = "HH:mm";
            tbJamMulai.ShowUpDown = true;

            tbJamSelesai.Format = DateTimePickerFormat.Custom;
            tbJamSelesai.CustomFormat = "HH:mm";
            tbJamSelesai.ShowUpDown = true;
            tbJamSelesai.Enabled = false;
        }

        private void cbTipeKelas_SelectedIndexChanged(object sender, EventArgs e) => HitungJamSelesai();
        private void tbJamMulai_ValueChanged(object sender, EventArgs e) => HitungJamSelesai();
        private void tbDurasiPerSKS_TextChanged(object sender, EventArgs e) => HitungJamSelesai();

        private void HitungJamSelesai()
        {
            if (!int.TryParse(tbDurasiPerSKS.Text, out int durasi)) return;

            string tipe = cbTipeKelas.SelectedItem.ToString();
            int jumlahIstirahat = (tipe == "Pagi") ? 2 : 1;

            TimeSpan jamMulai = tbJamMulai.Value.TimeOfDay;
            TimeSpan total = TimeSpan.FromMinutes((8 * durasi) + (jumlahIstirahat * 30));
            tbJamSelesai.Value = DateTime.Today.Add(jamMulai.Add(total));
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (cbTipeKelas.SelectedItem == null || string.IsNullOrWhiteSpace(tbDurasiPerSKS.Text) || string.IsNullOrWhiteSpace(tbIstirahatSesi.Text))
            {
                MessageBox.Show("Harap lengkapi semua data.");
                return;
            }

            if (!int.TryParse(tbDurasiPerSKS.Text, out int durasi) || durasi <= 0)
            {
                MessageBox.Show("Durasi SKS harus angka positif.");
                return;
            }

            WaktuSlotConfig config = new WaktuSlotConfig
            {
                tipe_kelas = cbTipeKelas.SelectedItem.ToString(),
                jam_mulai = tbJamMulai.Value.TimeOfDay,
                jam_selesai = tbJamSelesai.Value.TimeOfDay,
                durasi_per_sks = durasi,
                istirahat_setelah_sesi = tbIstirahatSesi.Text.Trim()
            };

            new WaktuSlotConfigRepository().Insert(config);

            MessageBox.Show("Konfigurasi berhasil disimpan.");
            this.Close();
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
