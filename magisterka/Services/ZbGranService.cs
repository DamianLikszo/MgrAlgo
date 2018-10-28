using magisterka.Interfaces;
using magisterka.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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
                        _buildSortedTreeRef(granNew, granMax);       
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
                _getRoute(granMax[i], listOfRoute, previous);
                result += "{" + string.Join(", ", listOfRoute) + "}";
            }

            result += "\n}";

            return result;
        }

        public TreeNode[] DrawTreeView(ZbGran zbGran)
        {
            var result = new List<TreeNode>();

            foreach (var gran in zbGran.GetMax())
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

        public void SortZbGran(ZbGran zbGran)
        {
            zbGran.Granules.Sort((x, y) => x.Inside.Count(p => p == 1).CompareTo(y.Inside.Count(p => p == 1)));
        }

        private void _getRoute(Granula gran, List<string> listOfRoute, List<string> previous)
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

        private void _getBranchTree(Granula gran, List<TreeNode> listOfRoute, List<TreeNode> previous)
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

        private void _buildSortedTreeRef(Granula granNew, Granula gran)
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
                _buildSortedTreeRef(granNew, granChild);
            }
        }
    }
}
