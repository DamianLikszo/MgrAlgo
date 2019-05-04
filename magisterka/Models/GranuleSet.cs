﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace magisterka.Models
{
    public class GranuleSet : ICollection<Granule>
    {
        public IList<Granule> Granules { get; set; }

        public GranuleSet()
        {
            Granules = new List<Granule>();
        }

        public GranuleSet(GranuleSet zbGran)
        {
            Granules = zbGran.Granules.ToList();
        }

        public IEnumerator<Granule> GetEnumerator()
        {
            return Granules.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Granule item)
        {
            Granules.Add(item);
        }

        public void Clear()
        {
            Granules.Clear();
        }

        public bool Contains(Granule item)
        {
            return Granules.Contains(item);
        }

        public void CopyTo(Granule[] array, int arrayIndex)
        {
            Granules.CopyTo(array, arrayIndex);
        }

        public bool Remove(Granule item)
        {
            return Granules.Remove(item);
        }

        public int Count => Granules.Count;
        public bool IsReadOnly => Granules.IsReadOnly;

        public Granule this[int index]
        {
            get => Granules[index];
            set => Granules[index] = value;
        }

        public List<Granule> GetMax() => Granules.Where(o => o.IsMax).ToList();
    }
}