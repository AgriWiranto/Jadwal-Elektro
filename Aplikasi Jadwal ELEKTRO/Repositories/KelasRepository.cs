using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplikasi_Jadwal_ELEKTRO.Models;

namespace Aplikasi_Jadwal_ELEKTRO.Repositories
{
    public class KelasRepository
    {
        private string connectionString = @"Data Source=LAPTOP-8SSGNIP4\MSSQLSERVER01;Initial Catalog=Elektro_db;Trusted_Connection=True;";

        public List<Kelas> GetAll()
        {
            List<Kelas> list = new List<Kelas>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var query = "SELECT * FROM Kelas";
                var cmd = new SqlCommand(query, conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Kelas
                    {
                        id_kelas = (int)reader["id_kelas"],
                        namaKelas = reader["namaKelas"].ToString(),
                        angkatan = (int)reader["angkatan"],
                        tipe_kelas = reader["tipe_kelas"].ToString()
                    });
                }
            }
            return list;
        }

        public void Add(Kelas k)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var query = "INSERT INTO Kelas (id_kelas, namaKelas, angkatan, tipe_kelas) VALUES (@id, @nama, @angkatan, @tipe)";
                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", k.id_kelas);
                cmd.Parameters.AddWithValue("@nama", k.namaKelas);
                cmd.Parameters.AddWithValue("@angkatan", k.angkatan);
                cmd.Parameters.AddWithValue("@tipe", k.tipe_kelas);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id_kelas)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var query = "DELETE FROM Kelas WHERE id_kelas = @id";
                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id_kelas);
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Kelas k)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var query = "UPDATE Kelas SET namaKelas = @nama, angkatan = @angkatan, tipe_kelas = @tipe WHERE id_kelas = @id";
                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", k.id_kelas);
                cmd.Parameters.AddWithValue("@nama", k.namaKelas);
                cmd.Parameters.AddWithValue("@angkatan", k.angkatan);
                cmd.Parameters.AddWithValue("@tipe", k.tipe_kelas);
                cmd.ExecuteNonQuery();
            }
        }
    }

}
