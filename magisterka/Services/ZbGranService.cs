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

            //if count > 2

            for (int i = 1; i < zbGran.Granules.Count; i++)
            {
                var gran = zbGran.Granules[i];

                if (result.IsLesser(gran))
                {
                    result = gran;
                    i = 0; // moze jakas optymalizacja
                }
            }

            return result;
        }

        public ZbGran BuildSortedTree(ZbGran zbGranOrg)
        {
            var zbGran = new ZbGran(zbGranOrg);
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

                    // a co jeżeli nie będzie zawierania na maks ?
                    // Powienienem wtedy sprawdzić wzdłuż łańcucha i dodać gdzieś element
                    // Trzeba się też zastanowić czy mogę mieć 2 elementy minimalne
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
