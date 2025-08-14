using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Aplikasi_Jadwal_ELEKTRO
{
    public partial class TampilanMahasiswa : Form
    {
        private string dburl = @"Data Source=LAPTOP-8SSGNIP4\MSSQLSERVER01;
                                 Initial Catalog=Elektro_db;
                                 Trusted_Connection=True;";
        internal int kodeMatkul;
        internal string namaMatkul;
        internal int semester;
        internal string namaDosen;

        public TampilanMahasiswa()
        {
            InitializeComponent();
            LoadData(); // Memuat data saat aplikasi dibuka
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(dburl))
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();

                    using (DataTable dt = new DataTable("Jadwal_Kuliah"))
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT * FROM Jadwal_Kuliah", cn))
                        {
                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            adapter.Fill(dt);

                            // Menampilkan data ke dalam DataGridView
                            dataGridView1.DataSource = dt;

                            // Mencegah pengguna mengedit data
                            dataGridView1.ReadOnly = true;
                            dataGridView1.AllowUserToAddRows = false; // Mencegah baris kosong di akhir
                            lblTotal.Text = $"DATA DITEMUKAN: {dataGridView1.RowCount}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(); // Membuat instance Form1
            form1.Show(); // Menampilkan Form1
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(dburl))
                {
                    if (cn.State == ConnectionState.Closed)
                        cn.Open();

                    using (DataTable dt = new DataTable("Jadwal_Kuliah"))
                    {
                        using (SqlCommand cmd = new SqlCommand(@"
                    SELECT * FROM Jadwal_Kuliah 
                    WHERE nama_matkul LIKE @search 
                    OR nama_dosen LIKE @search 
                    OR nama_kelas LIKE @search 
                    OR nama_ruangan LIKE @search 
                    OR hari LIKE @search 
                    OR semester LIKE @search 
                    OR jam_mulai LIKE @search 
                    OR jam_selesai LIKE @search", cn))
                        {
                            cmd.Parameters.AddWithValue("@search", "%" + textBox1.Text + "%");

                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            adapter.Fill(dt);
                            dataGridView1.DataSource = dt;
                            lblTotal.Text = $"Hasil Pencarian : {dataGridView1.RowCount}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) //Enter
                btn_CariMK.PerformClick();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData(); // Memanggil ulang fungsi LoadData untuk merefresh tampilan
            textBox1.Text = ""; // Kosongkan textbox pencarian
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
