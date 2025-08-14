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
    public partial class FormMatkul_TambahData : Form
    {
        public FormMatkul_TambahData()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
        }

        private int MatkulId = 0;

        public void EditMatkul(Matkul matkul)
        {
            this.Text = "Edit Matkul";
            this.lbltitleMK.Text = "Edit Matkul";

            this.tb_KodeMatkul.Text = matkul.kode_matkul.ToString();
            this.tb_KodeMatkul.Enabled = false;

            this.tb_NamaMatkulMK.Text = matkul.nama_matkul;
            this.tb_sks.Text = matkul.sks.ToString();
            this.cmbRuangan.Text = matkul.ruangan_preferensi;

            this.MatkulId = matkul.kode_matkul;
        }

        private void FormMatkul_TambahData_Load(object sender, EventArgs e)
        {
            LoadRuangan();
        }

        private void btnSimpanMK_Click(object sender, EventArgs e)
        {
            Matkul matkul = new Matkul();

            if (!int.TryParse(this.tb_KodeMatkul.Text, out int kode_matkul))
            {
                MessageBox.Show("Kode Matkul harus berupa angka.",
                                "Input Tidak Valid",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            matkul.kode_matkul = kode_matkul;
            matkul.nama_matkul = this.tb_NamaMatkulMK.Text;
            matkul.ruangan_preferensi = cmbRuangan.Text;

            if (int.TryParse(this.tb_sks.Text, out int sks))
            {
                matkul.sks = sks;

                var repo = new MatkulRepository();

                if (this.MatkulId == 0)
                {
                    if (repo.CekKodeSudahAda(kode_matkul))
                    {
                        MessageBox.Show("Kode Matkul sudah digunakan!");
                        return;
                    }

                    repo.CreateMatkul(matkul);
                    MessageBox.Show("Data berhasil ditambahkan!");
                }
                else
                {
                    repo.UpdateMatkul(matkul);
                    MessageBox.Show("Data berhasil diupdate!");
                }

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("SKS harus berupa angka.",
                                "Input Tidak Valid",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }

        private void LoadRuangan()
        {
            var ruanganRepo = new RuanganRepository();
            var ruanganList = ruanganRepo.GetAll();

            ruanganList.Insert(0, new Ruangan { kode_ruangan = 0, nama = "-- Silahkan Pilih Ruangan --" });

            cmbRuangan.DataSource = ruanganList;
            cmbRuangan.DisplayMember = "nama";
            cmbRuangan.ValueMember = "kode_ruangan";
            cmbRuangan.SelectedIndex = 0;
        }

        private void btnKembaliMK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void label1_Click(object sender, EventArgs e) { }

        private void tb_NamaMatkulMK_TextChanged(object sender, EventArgs e) { }
    }
}
