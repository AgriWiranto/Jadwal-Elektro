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
    public partial class FormDosenMatkul : Form
    {
        public FormDosenMatkul()
        {
            InitializeComponent();
        }

        private void FormDosenMatkul_Load(object sender, EventArgs e)
        {
            var dosenRepo = new DosenRepository();
            var listDosen = dosenRepo.GetAll();

            // Tambahkan placeholder
            listDosen.Insert(0, new Dosen
            {
                kode_dosen = -1,
                nama_dosen = "-- Pilih Dosen Terlebih Dahulu --"
            });

            cbDosen.DataSource = listDosen;
            cbDosen.DisplayMember = "nama_dosen";
            cbDosen.ValueMember = "kode_dosen";
            cbDosen.SelectedIndex = 0;

            var matkulRepo = new MatkulRepository();
            var listMatkul = matkulRepo.GetAll();

            listMatkul.Insert(0, new Matkul
            {
                kode_matkul = -1,
                nama_matkul = "-- Pilih Mata Kuliah Terlebih Dahulu --"
            });

            cbMatkul.DataSource = listMatkul;
            cbMatkul.DisplayMember = "nama_matkul";
            cbMatkul.ValueMember = "kode_matkul";
            cbMatkul.SelectedIndex = 0;
          

        }

        private void cbDosen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDosen.SelectedValue is int kode_dosen)
            {
                var repo = new DosenMatkulRepository();
                dgvMatkulDosen.DataSource = repo.GetByDosen(kode_dosen);
                dgvMatkulDosen.Columns["id"].Visible = false;
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            int selectedDosen = (int)cbDosen.SelectedValue;
            int selectedMatkul = (int)cbMatkul.SelectedValue;

            if (selectedDosen == -1 || selectedMatkul == -1)
            {
                MessageBox.Show("Silakan pilih dosen dan mata kuliah yang valid.");
                return;
            }

            var dm = new DosenMatkul
            {
                kode_dosen = selectedDosen,
                kode_matkul = selectedMatkul
            };

            var repo = new DosenMatkulRepository();
            repo.Add(dm);

            dgvMatkulDosen.DataSource = repo.GetByDosen(dm.kode_dosen);
            dgvMatkulDosen.Columns["id"].Visible = false;

            //Simpan Log
            var logRepo = new LogDosenMatkulRepository();
            logRepo.AddLog(dm.kode_dosen, dm.kode_matkul, "tambah");
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvMatkulDosen.CurrentRow == null)
            {
                MessageBox.Show("Silakan pilih mata kuliah yang ingin dihapus.");
                return;
            }

            // Ambil DosenMatkul dari DataBoundItem, bukan dari Cell langsung
            var selectedRow = dgvMatkulDosen.CurrentRow?.DataBoundItem as DosenMatkul;
            if (selectedRow == null)
            {
                MessageBox.Show("Baris tidak valid.");
                return;
            }

            var confirm = MessageBox.Show("Hapus mata kuliah ini?", "Konfirmasi", MessageBoxButtons.YesNo);
            if (confirm != DialogResult.Yes) return;

            // Hapus dari DB
            var repo = new DosenMatkulRepository();
            repo.Delete(selectedRow.id);

            // Simpan log
            var logRepo = new LogDosenMatkulRepository();
            logRepo.AddLog(selectedRow.kode_dosen, selectedRow.kode_matkul, "hapus");

            // Refresh DataGridView
            dgvMatkulDosen.DataSource = repo.GetByDosen(selectedRow.kode_dosen);
            dgvMatkulDosen.Columns["id"].Visible = false;
        }

        private void cbMatkul_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbMatkul.SelectedValue is int val && val != -1)
                lblKodeMatkul.Text = val.ToString();
            else
                lblKodeMatkul.Text = ""; // Kosongkan jika belum dipilih
        }
    }
}
