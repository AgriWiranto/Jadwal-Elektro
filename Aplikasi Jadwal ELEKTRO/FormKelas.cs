using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aplikasi_Jadwal_ELEKTRO.Models;
using Aplikasi_Jadwal_ELEKTRO.Repositories;

namespace Aplikasi_Jadwal_ELEKTRO
{
    public partial class FormKelas : Form
    {
        public FormKelas()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            var repo = new KelasRepository();

            dgvKelas.DataSource = null; // ← bersihkan dahulu
            dgvKelas.DataSource = repo.GetAll();

            dgvKelas.Columns["id_kelas"].Visible = true;
            dgvKelas.Columns["id_kelas"].HeaderText = "Kode Kelas";
        }

        private void FormKelas_Load(object sender, EventArgs e)
        {
            LoadData(); // panggil method ini agar data langsung muncul saat form dibuka

        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            new FormKelas_TambahData().ShowDialog();
            LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvKelas.CurrentRow != null)
            {
                var selected = (Kelas)dgvKelas.CurrentRow.DataBoundItem;
                new FormKelas_TambahData(selected).ShowDialog();
                LoadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvKelas.CurrentRow != null)
            {
                var selected = (Kelas)dgvKelas.CurrentRow.DataBoundItem;
                var confirm = MessageBox.Show("Yakin ingin menghapus?", "Konfirmasi", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    new KelasRepository().Delete(selected.id_kelas);
                    LoadData();
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
