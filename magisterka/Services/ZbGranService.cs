using magisterka.Interfaces;
using magisterka.Models;
using System.Collections.Generic;

namespace magisterka.Services
{
    public class ZbGranService : IZbGranService
    {
        public Granula SearchMin(ZbGran zbGran)
        {
            Granula result = zbGran.Granules[0];

            for (int i = 0; i < zbGran.Granules.Count; i++)
            {
                var gran = zbGran.Granules[i];

                if (gran.IsLesser(result))
                {
                    result = gran;
                    i = 0;
                }
            }

            return result;
        }

        public ZbGran BuildSortedTree(ZbGran zbGran)
        {
            var result = new ZbGran();

            while (zbGran.Granules.Count > 0)
            {
                var gran = SearchMin(zbGran);
                zbGran.Remove(gran);

                if (result.Granules.Count != 0)
                {
                    foreach (var granMax in result.GetMax())
                    {
                        if (gran.IsGreaterOrEqual(granMax))
                        {
                            granMax.Parent.Add(gran);
                            gran.Child.Add(granMax);
                        }
                    }
                }

                result.Add(gran);
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
    }
}
