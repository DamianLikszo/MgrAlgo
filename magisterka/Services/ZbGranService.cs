using magisterka.Interfaces;
using magisterka.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using magisterka.Enums;

namespace magisterka.Services
{
    //TODO: rename to GranSetservice
    public class ZbGranService : IZbGranService
    {
        public Granule SearchMin(GranuleSet granuleSet)
        {
            if (granuleSet.Granules.Count == 0)
            {
                return null;
            }

            return granuleSet.Granules[0];
        }

        public GranuleSet BuildSortedTree(GranuleSet granuleSetOrg)
        {
            var zbGran = new GranuleSet(granuleSetOrg);
            var result = new GranuleSet();

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
                        _buildSortedTreeRef(granNew, granMax);       
                    }
                }

                result.Add(granNew);
            }

            return result;
        }

        public string ReadResult(GranuleSet granuleSet)
        {
            var result = "{\n";

            var granMax = granuleSet.GetMax();
            for (int i = 0; i < granMax.Count; i++)
            {
                if (i != 0)
                    result += ", \n";

                var listOfRoute = new List<string>();
                var previous = new List<string>();
                _getRoute(granMax[i], listOfRoute, previous);
                result += "{" + string.Join(", ", listOfRoute) + "}";
            }

            result += "\n}";

            return result;
        }

        public TreeNode[] DrawTreeView(GranuleSet granuleSet)
        {
            var result = new List<TreeNode>();

            foreach (var gran in granuleSet.GetMax())
            {
                var listOfRoute = new List<TreeNode>();
                var previous = new List<TreeNode>();
                _getBranchTree(gran, listOfRoute, previous);

                var branch = new TreeNode(gran.ToString());
                listOfRoute.Remove(listOfRoute.FirstOrDefault());

                branch.Nodes.AddRange(listOfRoute.ToArray());
                result.Add(branch);
            }

            return result.ToArray();
        }

        private void _getRoute(Granule gran, List<string> listOfRoute, List<string> previous)
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
                _getRoute(child[i], listOfRoute, previous);
            }
        }

        private void _getBranchTree(Granule gran, List<TreeNode> listOfRoute, List<TreeNode> previous)
        {
            var child = gran.Child;
            previous.Add(new TreeNode(gran.ToString()));
            
            if(child.Count == 0)
            {
                listOfRoute.AddRange(previous);
                return;
            }

            foreach (var item in child)
            {
                _getBranchTree(item, listOfRoute, previous);
            }
        }

        private void _buildSortedTreeRef(Granule granNew, Granule gran)
        {
            // NOWE: sprawdzić czy powinno byc equal
            var result = granNew.CompareTo(gran);
            if(result.Equals(GranuleComparerResult.IsGreater) || result.Equals(GranuleComparerResult.Equal))
            {
                //duplicat z innej gałęzi max
                if (!gran.Parent.Contains(granNew))
                {
                    gran.Parent.Add(granNew);
                    granNew.Child.Add(gran);
                }

                return;
            }

            foreach (var granChild in gran.Child)
            {
                _buildSortedTreeRef(granNew, granChild);
            }
        }
    }
}
