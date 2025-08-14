using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikasi_Jadwal_ELEKTRO.Models
{
    public class Kelas
    {
        public int id_kelas { get; set; }
        public string namaKelas { get; set; }
        public int angkatan { get; set; }
        public string tipe_kelas { get; set; }
    }
}
