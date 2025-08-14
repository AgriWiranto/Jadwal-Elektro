using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikasi_Jadwal_ELEKTRO.Models
{
    public class DashboardItem
    {
        public int KodeMatkul { get; set; }
        public string NamaMatkul { get; set; }
        public int Semester { get; set; }
        public string NamaDosen { get; set; }
        public string NamaKelas { get; set; }
        public string NamaProdi { get; set; }
        public string NamaRuangan { get; set; }
        public int JumlahSKS { get; set; }
        public int JumlahJam { get; set; }
    }
}
