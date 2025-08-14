using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Aplikasi_Jadwal_ELEKTRO.Models;

namespace Aplikasi_Jadwal_ELEKTRO.Helpers
{
    public class ProdiSemesterGroup
    {
        public string ProdiHeader { get; set; }      // "PRODI D4 TEKNIK INFORMATIKA"
        public string SemesterHeader { get; set; }   // "SEMESTER - V"
        public string ProdiCode { get; set; }        // "TI"
        public string SemesterRomawi { get; set; }   // "V"
        public List<Kelas> KelasList { get; set; } = new List<Kelas>();
    }

    public static class KelasGroupingHelper
    {
        public static string ToRoman(int n)
        {
            var map = new[] { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII" };
            if (n < 1 || n >= map.Length) n = 1;
            return map[n];
        }

        // contoh "5 TI 1" -> "TI" (ambil token huruf pertama)
        public static string ExtractProdiCode(string namaKelas)
        {
            if (string.IsNullOrWhiteSpace(namaKelas)) return "UNK";
            var parts = Regex.Matches(namaKelas.ToUpper(), "[A-Z]+");
            return parts.Count > 0 ? parts[0].Value : "UNK";
        }

        public static string MapProdiFullName(string code)
        {
            switch (code)
            {
                case "TI": return "D4 TEKNIK INFORMATIKA";
                case "TL": return "D4 TEKNIK LISTRIK";
                case "DK": return "D3 TEKNIK LISTRIK";
                case "DKOM": return "D3 TEKNIK KOMPUTER";
                default: return code;
            }
        }

        public static List<ProdiSemesterGroup> BuildGroups(List<Kelas> kelass)
        {
            return kelass
                .GroupBy(k => new { Code = ExtractProdiCode(k.namaKelas), Sem = ToRoman(k.angkatan) })
                .Select(g => new ProdiSemesterGroup
                {
                    ProdiCode = g.Key.Code,
                    SemesterRomawi = g.Key.Sem,
                    ProdiHeader = "PRODI " + MapProdiFullName(g.Key.Code),
                    SemesterHeader = "SEMESTER - " + g.Key.Sem,
                    KelasList = g.OrderBy(k => k.namaKelas).ToList()
                })
                .OrderBy(x => x.ProdiCode)
                .ThenBy(x => x.SemesterRomawi)
                .ToList();
        }
    }
}
