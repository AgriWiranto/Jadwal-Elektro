using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Aplikasi_Jadwal_ELEKTRO.Models;

namespace Aplikasi_Jadwal_ELEKTRO.Repositories
{
    public class WaktuRepository
    {
        private string connectionString = @"Data Source=LAPTOP-8SSGNIP4\MSSQLSERVER01;Initial Catalog=Elektro_db;Trusted_Connection=True;";

        public List<Waktu> GetAll()
        {
            var list = new List<Waktu>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Waktu ORDER BY hari, tipe_kelas, jam_mulai";
                var cmd = new SqlCommand(query, conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Waktu
                    {
                        id_waktu = (int)reader["id_waktu"],
                        hari = reader["hari"].ToString(),
                        tipe_kelas = reader["tipe_kelas"].ToString(),
                        sesi = reader["sesi"].ToString(),
                        jam_mulai = (TimeSpan)reader["jam_mulai"],
                        jam_selesai = (TimeSpan)reader["jam_selesai"],
                        keterangan = reader["keterangan"].ToString(),
                        id_kelas = reader["id_kelas"] == DBNull.Value ? 0 : (int)reader["id_kelas"]
                    });
                }
            }

            return list;
        }

        public void InsertBatch(List<Waktu> list)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                foreach (var item in list)
                {
                    string query = @"
                                    INSERT INTO Waktu (id_kelas, hari, tipe_kelas, sesi, jam_mulai, jam_selesai, keterangan)
                                    VALUES (@id_kelas, @hari, @tipe_kelas, @sesi, @jam_mulai, @jam_selesai, @keterangan)";

                    var cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@hari", item.hari);
                    cmd.Parameters.AddWithValue("@tipe_kelas", item.tipe_kelas);
                    cmd.Parameters.AddWithValue("@sesi", item.sesi);
                    cmd.Parameters.AddWithValue("@jam_mulai", item.jam_mulai);
                    cmd.Parameters.AddWithValue("@jam_selesai", item.jam_selesai);
                    cmd.Parameters.AddWithValue("@keterangan", item.keterangan);
                    cmd.Parameters.AddWithValue("@id_kelas", item.id_kelas);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void HapusSemua()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM Waktu", conn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
