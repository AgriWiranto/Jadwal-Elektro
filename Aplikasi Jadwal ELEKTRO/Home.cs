using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Reflection.Emit;

namespace Aplikasi_Jadwal_ELEKTRO
{
    public partial class Home : Form
    {
        private string username;
        private Button activeButton;

        // Tambahkan dictionary agar tahu hubungan antara Button dan PictureBox
        private Dictionary<Button, PictureBox> buttonIcons = new Dictionary<Button, PictureBox>();
        private Color warnaAktif = Color.FromArgb(0, 0, 192); // Biru tua
        private Color warnaDefault = Color.FromArgb(86, 128, 183); // Biru muda

        // Ganti inisialisasi buttonIcons ke dalam Home_Load agar semua Button dan PictureBox sudah terinisialisasi
        public Home(string uname)
        {
            InitializeComponent();

            // Simpan username yang diterima ke field
            username = uname;

            // Menyimpan username ke dalam Properties.Settings
            Properties.Settings.Default.Username = username;
            Properties.Settings.Default.Save();

            lblWelcome.BackColor = Color.Transparent;
            this.DoubleBuffered = true;
            btn_keluar.Click += new EventHandler(BtnLogout_Click);
        }

        private void Home_Load(object sender, EventArgs e)
        {
            // Inisialisasi mapping tombol dengan icon PictureBox-nya di sini
            buttonIcons.Clear();
            buttonIcons.Add(btn_dosen, pic_dosen);
            buttonIcons.Add(btn_matkul, pic_matkul);
            //buttonIcons.Add(btn_kelas, pic_kelas);
            buttonIcons.Add(btn_ruangan, pic_ruangan);
            buttonIcons.Add(btn_dashboard, pic_dashboard);
            // Tambahkan mapping untuk button lain jika ada, misal:
            // buttonIcons.Add(button3, pic_programStudi);
            // buttonIcons.Add(button2, pic_semester);
            // buttonIcons.Add(button4, pic_waktuTersedia);

            ShowDashboard();
            this.WindowState = FormWindowState.Maximized;
        }

        private void ShowDashboard()
        {
            OpenFormInPanel(new FormDashboardAdmin());
        }


        // Perbaiki SetActiveButton agar hanya tombol yang ada di dictionary yang diubah warnanya
        private void SetActiveButton(Button clickedButton)
        {
            // Reset semua tombol dan ikon ke default
            foreach (var pair in buttonIcons)
            {
                pair.Key.BackColor = warnaDefault;
                pair.Key.ForeColor = Color.Black;
                pair.Value.BackColor = warnaDefault;
            }

            // Aktifkan tombol & ikon jika ada di dictionary
            if (buttonIcons.ContainsKey(clickedButton))
            {
                clickedButton.BackColor = warnaAktif;
                clickedButton.ForeColor = Color.White;
                buttonIcons[clickedButton].BackColor = warnaAktif;
            }
            else
            {
                // Jika tombol tidak ada di dictionary, tetap set warnanya
                clickedButton.BackColor = warnaAktif;
                clickedButton.ForeColor = Color.White;
            }

            activeButton = clickedButton;
        }



        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        //Tombol Untuk Menu Ruangan
        private void button4_Click(object sender, EventArgs e)
        {
            SetActiveButton(btn_ruangan);
            OpenFormInPanel(new FormRuangan());
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        // Fix for CS0121: The call is ambiguous between the following methods or properties: 'FormDosen.FormDosen()' and 'FormDosen.FormDosen()'

        // The error occurs because there are multiple constructors for the `FormDosen` class with the same signature, 
        // or the compiler cannot distinguish between them. To resolve this, ensure that the correct constructor is explicitly called.

        private void btn_dosen_Click(object sender, EventArgs e)
        {
            SetActiveButton(btn_dosen);

            // Explicitly specify the correct constructor for FormDosen
            OpenFormInPanel(new FormDosen(/* Pass required parameters if any */));
        }

        //Tombol Untuk Menu Dosen
        private void btn_matkul_Click(object sender, EventArgs e)
        {
            SetActiveButton(btn_matkul);
            OpenFormInPanel(new FormMatkul());
        }

        //Tombol Untuk Menu Kelas
        //private void btn_kelas_Click(object sender, EventArgs e)
        //{
            //SetActiveButton(btn_kelas);
            //OpenFormInPanel(new FormKelas());
        //}

       


        private void lbltext_Click(object sender, EventArgs e)
        {

        }

        private void btn_keluar_Click(object sender, EventArgs e)
        {
           
        }
        // FUNGSI LOGOUT
        private void BtnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah Anda yakin ingin logout?", "Konfirmasi Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Properties.Settings.Default.Username = ""; // Hapus username saat logout
                Properties.Settings.Default.Save();

                this.Hide(); // Sembunyikan form Home
                Form1 loginForm = new Form1();
                loginForm.Show(); // Tampilkan kembali form login
            }
        }

        private void OpenFormInPanel(Form form)
        {
            panelContent.Controls.Clear(); // Hapus form sebelumnya
            form.TopLevel = false;  // Form tidak berdiri sendiri
            form.FormBorderStyle = FormBorderStyle.None; // Hilangkan border
            form.Dock = DockStyle.Fill; // Isi panel sepenuhnya
            panelContent.Controls.Add(form); // Tambahkan form ke panel
            form.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btn_dashboard_Click(object sender, EventArgs e)
        {
            SetActiveButton(btn_dashboard);
            ShowDashboard();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panelContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pic_dashboard_Click(object sender, EventArgs e)
        {

        }

       // private void button3_Click(object sender, EventArgs e)
        //{
          //  SetActiveButton(button3);
            //OpenFormInPanel(new FormProgramStudi());
        //}

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
        // private void button2_Click(object sender, EventArgs e)
        // {
        //     SetActiveButton(button2);
        //     OpenFormInPanel(new FormSemester());
        // }

        // private void button4_Click_1(object sender, EventArgs e)
        // {
        //     SetActiveButton(button4);
        //     OpenFormInPanel(new FormWaktuTersedia());
        // }

        private void pic_kelas_Click(object sender, EventArgs e)
        {

        }

        private void btn_waktu_Click(object sender, EventArgs e)
        {
            SetActiveButton(btn_waktu);
            OpenFormInPanel(new FormWaktu());
        }

        private void btn_DosenMatkul_Click(object sender, EventArgs e)
        {
            SetActiveButton(btn_DosenMatkul);
            OpenFormInPanel(new FormDosenMatkul());
        }

        private void btn_kelas_Click(object sender, EventArgs e)
        {
            SetActiveButton(btn_kelas);
            OpenFormInPanel(new FormKelas());
        }

        private void btnKelasMatkul_Click(object sender, EventArgs e)
        {
            SetActiveButton(btnKelasMatkul);
            OpenFormInPanel(new FormKelasMatkul());
        }
    }
}
