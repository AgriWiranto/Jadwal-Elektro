using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikasi_Jadwal_ELEKTRO.Models
{
    public class WaktuSlotConfig
    {
        public int id { get; set; }
        public string tipe_kelas { get; set; } // "Pagi" atau "Malam"
        public TimeSpan jam_mulai { get; set; }
        public TimeSpan jam_selesai { get; set; } // dihitung otomatis
        public int durasi_per_sks { get; set; }   // menit per sesi
        public string istirahat_setelah_sesi { get; set; } // contoh: "3,6"
    }
}

