using System;
using System.Collections;
using System.Collections.Generic;

namespace magisterka.Models
{
    public class Granule : IEnumerable<int>, IComparable<Granule>
    {
        private List<int> Inside { get; set; }
        public List<Granule> Child { get; set; }
        public List<Granule> Parent { get; set; }

        public bool IsMax => Parent.Count == 0;
        public bool IsMin => Child.Count == 0;

        public Granule()
        {
            Inside = new List<int>();
            Parent = new List<Granule>();
            Child = new List<Granule>();
        }

        public Granule(List<int> insideList)
        {
            Inside = insideList;
            Parent = new List<Granule>();
            Child = new List<Granule>();
        }

        public void AddToInside(int value) => Inside.Add(value);
        public int Count() => Inside.Count;

        public IEnumerator<int> GetEnumerator()
        {
            return Inside.GetEnumerator();
        }

        public int CompareTo(Granule other)
        {
            var comparer = new GranuleComparer();
            return comparer.Compare(this, other);
        }

        public override string ToString() => "{" + string.Join(", ", Inside) + "}";

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int this[int index]
        {
            get => Inside[index];
            set => Inside[index] = value;
        }
    }
}