using System.Collections.Generic;

namespace magisterka.Models
{
    //ienumerator po inside
    public class Granula
    {
        public List<int> Inside { get; set; }
        public List<Granula> Child { get; set; }
        public List<Granula> Parent { get; set; }

        public bool isMax => Parent.Count == 0;
        public bool isMin => Child.Count == 0;

        public Granula()
        {
            Inside = new List<int>();
            Parent = new List<Granula>();
            Child = new List<Granula>();
        }

        public void AddToInside(int value) => Inside.Add(value);
        public int Count() => Inside.Count;

        public override string ToString() => "{" + string.Join(", ", Inside) + "}";
    }
}