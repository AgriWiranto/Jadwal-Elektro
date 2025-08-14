using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplikasi_Jadwal_ELEKTRO.Models;

namespace Aplikasi_Jadwal_ELEKTRO.Repositories
{
    public class KelasMatkulRepository
    {
        private string connectionString = @"Data Source=LAPTOP-8SSGNIP4\MSSQLSERVER01;Initial Catalog=Elektro_db;Trusted_Connection=True;";

        public List<KelasMatkul> GetAll()
        {
            var list = new List<KelasMatkul>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var query = @"
                    SELECT km.id_kelas, km.kode_matkul, k.namaKelas, m.nama_matkul
                    FROM KelasMatkul km
                    JOIN Kelas k ON km.id_kelas = k.id_kelas
                    JOIN Matkul m ON km.kode_matkul = m.kode_matkul";
                var cmd = new SqlCommand(query, conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new KelasMatkul
                    {
                        id_kelas = (int)reader["id_kelas"],
                        kode_matkul = (int)reader["kode_matkul"],
                        nama_kelas = reader["namaKelas"].ToString(),
                        nama_matkul = reader["nama_matkul"].ToString()
                    });
                }
            }
            return list;
        }

        public void TambahMatkulUntukKelas(string id_kelas, int kode_matkul)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("INSERT INTO KelasMatkul VALUES (@kelas, @matkul)", conn);
                cmd.Parameters.AddWithValue("@kelas", id_kelas);
                cmd.Parameters.AddWithValue("@matkul", kode_matkul);
                cmd.ExecuteNonQuery();
            }
        }


        public void HapusMatkulUntukKelas(string id_kelas, int kode_matkul)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM KelasMatkul WHERE id_kelas = @kelas AND kode_matkul = @matkul", conn);
                cmd.Parameters.AddWithValue("@kelas", id_kelas);
                cmd.Parameters.AddWithValue("@matkul", kode_matkul);
                cmd.ExecuteNonQuery();
            }
        }


        public List<Matkul> GetMatkulByKelas(string id_kelas)
        {
            List<Matkul> list = new List<Matkul>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var query = @"SELECT m.* 
                              FROM KelasMatkul km 
                              JOIN Matkul m ON km.kode_matkul = m.kode_matkul 
                              WHERE km.id_kelas = @id_kelas";
                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id_kelas", id_kelas);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Matkul
                    {
                        kode_matkul = reader["kode_matkul"] != DBNull.Value ? Convert.ToInt32(reader["kode_matkul"]) : 0,
                        nama_matkul = reader["nama_matkul"]?.ToString() ?? "",
                        sks = reader["sks"] != DBNull.Value ? Convert.ToInt32(reader["sks"]) : 0,
                    });
                }
            }
            return list;
        }

    }
}
