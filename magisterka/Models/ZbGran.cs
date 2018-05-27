using System.Collections.Generic;

namespace magisterka.Models
{
    public class ZbGran
    {
        public List<Granula> Granules { get; set; }

        public ZbGran()
        {
            Granules = new List<Granula>();
        }

        public void Add(Granula gran)
        {
            Granules.Add(gran);
        }
    }
}
