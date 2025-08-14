using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplikasi_Jadwal_ELEKTRO.Models;

namespace Aplikasi_Jadwal_ELEKTRO.Repositories
{
    public class JadwalKuliahRepository
    {
        private string connectionString = @"Data Source=LAPTOP-8SSGNIP4\MSSQLSERVER01;
                                            Initial Catalog=Elektro_db;
                                            Trusted_Connection=True;";

        public void Reset()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM JadwalFinal";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
        }

        public void Insert(JadwalFinal jadwal)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    INSERT INTO JadwalFinal 
                    (kode_dosen, kode_matkul, id_kelas, id_waktu, kode_ruangan)
                    VALUES 
                    (@kode_dosen, @kode_matkul, @id_kelas, @id_waktu, @kode_ruangan)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@kode_dosen", jadwal.kode_dosen);
                cmd.Parameters.AddWithValue("@kode_matkul", jadwal.kode_matkul);
                cmd.Parameters.AddWithValue("@id_kelas", jadwal.id_kelas);
                cmd.Parameters.AddWithValue("@id_waktu", jadwal.id_waktu); // ini adalah id_waktu
                cmd.Parameters.AddWithValue("@kode_ruangan", jadwal.kode_ruangan);

                cmd.ExecuteNonQuery();
            }
        }

        public List<JadwalFinal> GetAll()
        {
            List<JadwalFinal> list = new List<JadwalFinal>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM JadwalFinal";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new JadwalFinal
                    {
                        id_jadwal = (int)reader["id_jadwal"],
                        kode_dosen = (int)reader["kode_dosen"],
                        kode_matkul = (int)reader["kode_matkul"],
                        id_kelas = (int)reader["id_kelas"],
                        id_waktu = (int)reader["id_waktu"],
                        kode_ruangan = (int)reader["kode_ruangan"]
                    });
                }
            }

            return list;
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM JadwalFinal WHERE id_jadwal = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }


    }
}
