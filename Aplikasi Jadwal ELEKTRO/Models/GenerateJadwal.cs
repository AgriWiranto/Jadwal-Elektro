using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikasi_Jadwal_ELEKTRO.Models
{
    public class GenerateJadwal
    {
        public string nama_dosen { get; set; }
        public string nama_matkul { get; set; }
        public int id_semester { get; set; }
        public string nama_kelas { get; set; }
        public string nama_ruangan { get; set; }
        public string hari { get; set; }
        public TimeSpan jam_mulai { get; set; }
        public TimeSpan jam_selesai { get; set; }
    }
}
