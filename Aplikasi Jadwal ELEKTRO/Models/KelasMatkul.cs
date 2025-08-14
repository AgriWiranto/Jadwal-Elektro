using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikasi_Jadwal_ELEKTRO.Models
{
    public class KelasMatkul
    {
        public int id_kelas { get; set; }
        public int kode_matkul { get; set; }

        // Optional untuk tampilan
        public string nama_kelas { get; set; }
        public string nama_matkul { get; set; }
    }

}
