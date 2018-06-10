using System.Collections.Generic;
using System.Linq;

namespace magisterka.Models
{
    //ienumerator po granules
    public class ZbGran
    {
        public List<Granula> Granules { get; set; }

        public ZbGran()
        {
            Granules = new List<Granula>();
        }

        public ZbGran(ZbGran zbGran) : base()
        {
            Granules = zbGran.Granules.ToList();
        }

        public void Add(Granula gran) => Granules.Add(gran);
        public void Remove(Granula gran) => Granules.Remove(gran);
        public List<Granula> GetMax() => Granules.Where(o => o.isMax).ToList();
        public int Count() => Granules.Count();
    }
}
