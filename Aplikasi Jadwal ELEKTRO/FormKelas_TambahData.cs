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
    public partial class FormKelas_TambahData : Form
    {
        private Kelas currentData;

        public FormKelas_TambahData()
        {
            InitializeComponent();
        }

        public FormKelas_TambahData(Kelas k) : this()
        {
            currentData = k;
        }

        private void FormKelas_TambahData_Load(object sender, EventArgs e)
        {
            if (currentData != null)
            {
                txtId.Text = currentData.id_kelas.ToString();
                txtId.Enabled = false; // Tidak bisa ubah ID saat edit
                txtNama.Text = currentData.namaKelas;
                txtAngkatan.Text = currentData.angkatan.ToString();
                txtTipe.Text = currentData.tipe_kelas;

                // Ganti judul form
                lblTitleKls.Text = "EDIT KELAS";
                btnSimpan.Text = "Update";
            }
            else
            {
                // Jika tambah data
                lblTitleKls.Text = "TAMBAH KELAS";
                btnSimpan.Text = "Simpan";
            }
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtId.Text, out int id))
            {
                MessageBox.Show("Kode Kelas harus berupa angka.");
                return;
            }

            if (!int.TryParse(txtAngkatan.Text, out int angkatan))
            {
                MessageBox.Show("Angkatan harus berupa angka.");
                return;
            }

            var kelas = new Kelas
            {
                id_kelas = id,
                namaKelas = txtNama.Text.Trim(),
                angkatan = angkatan,
                tipe_kelas = txtTipe.Text.Trim()
            };

            var repo = new KelasRepository();

            try
            {
                if (currentData == null) // mode tambah
                {
                    var existing = repo.GetAll().FirstOrDefault(k => k.id_kelas == id);
                    if (existing != null)
                    {
                        MessageBox.Show("Kode Kelas sudah digunakan. Silakan gunakan ID yang lain.");
                        return;
                    }

                    repo.Add(kelas);
                }
                else // mode edit
                {
                    repo.Update(kelas);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat menyimpan data: " + ex.Message);
            }
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
