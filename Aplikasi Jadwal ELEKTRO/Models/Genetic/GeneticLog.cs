using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikasi_Jadwal_ELEKTRO.Models.Genetic
{
    public class GeneticLog
    {
        public int id { get; set; }
        public int generation { get; set; }
        public int fitness { get; set; }
        public int total_genes { get; set; }
        public DateTime timestamp { get; set; }
    }
}
