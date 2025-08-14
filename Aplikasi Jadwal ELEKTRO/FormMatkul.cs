using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Aplikasi_Jadwal_ELEKTRO.Models;
using Aplikasi_Jadwal_ELEKTRO.Repositories;

namespace Aplikasi_Jadwal_ELEKTRO
{
    public partial class FormMatkul : Form
    {
        public FormMatkul()
        {
            InitializeComponent();
        }

        private void FormMatkul_Load(object sender, EventArgs e)
        {
            ReadMatkuls();
            btn_editMatkul.Enabled = false;
            btn_deleteMatkul.Enabled = false;
            MatkulTabel.SelectionChanged += MatkulTabel_SelectionChanged;
        }

        private void ReadMatkuls()
        {
            var repo = new MatkulRepository();
            var matkulList = repo.GetAll(); // <- ini karena GetAll() menyertakan ruangan_preferensi

            DataTable dt = new DataTable();
            dt.Columns.Add("Kode Matkul");
            dt.Columns.Add("Nama Matkul");
            dt.Columns.Add("SKS");
            dt.Columns.Add("Ruangan");

            foreach (var matkul in matkulList)
            {
                DataRow row = dt.NewRow();
                row["Kode Matkul"] = matkul.kode_matkul;
                row["Nama Matkul"] = matkul.nama_matkul;
                row["SKS"] = matkul.sks;
                row["Ruangan"] = matkul.ruangan_preferensi ?? "";
                dt.Rows.Add(row);
            }

            MatkulTabel.DataSource = dt;
        }

        private void RefreshMatkulTable()
        {
            ReadMatkuls();
        }

        private void btnRefreshMK_Click(object sender, EventArgs e)
        {
            RefreshMatkulTable();
        }

        private void btn_tambahdataMatkul_Click(object sender, EventArgs e)
        {
            FormMatkul_TambahData form = new FormMatkul_TambahData();
            if (form.ShowDialog() == DialogResult.OK)
            {
                RefreshMatkulTable();
            }
        }

        private void btn_editMatkul_Click(object sender, EventArgs e)
        {
            if (MatkulTabel.SelectedRows.Count == 0) return;

            // Kolom 0 adalah "Kode Matkul"
            string val = MatkulTabel.SelectedRows[0].Cells[0].Value?.ToString();
            if (!int.TryParse(val, out int matkulId)) return;

            var repo = new MatkulRepository();
            var matkul = repo.GetMatkul(matkulId);
            if (matkul == null) return;

            FormMatkul_TambahData form = new FormMatkul_TambahData();
            form.EditMatkul(matkul);
            if (form.ShowDialog() == DialogResult.OK)
            {
                RefreshMatkulTable();
            }
        }

        private void btn_deleteMatkul_Click(object sender, EventArgs e)
        {
            if (MatkulTabel.SelectedRows.Count == 0) return;

            string val = MatkulTabel.SelectedRows[0].Cells[0].Value?.ToString();
            if (!int.TryParse(val, out int matkulId)) return;

            // Hitung relasi yang akan terhapus
            int countKelasMatkul = CountRelasi("KelasMatkul", "kode_matkul", matkulId);
            int countDosenMatkul = CountRelasi("DosenMatkul", "kode_matkul", matkulId);
            int countJadwalFinal = CountRelasi("JadwalFinal", "kode_matkul", matkulId);

            string pesan = $"Mata kuliah ini terhubung dengan:\n\n" +
                           $"- {countKelasMatkul} data di KelasMatkul\n" +
                           $"- {countDosenMatkul} data di DosenMatkul\n" +
                           $"- {countJadwalFinal} data di JadwalFinal\n\n" +
                           $"Apakah kamu yakin ingin menghapus semuanya?";

            DialogResult result = MessageBox.Show(pesan, "Konfirmasi Penghapusan", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                var repo = new MatkulRepository();
                repo.DeleteMatkul(matkulId);
                MessageBox.Show("Data berhasil dihapus.");
                RefreshMatkulTable();
            }
        }

        private int CountRelasi(string tableName, string kolom, int matkulId)
        {
            int count = 0;
            string connectionString = @"Data Source=LAPTOP-8SSGNIP4\MSSQLSERVER01;Initial Catalog=Elektro_db;Trusted_Connection=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = $"SELECT COUNT(*) FROM {tableName} WHERE {kolom} = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", matkulId);
                count = (int)cmd.ExecuteScalar();
            }

            return count;
        }


        private void MatkulTabel_SelectionChanged(object sender, EventArgs e)
        {
            bool selected = MatkulTabel.SelectedRows.Count > 0;
            btn_editMatkul.Enabled = selected;
            btn_deleteMatkul.Enabled = selected;
        }

        private void label1_Click(object sender, EventArgs e) { }

        private void MatkulTabel_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}
