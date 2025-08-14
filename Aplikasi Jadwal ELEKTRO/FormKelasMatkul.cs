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
    public partial class FormKelasMatkul : Form
    {
        private KelasMatkulRepository repo = new KelasMatkulRepository();
        private List<Kelas> kelasList;
        private List<Matkul> matkulList;

        public FormKelasMatkul()
        {
            InitializeComponent();
        }

        private void FormKelasMatkul_Load(object sender, EventArgs e)
        {
            LoadKelas();
            LoadMatkul();
        }

        private void LoadKelas()
        {
            var kelasRepo = new KelasRepository();
            kelasList = kelasRepo.GetAll();

            // Tambah placeholder
            kelasList.Insert(0, new Kelas
            {
                id_kelas = -1,
                namaKelas = "-- Silakan Pilih Kelas --"
            });

            cmbKelas.DataSource = kelasList;
            cmbKelas.DisplayMember = "namaKelas";
            cmbKelas.ValueMember = "id_kelas";
            cmbKelas.SelectedIndex = 0;
        }

        private void LoadMatkul()
        {
            var matkulRepo = new MatkulRepository();
            matkulList = matkulRepo.GetMatkuls();

            // Tambah placeholder
            matkulList.Insert(0, new Matkul
            {
                kode_matkul = -1,
                nama_matkul = "-- Silakan Pilih Mata Kuliah --"
            });

            cmbMatkul.DataSource = matkulList;
            cmbMatkul.DisplayMember = "nama_matkul";
            cmbMatkul.ValueMember = "kode_matkul";
            cmbMatkul.SelectedIndex = 0;
        }

        private void cmbKelas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbKelas.SelectedItem is Kelas selected)
            {
                LoadMatkulKelas(selected.id_kelas.ToString());
            }
        }

        private void LoadMatkulKelas(string id_kelas)
        {
            var list = repo.GetMatkulByKelas(id_kelas);
            dgvKelasMatkul.DataSource = list;
        }

        private void TambahMatkulUntukKelas_Click(object sender, EventArgs e)
        {
            if ((int)cmbKelas.SelectedValue == -1 || (int)cmbMatkul.SelectedValue == -1)
            {
                MessageBox.Show("Silakan pilih kelas dan mata kuliah yang valid terlebih dahulu.");
                return;
            }

            var id_kelas = cmbKelas.SelectedValue.ToString();
            var kode_matkul = (int)cmbMatkul.SelectedValue;

            repo.TambahMatkulUntukKelas(id_kelas, kode_matkul);
            LoadMatkulKelas(id_kelas); LoadMatkulKelas(id_kelas);
            
        }

        private void HapusMatkulUntukKelas_Click(object sender, EventArgs e)
        {
            if ((int)cmbKelas.SelectedValue == -1 || dgvKelasMatkul.CurrentRow == null)
            {
                MessageBox.Show("Pilih kelas dan mata kuliah yang ingin dihapus.");
                return;
            }

            var id_kelas = cmbKelas.SelectedValue.ToString();
            var selected = (Matkul)dgvKelasMatkul.CurrentRow.DataBoundItem;

            if (selected == null) return;

            var confirm = MessageBox.Show("Yakin ingin menghapus mata kuliah ini?", "Konfirmasi", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                repo.HapusMatkulUntukKelas(id_kelas, selected.kode_matkul);
                LoadMatkulKelas(id_kelas);
            }
        }

        private void cmbMatkul_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMatkul.SelectedValue is int val && val != -1)
                lblKodeMatkul.Text = val.ToString();
            else
                lblKodeMatkul.Text = ""; // Kosongkan jika belum dipilih
        }
    }
}
