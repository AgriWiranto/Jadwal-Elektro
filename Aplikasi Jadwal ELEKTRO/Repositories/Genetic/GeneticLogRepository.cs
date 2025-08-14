using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplikasi_Jadwal_ELEKTRO.Models.Genetic;

namespace Aplikasi_Jadwal_ELEKTRO.Repositories.Genetic
{
    public class GeneticLogRepository
    {
        private string connectionString = @"Data Source=LAPTOP-8SSGNIP4\MSSQLSERVER01;Initial Catalog=Elektro_db;Trusted_Connection=True;";

        public void InsertLog(GeneticLog log)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"
                    INSERT INTO GeneticLog (generation, fitness, total_genes, timestamp)
                    VALUES (@generation, @fitness, @total_genes, @timestamp)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@generation", log.generation);
                    cmd.Parameters.AddWithValue("@fitness", log.fitness);
                    cmd.Parameters.AddWithValue("@total_genes", log.total_genes);
                    cmd.Parameters.AddWithValue("@timestamp", log.timestamp);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
