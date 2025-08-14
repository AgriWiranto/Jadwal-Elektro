using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikasi_Jadwal_ELEKTRO.Models
{
    public class Gene
    {
        public int kode_dosen { get; set; }
        public int kode_matkul { get; set; }
        public int id_kelas { get; set; }
        public int id_waktu { get; set; }
        public int kode_ruangan { get; set; }

        public int sks { get; set; }
        public string preferensi_ruangan { get; set; }
        public string tipe_kelas { get; set; }

        public string GetKey()
        {
            return $"{kode_dosen}-{kode_matkul}-{id_kelas}-{id_waktu}-{kode_ruangan}";
        }
    }



}
