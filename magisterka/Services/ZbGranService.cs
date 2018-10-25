using magisterka.Interfaces;
using magisterka.Models;
using System.Collections.Generic;
using System.Linq;

namespace magisterka.Services
{
    public class ZbGranService : IZbGranService
    {
        public readonly IGranuleService GranuleService = new GranuleService();

        public Granula SearchMin(ZbGran zbGran)
        {
            if (zbGran.Granules.Count == 0)
            {
                return null;
            }

            return zbGran.Granules[0];
        }

        public ZbGran BuildSortedTree(ZbGran zbGranOrg)
        {
            var zbGran = new ZbGran(zbGranOrg);
            var result = new ZbGran();

            while (zbGran.Granules.Count > 0)
            {
                var granNew = SearchMin(zbGran);

                if (granNew == null)
                {
                    break;
                }

                zbGran.Remove(granNew);

                if (result.Granules.Count != 0)
                {
                    foreach (var granMax in result.GetMax())
                    {
                        BuildSortedTreeRef(granNew, granMax);       
                    }
                }

                result.Add(granNew);
            }

            return result;
        }

        public string ReadResult(ZbGran treeGran)
        {
            var result = "{\n";

            var granMax = treeGran.GetMax();
            for (int i = 0; i < granMax.Count; i++)
            {
                if (i != 0)
                    result += ", \n";

                var listOfRoute = new List<string>();
                var previous = new List<string>();
                getRoute(granMax[i], listOfRoute, previous);
                result += "{" + string.Join(", ", listOfRoute) + "}";
            }

            result += "\n}";

            return result;
        }

        public void SortZbGran(ZbGran zbGran)
        {
            zbGran.Granules.Sort((x, y) => x.Inside.Count(p => p == 1).CompareTo(y.Inside.Count(p => p == 1)));
        }

        private void getRoute(Granula gran, List<string> listOfRoute, List<string> previous)
        {
            var child = gran.Child;
            previous.Add(gran.ToString());

            if (child.Count == 0)
            {
                var route = string.Join(", ", previous);
                listOfRoute.Add(route);
                return;
            }

            for (int i = 0; i < child.Count; i++)
            {
                getRoute(child[i], listOfRoute, previous);
            }
        }

        private void BuildSortedTreeRef(Granula granNew, Granula gran)
        {
            if (GranuleService.IsGreaterOrEqual(granNew, gran))
            {
                //duplicat z innej gałęzi max
                if(!gran.Parent.Contains(granNew))
                {
                    gran.Parent.Add(granNew);
                    granNew.Child.Add(gran);
                }

                return;
            }

            foreach (var granChild in gran.Child)
            {
                BuildSortedTreeRef(granNew, granChild);
            }
        }
    }
}
