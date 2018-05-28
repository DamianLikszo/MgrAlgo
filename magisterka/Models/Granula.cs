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

        public bool IsLesser(Granula gran)
        {
            if (Inside.Count != gran.Inside.Count)
                return false;

            for (int i = 0; i < Inside.Count; i++)
            {
                if (gran.Inside[i] >= Inside[i])
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsGreaterOrEqual(Granula gran)
        {
            if (Inside.Count != gran.Inside.Count)
                return false;

            for (int i = 0; i < Inside.Count; i++)
            {
                if (gran.Inside[i] < Inside[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            var result = "{";
            
            for (int i = 0; i < Inside.Count; i++)
            {
                if (i != 0)
                    result += ", ";

                result += Inside[i];
            }

            result += "}";

            return result;
        }
    }
}