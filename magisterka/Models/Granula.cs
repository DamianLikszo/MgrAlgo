using System.Collections.Generic;

namespace magisterka.Models
{
    public class Granula
    {
        public List<int> Inside { get; set; }
        public Granula Child { get; set; }
        public List<Granula> Parent { get; set; }

        public bool isMax => Parent.Count == 0;
        public bool isMin => Child == null;

        public Granula()
        {
            Inside = new List<int>();
            Parent = new List<Granula>();
        }

        public void AddToInside(int value)
        {
            Inside.Add(value);
        }
    }
}