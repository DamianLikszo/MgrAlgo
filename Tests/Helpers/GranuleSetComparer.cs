using System;
using System.Collections.Generic;
using System.Linq;
using App.Models;

namespace Test.Helpers
{
    public class GranuleSetComparer : IEqualityComparer<GranuleSet>
    {
        public bool Equals(GranuleSet x, GranuleSet y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            if (x == null || y == null)
            {
                return false;
            }

            var xList = x.OrderBy(CalculateItemOrder).ToList();
            var yList = y.OrderBy(CalculateItemOrder).ToList();

            if (xList.Count != yList.Count)
            {
                return false;
            }
            
            for (var i = 0; i < xList.Count; i++)
            {
                if (xList[i].ObjectNumber != yList[i].ObjectNumber)
                {
                    return false;
                }

                if (!xList[i].SequenceEqual(yList[i]))
                {
                    return false;
                }

                if (!ChildrenAndParentCompare(xList[i], yList[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public int GetHashCode(GranuleSet obj)
        {
            return obj.GetHashCode();
        }

        private bool ChildrenAndParentCompare(Granule x, Granule y)
        {
            if (x.Children.Count != y.Children.Count || x.Parent.Count != y.Parent.Count)
            {
                return false;
            }

            var xChildren = x.Children.OrderBy(CalculateItemOrder).ToList();
            var xParents = x.Parent.OrderBy(CalculateItemOrder).ToList();
            var yChildren = y.Children.OrderBy(CalculateItemOrder).ToList();
            var yParents = y.Parent.OrderBy(CalculateItemOrder).ToList();

            return !xChildren.Where((t, j) => !t.SequenceEqual(yChildren[j])).Any() &&
                   !xParents.Where((t, j) => !t.SequenceEqual(yParents[j])).Any();
        }

        private int CalculateItemOrder(Granule granule)
        {
            var value = 0;

            for (var i = granule.Count() - 1; i >= 0; i--)
            {
                value += granule.Inside[i] * (int)Math.Pow(10, i);
            }

            return value;
        }
    }
}
