using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikasi_Jadwal_ELEKTRO.Models
{
    public class DosenSKSLog
    {
        public int id_log { get; set; }
        public int kode_dosen { get; set; }
        public string nama_dosen { get; set; }
        public int total_sks { get; set; }
        public int batas_sks { get; set; }
        public string keterangan { get; set; }
        public DateTime timestamp { get; set; }
    }

}
