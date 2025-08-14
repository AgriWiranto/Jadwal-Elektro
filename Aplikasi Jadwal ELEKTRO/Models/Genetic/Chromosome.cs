using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikasi_Jadwal_ELEKTRO.Models.Genetic
{
    public class Chromosome
    {
        public List<Gene> Genes { get; set; } = new List<Gene>();
        public int Fitness { get; set; } = 0;

        public Chromosome Clone()
        {
            return new Chromosome
            {
                Genes = new List<Gene>(this.Genes),
                Fitness = this.Fitness
            };
        }
    }
}
