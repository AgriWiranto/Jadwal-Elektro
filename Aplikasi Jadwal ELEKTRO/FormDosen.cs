using System;
using System.Data;
using System.Windows.Forms;
using Aplikasi_Jadwal_ELEKTRO.Models;
using Aplikasi_Jadwal_ELEKTRO.Repositories;

namespace Aplikasi_Jadwal_ELEKTRO
{
    public partial class FormDosen : Form
    {
        public FormDosen()
        {
            InitializeComponent();
        }

        private void FormDosen_Load(object sender, EventArgs e)
        {
            LoadDosens();
            DataGridDosen.SelectionChanged += DataGridDosen_SelectionChanged;
            btn_editDosen.Enabled = false;
            btn_deleteDosen.Enabled = false;
        }

        private void LoadDosens()
        {
            try
            {
                var repo = new DosenRepository();
                var dosens = repo.GetDosens();

                if (dosens == null || dosens.Count == 0)
                {
                    DataGridDosen.DataSource = null;
                    return;
                }

                DataTable dt = new DataTable();
                dt.Columns.Add("KODE DOSEN");
                dt.Columns.Add("NAMA DOSEN");
                dt.Columns.Add("NIP");

                foreach (var dosen in dosens)
                {
                    dt.Rows.Add(dosen.kode_dosen, dosen.nama_dosen, dosen.nip);
                }

                DataGridDosen.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data dosen: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_tambahdataDosen_Click(object sender, EventArgs e)
        {
            FormDosen_TambahData form = new FormDosen_TambahData();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadDosens();
            }
        }

        private void Btn_editDosen_Click(object sender, EventArgs e)
        {
            if (DataGridDosen.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data dosen terlebih dahulu.");
                return;
            }

            string val = DataGridDosen.SelectedRows[0].Cells[0].Value?.ToString();
            if (string.IsNullOrEmpty(val) || !int.TryParse(val, out int dosenId)) return;

            var repo = new DosenRepository();
            var dosen = repo.GetDosen(dosenId);

            if (dosen == null) return;

            FormDosen_TambahData form = new FormDosen_TambahData();
            form.EditDosen(dosen);
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                LoadDosens();
            }
        }

        private void Btn_deleteDosen_Click(object sender, EventArgs e)
        {
            if (DataGridDosen.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data dosen terlebih dahulu.");
                return;
            }

            string val = DataGridDosen.SelectedRows[0].Cells[0].Value?.ToString();
            if (string.IsNullOrEmpty(val) || !int.TryParse(val, out int dosenId)) return;

            DialogResult dialogResult = MessageBox.Show(
                "Apakah Anda yakin ingin menghapus data dosen ini?",
                "Hapus Dosen",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                var repo = new DosenRepository();
                repo.DeleteDosen(dosenId);
                LoadDosens();
                MessageBox.Show("Data dosen berhasil dihapus.");
            }
        }

        private void BtnRefreshDosen_Click(object sender, EventArgs e)
        {
            LoadDosens();
        }

        private void DataGridDosen_SelectionChanged(object sender, EventArgs e)
        {
            bool selected = DataGridDosen.SelectedRows.Count > 0;
            btn_editDosen.Enabled = selected;
            btn_deleteDosen.Enabled = selected;
        }

        private void DataGridDosen_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void Btn_dosenmatakuliah_Click_1(object sender, EventArgs e) { }
        private void btn_Refreshdosenmatkul_Click(object sender, EventArgs e) { }
        private void Btn_editdosenmatkul_Click(object sender, EventArgs e) { }
        private void Label1_Click(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            if (DataGridDosen.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data dosen terlebih dahulu.");
                return;
            }

            // Placeholder: tambahkan fitur lihat matkul jika diperlukan
        }
    }
}
