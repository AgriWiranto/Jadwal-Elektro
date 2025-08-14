using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Aplikasi_Jadwal_ELEKTRO.Models;
using Aplikasi_Jadwal_ELEKTRO.Repositories;

namespace Aplikasi_Jadwal_ELEKTRO
{
    public partial class FormRuangan : Form
    {
        public FormRuangan()
        {
            InitializeComponent();
        }

        private void FormRuangan_Load(object sender, EventArgs e)
        {
            RefreshRuanganTable();
            RuanganTabel.SelectionChanged += RuanganTabel_SelectionChanged;
            btn_editRGN.Enabled = false;
            btn_deleteRGN.Enabled = false;
        }

        private void RefreshRuanganTable()
        {
            var repo = new RuanganRepository();
            var ruanganList = repo.GetRuangans();

            DataTable dt = new DataTable();
            dt.Columns.Add("Kode");
            dt.Columns.Add("Nama");
            dt.Columns.Add("Kategori");

            foreach (var ruangan in ruanganList)
            {
                DataRow row = dt.NewRow();
                row["Kode"] = ruangan.kode_ruangan;
                row["Nama"] = ruangan.nama;
                row["Kategori"] = ruangan.kategori;
                dt.Rows.Add(row);
            }

            RuanganTabel.DataSource = dt;
        }

        private void btnRefreshRGN_Click(object sender, EventArgs e)
        {
            RefreshRuanganTable();
        }

        private void btn_tambahdataRGN_Click(object sender, EventArgs e)
        {
            FormRuangan_TambahData form = new FormRuangan_TambahData();
            if (form.ShowDialog() == DialogResult.OK)
            {
                RefreshRuanganTable();
            }
        }

        private void btn_editRGN_Click(object sender, EventArgs e)
        {
            if (RuanganTabel.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data ruangan terlebih dahulu.");
                return;
            }

            var val = RuanganTabel.SelectedRows[0].Cells["Kode"].Value?.ToString();
            if (string.IsNullOrEmpty(val) || !int.TryParse(val, out int ruanganId)) return;

            var repo = new RuanganRepository();
            var ruangan = repo.GetRuangan(ruanganId);
            if (ruangan == null) return;

            FormRuangan_TambahData form = new FormRuangan_TambahData();
            form.EditRuangan(ruangan);
            if (form.ShowDialog() == DialogResult.OK)
            {
                RefreshRuanganTable();
            }
        }

        private void btn_deleteRGN_Click(object sender, EventArgs e)
        {
            if (RuanganTabel.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data ruangan terlebih dahulu.");
                return;
            }

            var val = RuanganTabel.SelectedRows[0].Cells["Kode"].Value?.ToString();
            if (string.IsNullOrEmpty(val) || !int.TryParse(val, out int ruanganId)) return;

            DialogResult dialogResult = MessageBox.Show(
                "Apakah Kamu Ingin Benar menghapus data Ruangan ini?",
                "Hapus Ruangan",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (dialogResult == DialogResult.Yes)
            {
                var repo = new RuanganRepository();
                repo.DeleteRuangan(ruanganId);
                RefreshRuanganTable();
            }
        }

        private void RuanganTabel_SelectionChanged(object sender, EventArgs e)
        {
            bool selected = RuanganTabel.SelectedRows.Count > 0;
            btn_editRGN.Enabled = selected;
            btn_deleteRGN.Enabled = selected;
        }
    }
}
