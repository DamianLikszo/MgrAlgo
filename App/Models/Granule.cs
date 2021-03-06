﻿using System;
using System.Collections;
using System.Collections.Generic;
using App.Compares;

namespace App.Models
{
    public class Granule : IEnumerable<int>, IComparable<Granule>
    {
        public int ObjectNumber { get; }
        public List<int> Inside { get; }
        public List<Granule> Children { get; set; }
        public List<Granule> Parent { get; set; }

        public bool IsMax => Parent.Count == 0;
        public bool IsMin => Children.Count == 0;

        public Granule(IEnumerable<int> inside, int objectNumber)
        {
            ObjectNumber = objectNumber;
            Inside = new List<int>(inside);
            Parent = new List<Granule>();
            Children = new List<Granule>();
        }
        
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

        public override string ToString() => "g(u" + ObjectNumber + ") = {" + string.Join(", ", Inside) + "}";

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