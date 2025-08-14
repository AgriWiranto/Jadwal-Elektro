using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikasi_Jadwal_ELEKTRO.Repositories
{
    public class LogDosenMatkulRepository
    {
        private string connectionString = @"Data Source=LAPTOP-8SSGNIP4\MSSQLSERVER01;
                                            Initial Catalog=Elektro_db;
                                            Trusted_Connection=True;";

        public void AddLog(int kode_dosen, int kode_matkul, string aksi)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                INSERT INTO LogDosenMatkul (kode_dosen, kode_matkul, aksi)
                VALUES (@kode_dosen, @kode_matkul, @aksi)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@kode_dosen", kode_dosen);
                cmd.Parameters.AddWithValue("@kode_matkul", kode_matkul);
                cmd.Parameters.AddWithValue("@aksi", aksi);
                cmd.ExecuteNonQuery();
            }
        }
    }

}
