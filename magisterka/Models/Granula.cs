using System.Collections.Generic;

namespace magisterka.Models
{
    public class Granula
    {
        public List<int> Inside { get; set; }
        public Granula Child { get; set; }
        public Granula Parent { get; set; }

        public Granula()
        {
            Inside = new List<int>();
        }
    }
}