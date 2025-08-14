using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikasi_Jadwal_ELEKTRO.Models
{
    public class LogDosenMatkul
    {
        public int id { get; set; }
        public int kode_dosen { get; set; }
        public int kode_matkul { get; set; }
        public string aksi { get; set; }
        public DateTime waktu { get; set; }
    }

}
