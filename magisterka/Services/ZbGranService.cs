using magisterka.Interfaces;
using magisterka.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace magisterka.Services
{
    //TODO: rename to GranSetservice
    public class ZbGranService : IZbGranService
    {

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
    }
}
