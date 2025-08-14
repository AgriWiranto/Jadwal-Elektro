using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Aplikasi_Jadwal_ELEKTRO.Models;

namespace Aplikasi_Jadwal_ELEKTRO.Repositories
{
    public class RuanganRepository
    {
        private string connectionString = @"Data Source=LAPTOP-8SSGNIP4\MSSQLSERVER01;
                                             Initial Catalog=Elektro_db;
                                             Trusted_Connection=True;";

        public List<Ruangan> GetRuangans()
        {
            var ruangans = new List<Ruangan>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM ruangan ORDER BY kode DESC";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Ruangan ruangan = new Ruangan
                            {
                                kode_ruangan = (int)reader["kode"],
                                nama = reader["nama"].ToString(),
                                kategori = reader["kategori"].ToString()
                            };

                            ruangans.Add(ruangan);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Gagal koneksi atau query: " + ex.ToString());
            }

            return ruangans;
        }

        public Ruangan GetRuangan(int kode)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM ruangan WHERE kode=@kode";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@kode", kode);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Ruangan
                                {
                                    kode_ruangan = (int)reader["kode"],
                                    nama = reader["nama"].ToString(),
                                    kategori = reader["kategori"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

            return null;
        }

        public void CreateRuangan(Ruangan ruangan)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Ruangan (kode, nama, kategori) VALUES (@kode, @nama, @kategori)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@kode", ruangan.kode_ruangan);
                        command.Parameters.AddWithValue("@nama", ruangan.nama);
                        command.Parameters.AddWithValue("@kategori", ruangan.kategori);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }

        public void UpdateRuangan(Ruangan ruangan)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE ruangan SET nama = @nama, kategori = @kategori WHERE kode = @kode";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@kode", ruangan.kode_ruangan);
                        command.Parameters.AddWithValue("@nama", ruangan.nama);
                        command.Parameters.AddWithValue("@kategori", ruangan.kategori);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }

        public void DeleteRuangan(int kode)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "DELETE FROM ruangan WHERE kode=@kode";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@kode", kode);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }

        public bool CekKodeSudahAda(int kode)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM ruangan WHERE kode = @kode";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@kode", kode);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public List<Ruangan> GetAll()
        {
            List<Ruangan> list = new List<Ruangan>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Ruangan";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Ruangan
                    {
                        kode_ruangan = (int)reader["kode"],
                        nama = reader["nama"].ToString(),
                        kategori = reader["kategori"].ToString()
                    });
                }
            }

            return list;
        }


    }
}
