using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Aplikasi_Jadwal_ELEKTRO.Models;

namespace Aplikasi_Jadwal_ELEKTRO.Repositories
{
    public class WaktuSlotConfigRepository
    {
        private string connectionString = @"Data Source=LAPTOP-8SSGNIP4\MSSQLSERVER01;Initial Catalog=Elektro_db;Trusted_Connection=True;";

        public List<WaktuSlotConfig> GetAll()
        {
            var list = new List<WaktuSlotConfig>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM WaktuSlotConfig";
                var cmd = new SqlCommand(query, conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new WaktuSlotConfig
                    {
                        id = (int)reader["id"],
                        tipe_kelas = reader["tipe_kelas"].ToString(),
                        jam_mulai = (TimeSpan)reader["jam_mulai"],
                        jam_selesai = (TimeSpan)reader["jam_selesai"],
                        durasi_per_sks = (int)reader["durasi_per_sks"],
                        istirahat_setelah_sesi = reader["istirahat_setelah_sesi"].ToString()
                    });
                }
            }

            return list;
        }

        public void Insert(WaktuSlotConfig config)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    INSERT INTO WaktuSlotConfig (tipe_kelas, jam_mulai, jam_selesai, durasi_per_sks, istirahat_setelah_sesi)
                    VALUES (@tipe_kelas, @jam_mulai, @jam_selesai, @durasi_per_sks, @istirahat)";

                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@tipe_kelas", config.tipe_kelas);
                cmd.Parameters.AddWithValue("@jam_mulai", config.jam_mulai);
                cmd.Parameters.AddWithValue("@jam_selesai", config.jam_selesai);
                cmd.Parameters.AddWithValue("@durasi_per_sks", config.durasi_per_sks);
                cmd.Parameters.AddWithValue("@istirahat", config.istirahat_setelah_sesi);

                cmd.ExecuteNonQuery();
            }
        }

        public void HapusSemua()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM WaktuSlotConfig", conn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
