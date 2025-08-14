using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using Aplikasi_Jadwal_ELEKTRO.Models;

namespace Aplikasi_Jadwal_ELEKTRO.Repositories
{
    public class DosenRepository
    {
        private string connectionString = @"Data Source=LAPTOP-8SSGNIP4\MSSQLSERVER01;
                                            Initial Catalog=Elektro_db;
                                            Trusted_Connection=True;";

        public List<Dosen> GetDosens()
        {
            var dosens = new List<Dosen>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Dosen_Elek ORDER BY kode_dosen DESC";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Dosen dosen = new Dosen();
                                dosen.kode_dosen = (int)reader["kode_dosen"];
                                dosen.nama_dosen = reader["nama_dosen"].ToString();
                                dosen.nip = reader["nip"].ToString();
             

                                dosens.Add(dosen);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Gagal koneksi atau query: " + ex.ToString());
            }

            return dosens;
        }

        public Dosen GetDosen(int kode_dosen)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM Dosen_Elek WHERE kode_dosen=@kode_dosen";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@kode_dosen", kode_dosen);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Dosen dosen = new Dosen();
                                dosen.kode_dosen = (int)reader["kode_dosen"];
                                dosen.nama_dosen= reader["nama_dosen"].ToString();
                                dosen.nip = reader["nip"].ToString();
                                return dosen;
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

        public bool CreateDosen(Dosen dosen)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"INSERT INTO Dosen_Elek (kode_dosen, nama_dosen, nip)

                           VALUES (@kode_dosen, @nama_dosen, @nip)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@kode_dosen", dosen.kode_dosen);
                        command.Parameters.AddWithValue("@nama_dosen", dosen.nama_dosen);
                        command.Parameters.AddWithValue("@nip", dosen.nip);
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat menambahkan dosen: " + ex.Message);
                return false;
            }
        }



        public void UpdateDosen(Dosen dosen)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"UPDATE Dosen_Elek 
                           SET nama_dosen=@nama_dosen, nip=@nip
                           WHERE kode_dosen=@kode_dosen";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nama_dosen", dosen.nama_dosen);
                        command.Parameters.AddWithValue("@nip", dosen.nip);
                        command.Parameters.AddWithValue("@kode_dosen", dosen.kode_dosen);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }


        public bool CekIdSudahAda(int kode_dosen)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Dosen_Elek WHERE kode_dosen = @kode_dosen";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@kode_dosen", kode_dosen);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public void DeleteDosen(int kode_dosen)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "DELETE FROM Dosen_Elek WHERE kode_dosen = @kode_dosen";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@kode_dosen", kode_dosen);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Gagal menghapus dosen. Pastikan tidak ada relasi aktif.\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public List<Dosen> GetAll()
        {
            List<Dosen> list = new List<Dosen>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Dosen_Elek";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Dosen
                    {
                        kode_dosen = (int)reader["kode_dosen"],
                        nama_dosen = reader["nama_dosen"].ToString(),
                        nip = reader["nip"].ToString()
                    });
                }
            }
            return list;
        }


    }
}
