using System;
using System.Windows.Forms;
using Aplikasi_Jadwal_ELEKTRO.Models;
using Aplikasi_Jadwal_ELEKTRO.Repositories;

namespace Aplikasi_Jadwal_ELEKTRO
{
    public partial class FormRuangan_TambahData : Form
    {
        private int ruanganId = 0;
        private bool isEditMode = false;

        public FormRuangan_TambahData()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
        }

        public void EditRuangan(Ruangan ruangan)
        {
            this.Text = "Edit Data Ruangan";
            this.lbltitleRGN.Text = "EDIT DATA RUANGAN";

            this.tb_kode.Text = ruangan.kode_ruangan.ToString();
            this.tb_kode.Enabled = false;
            this.tb_nama.Text = ruangan.nama;

            if (!cmb_kategori.Items.Contains(ruangan.kategori))
                cmb_kategori.Items.Add(ruangan.kategori);
            cmb_kategori.SelectedItem = ruangan.kategori;

            this.ruanganId = ruangan.kode_ruangan;
            this.isEditMode = true;
        }

        private void btnSimpanRGN_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(this.tb_kode.Text.Trim(), out int kode))
            {
                MessageBox.Show("ID Ruangan harus berupa angka.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nama = this.tb_nama.Text.Trim();
            string kategori = cmb_kategori.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(nama) || string.IsNullOrEmpty(kategori))
            {
                MessageBox.Show("Nama dan Kategori Ruangan tidak boleh kosong.", "Input Tidak Valid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Ruangan ruangan = new Ruangan
            {
                kode_ruangan = kode,
                nama = nama,
                kategori = kategori
            };

            var repo = new RuanganRepository();

            if (isEditMode)
            {
                repo.UpdateRuangan(ruangan);
                MessageBox.Show("Data Ruangan berhasil diupdate!");
            }
            else
            {
                if (repo.CekKodeSudahAda(kode))
                {
                    MessageBox.Show("ID Ruangan sudah digunakan!", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                repo.CreateRuangan(ruangan);
                MessageBox.Show("Data Ruangan berhasil ditambahkan!");
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnKembaliRGN_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FormRuangan_TambahData_Load(object sender, EventArgs e)
        {
            cmb_kategori.Items.Clear();
            cmb_kategori.Items.Add("GKT");
            cmb_kategori.Items.Add("LAB");
            cmb_kategori.SelectedIndex = 0;
        }
    }
}
