using System;
using System.Windows.Forms;
using Aplikasi_Jadwal_ELEKTRO.Models;
using Aplikasi_Jadwal_ELEKTRO.Repositories;

namespace Aplikasi_Jadwal_ELEKTRO
{
    public partial class FormDosen_TambahData : Form
    {
        private bool isEditMode = false;
        private int currentDosenId = 0;

        public FormDosen_TambahData()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
        }

        private void FormDosen_TambahData_Load(object sender, EventArgs e)
        {
            // Kosong
        }

        public void EditDosen(Dosen dosen)
        {
            this.Text = "Edit Dosen";
            this.lbltitle.Text = "EDIT DATA DOSEN";

            this.tb_kodedosen.Text = dosen.kode_dosen.ToString();
            this.tb_kodedosen.Enabled = false;

            this.tb_NamaDosen.Text = dosen.nama_dosen;
            this.nip.Text = dosen.nip;

            this.isEditMode = true;
            this.currentDosenId = dosen.kode_dosen;
        }

        private void btnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                var repo = new DosenRepository();
                var dosen = new Dosen
                {
                    kode_dosen = int.Parse(tb_kodedosen.Text),
                    nama_dosen = tb_NamaDosen.Text,
                    nip = nip.Text,
                };

                if (isEditMode)
                {
                    repo.UpdateDosen(dosen);
                    MessageBox.Show("Data dosen berhasil diperbarui.");
                }
                else
                {
                    if (repo.CekIdSudahAda(dosen.kode_dosen))
                    {
                        MessageBox.Show("Kode dosen sudah ada, gunakan kode lain.");
                        return;
                    }

                    if (!repo.CreateDosen(dosen))
                    {
                        MessageBox.Show("Gagal menambahkan dosen baru.");
                        return;
                    }

                    MessageBox.Show("Data dosen berhasil disimpan.");
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat menyimpan data: " + ex.Message);
            }
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
