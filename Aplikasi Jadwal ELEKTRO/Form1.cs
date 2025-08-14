using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Policy;

namespace Aplikasi_Jadwal_ELEKTRO
{
    public partial class Form1 : Form
    {
        private string dburl = @"Data Source=LAPTOP-8SSGNIP4\MSSQLSERVER01;
                                 Initial Catalog=Elektro_db;
                                 Trusted_Connection=True;";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_2(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide(); // Sembunyikan Form1 (Login Admin)

            // Cari instance Dashboard yang sudah ada
            foreach (Form form in Application.OpenForms)
            {
                if (form is TampilanMahasiswa)
                {
                    form.Show(); // Tampilkan kembali form Dashboard yang sudah ada
                    return;
                }
            }

            // Jika tidak ditemukan, buat baru (cadangan jika form ditutup)
            TampilanMahasiswa dashboardForm = new TampilanMahasiswa();
            dashboardForm.Show();

            Close(); // Tutup Form1 setelah membuka Dashboard
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtname.Clear();
            txtpass.Clear();
            txtname.Focus();
        }

    
        //BUTTON MASUK
        private void button1_Click(object sender, EventArgs e)
        {
            string uname = txtname.Text.Trim();
            string upass = txtpass.Text.Trim();

            if (string.IsNullOrEmpty(uname) || string.IsNullOrEmpty(upass))
            {
                MessageBox.Show("Username dan Password harus diisi terlebih dahulu!");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(dburl))
                {
                    conn.Open();

                    string sqlquery = "SELECT * FROM Login_Admin WHERE NamaUser = @uname AND Password = @upass";
                    using (SqlCommand cmd = new SqlCommand(sqlquery, conn))
                    {
                        cmd.Parameters.AddWithValue("@uname", uname);
                        cmd.Parameters.AddWithValue("@upass", upass);

                        using (SqlDataReader rs = cmd.ExecuteReader())
                        {
                            if (rs.Read())
                            {
                                MessageBox.Show("Login berhasil!");

                                // Kirim username ke form Index
                                this.Hide();
                                Home indexForm = new Home(uname);
                                indexForm.ShowDialog();
                                this.Show();
                            }
                            else
                            {
                                MessageBox.Show("Login gagal! Periksa kembali username dan password.");
                                txtname.Clear();
                                txtpass.Clear();
                                txtname.Focus();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan: " + ex.Message);
            }
        }

        private void txtpass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Jika tombol Enter ditekan
            {
                button1.PerformClick(); // Jalankan event klik pada tombol login
                e.SuppressKeyPress = true; // Mencegah bunyi 'beep' saat menekan Enter
            }
        }

        private void txtpass_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtname_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab) // Jika tombol Tab ditekan
            {
                txtpass.Focus(); // Pindahkan fokus ke txtPassword
                e.SuppressKeyPress = true; // Mencegah efek default Tab pada kontrol lain
            }
        }
    }
}
