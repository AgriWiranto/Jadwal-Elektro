using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Aplikasi_Jadwal_ELEKTRO.Models;

namespace Aplikasi_Jadwal_ELEKTRO.Repositories
{
    public class MatkulRepository
    {
        private string connectionString = @"Data Source=LAPTOP-8SSGNIP4\MSSQLSERVER01;
                                            Initial Catalog=Elektro_db;
                                            Trusted_Connection=True;";

        public List<Matkul> GetMatkuls()
        {
            var list = new List<Matkul>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Matkul";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var matkul = new Matkul();
                    matkul.kode_matkul = Convert.ToInt32(reader["kode_matkul"]);
                    matkul.nama_matkul = reader["nama_matkul"]?.ToString();
                    matkul.sks = reader["sks"] != DBNull.Value ? Convert.ToInt32(reader["sks"]) : 0;
                    matkul.ruangan_preferensi = reader["ruangan_preferensi"]?.ToString();

                    list.Add(matkul);
                }
            }

            return list;
        }

        public List<Matkul> GetAll()
        {
            var list = new List<Matkul>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Matkul";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Matkul
                    {
                        kode_matkul = (int)reader["kode_matkul"],
                        nama_matkul = reader["nama_matkul"].ToString(),
                        sks = reader["sks"] != DBNull.Value ? Convert.ToInt32(reader["sks"]) : 0,
                        ruangan_preferensi = reader["ruangan_preferensi"]?.ToString()
                    });
                }
            }
            return list;
        }

        public Matkul GetMatkul(int kode)
        {
            Matkul matkul = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Matkul WHERE kode_matkul = @kode";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@kode", kode);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    matkul = new Matkul();
                    matkul.kode_matkul = Convert.ToInt32(reader["kode_matkul"]);
                    matkul.nama_matkul = reader["nama_matkul"]?.ToString();
                    matkul.sks = reader["sks"] != DBNull.Value ? Convert.ToInt32(reader["sks"]) : 0;
                    matkul.ruangan_preferensi = reader["ruangan_preferensi"]?.ToString();
                }
            }

            return matkul;
        }

        public void DeleteMatkul(int kode)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // 1. Hapus data dari tabel relasi yang memiliki foreign key ke Matkul
                string deleteJadwal = "DELETE FROM JadwalFinal WHERE kode_matkul = @kode";
                string deleteKelasMatkul = "DELETE FROM KelasMatkul WHERE kode_matkul = @kode";
                string deleteDosenMatkul = "DELETE FROM DosenMatkul WHERE kode_matkul = @kode";

                // 2. Hapus dari Matkul
                string deleteMatkul = "DELETE FROM Matkul WHERE kode_matkul = @kode";

                using (SqlCommand cmd1 = new SqlCommand(deleteJadwal, conn))
                using (SqlCommand cmd2 = new SqlCommand(deleteKelasMatkul, conn))
                using (SqlCommand cmd3 = new SqlCommand(deleteDosenMatkul, conn))
                using (SqlCommand cmd4 = new SqlCommand(deleteMatkul, conn))
                {
                    cmd1.Parameters.AddWithValue("@kode", kode);
                    cmd2.Parameters.AddWithValue("@kode", kode);
                    cmd3.Parameters.AddWithValue("@kode", kode);
                    cmd4.Parameters.AddWithValue("@kode", kode);

                    cmd1.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                    cmd3.ExecuteNonQuery();
                    cmd4.ExecuteNonQuery();
                }
            }
        }

        public void CreateMatkul(Matkul matkul)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO Matkul 
                                (kode_matkul, nama_matkul, sks, ruangan_preferensi) 
                                VALUES (@kode, @nama, @sks, @ruangan)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@kode", matkul.kode_matkul);
                cmd.Parameters.AddWithValue("@nama", matkul.nama_matkul);
                cmd.Parameters.AddWithValue("@sks", matkul.sks);
                cmd.Parameters.AddWithValue("@ruangan", matkul.ruangan_preferensi ?? (object)DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateMatkul(Matkul matkul)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"UPDATE Matkul SET 
                                nama_matkul = @nama, 
                                sks = @sks, 
                                ruangan_preferensi = @ruangan
                                WHERE kode_matkul = @kode";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@kode", matkul.kode_matkul);
                cmd.Parameters.AddWithValue("@nama", matkul.nama_matkul);
                cmd.Parameters.AddWithValue("@sks", matkul.sks);
                cmd.Parameters.AddWithValue("@ruangan", matkul.ruangan_preferensi ?? (object)DBNull.Value);

                cmd.ExecuteNonQuery();
            }
        }

        public bool CekKodeSudahAda(int kodeMatkul)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Matkul WHERE kode_matkul = @kode";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@kode", kodeMatkul);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
