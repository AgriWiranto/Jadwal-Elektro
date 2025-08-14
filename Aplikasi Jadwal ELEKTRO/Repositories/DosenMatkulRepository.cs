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
    public class DosenMatkulRepository
    {
        private string connectionString = @"Data Source=LAPTOP-8SSGNIP4\MSSQLSERVER01;
                                            Initial Catalog=Elektro_db;
                                            Trusted_Connection=True;";

        public List<DosenMatkul> GetByDosen(int kode_dosen)
        {
            List<DosenMatkul> list = new List<DosenMatkul>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                SELECT dm.id, dm.kode_dosen, dm.kode_matkul,
                       d.nama_dosen, m.nama_matkul
                FROM DosenMatkul dm
                JOIN Dosen_Elek d ON dm.kode_dosen = d.kode_dosen
                JOIN Matkul m ON dm.kode_matkul = m.kode_matkul
                WHERE dm.kode_dosen = @kode_dosen";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@kode_dosen", kode_dosen);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new DosenMatkul
                    {
                        id = (int)reader["id"],
                        kode_dosen = (int)reader["kode_dosen"],
                        kode_matkul = (int)reader["kode_matkul"],
                        nama_dosen = reader["nama_dosen"].ToString(),
                        nama_matkul = reader["nama_matkul"].ToString()
                    });
                }
            }
            return list;
        }

        public List<DosenMatkul> GetAll()
        {
            var list = new List<DosenMatkul>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var query = "SELECT * FROM DosenMatkul";
                var cmd = new SqlCommand(query, conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new DosenMatkul
                    {
                        kode_dosen = (int)reader["kode_dosen"],
                        kode_matkul = (int)reader["kode_matkul"],
                        //sks_diampu = (int)reader["sks_diampu"]
                    });
                }
            }
            return list;
        }

        public void Add(DosenMatkul dm)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO DosenMatkul (kode_dosen, kode_matkul) VALUES (@kode_dosen, @kode_matkul)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@kode_dosen", dm.kode_dosen);
                cmd.Parameters.AddWithValue("@kode_matkul", dm.kode_matkul);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM DosenMatkul WHERE id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }


}
