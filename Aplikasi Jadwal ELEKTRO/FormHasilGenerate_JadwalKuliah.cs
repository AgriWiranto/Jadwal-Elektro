using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aplikasi_Jadwal_ELEKTRO
{
    public partial class FormHasilGenerate_JadwalKuliah : Form
    {

        private string connectionString = @"Data Source=LAPTOP-8SSGNIP4\MSSQLSERVER01;
                                             Initial Catalog=Elektro_db;
                                             Trusted_Connection=True;";

        public FormHasilGenerate_JadwalKuliah()
        {
            InitializeComponent();
        }

        private void FormHasilGenerate_JadwalKuliah_Load(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Jadwal_Kuliah ORDER BY hari, jam_mulai", conn);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGridViewHasilGenerate.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menampilkan hasil generate: " + ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
