using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikasi_Jadwal_ELEKTRO.Models
{
    public class Waktu
    {
        public int id_waktu { get; set; }
        public string hari { get; set; }           // Senin - Jumat
        public string tipe_kelas { get; set; }     // Pagi / Malam
        public string sesi { get; set; }           // P1 - P8 / M1 - M8 / "-"
        public TimeSpan jam_mulai { get; set; }
        public TimeSpan jam_selesai { get; set; }
        public string keterangan { get; set; }     // Kuliah / Istirahat
        public int id_kelas { get; set; }  // foreign key ke Kelas

    }
}

