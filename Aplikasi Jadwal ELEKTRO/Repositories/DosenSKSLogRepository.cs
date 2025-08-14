using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplikasi_Jadwal_ELEKTRO.Models;

namespace Aplikasi_Jadwal_ELEKTRO.Repositories
{
    public class DosenSKSLogRepository
    {
        private string connectionString = @"Data Source=LAPTOP-8SSGNIP4\MSSQLSERVER01;Initial Catalog=Elektro_db;Trusted_Connection=True;";

        public void InsertLog(DosenSKSLog log)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO DosenSKSLog 
                    (kode_dosen, nama_dosen, total_sks, batas_sks, keterangan) 
                    VALUES (@kode_dosen, @nama_dosen, @total_sks, @batas_sks, @keterangan)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@kode_dosen", log.kode_dosen);
                cmd.Parameters.AddWithValue("@nama_dosen", log.nama_dosen ?? "");
                cmd.Parameters.AddWithValue("@total_sks", log.total_sks);
                cmd.Parameters.AddWithValue("@batas_sks", log.batas_sks);
                cmd.Parameters.AddWithValue("@keterangan", log.keterangan ?? "");
                cmd.ExecuteNonQuery();
            }
        }
    }
}
