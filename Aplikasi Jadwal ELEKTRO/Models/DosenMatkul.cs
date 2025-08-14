using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikasi_Jadwal_ELEKTRO.Models
{
    public class DosenMatkul
    {
        public int id { get; set; }
        public int kode_dosen { get; set; }
        public int kode_matkul { get; set; }

        public string nama_dosen { get; set; }  // opsional tampilan
        public string nama_matkul { get; set; } // opsional tampilan
    }

}
