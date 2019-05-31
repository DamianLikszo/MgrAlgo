using System;
using System.Collections.Generic;
using System.Linq;
using magisterka.Models;

namespace Test.Helpers
{
    public class EnumerableGranuleDtoComparer : IEqualityComparer<IEnumerable<GranuleDto>>
    {
        public bool Equals(IEnumerable<GranuleDto> x, IEnumerable<GranuleDto> y)
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
                if (!xList[i].Inside.SequenceEqual(yList[i].Inside))
                {
                    return false;
                }

                if (!ChildrenCompare(xList[i], yList[i]))
                {
                    return false;
                }

            }

            return true;
        }

        public int GetHashCode(IEnumerable<GranuleDto> obj)
        {
            return obj.GetHashCode();
        }

        private bool ChildrenCompare(GranuleDto x, GranuleDto y)
        {
            if (x.Children == null && y.Children == null)
            {
                return true;
            }

            if (x.Children == null || y.Children == null || x.Children.Length != y.Children.Length)
            {
                return false;
            }

            return x.Children.All(childX => y.Children.Any(p => p.SequenceEqual(childX)));
        }

        private int CalculateItemOrder(GranuleDto granuleDto)
        {
            var value = 0;

            for (var i = granuleDto.Inside.Count() - 1; i >= 0; i--)
            {
                value += granuleDto.Inside[i] * (int)Math.Pow(10, i);
            }

            return value;
        }
    }
}
