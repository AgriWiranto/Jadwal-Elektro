using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikasi_Jadwal_ELEKTRO.Models
{
    public sealed class GaProgress
    {
        public int Percent { get; set; }    // 0..100
        public int Generation { get; set; } // generasi saat ini
        public string Message { get; set; } // keterangan
    }
}
