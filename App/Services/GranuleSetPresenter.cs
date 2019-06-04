using App.Interfaces;
using App.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace App.Services
{
    public class GranuleSetPresenter : IGranuleSetPresenter
    {
        public TreeNode[] DrawTreeView(GranuleSet granuleSet)
        {
            var result = new List<TreeNode>();

            foreach (var granule in granuleSet.GetHighest())
            {
                var branches = _getBranches(granule);

                foreach (var branch in branches)
                {
                    var root = new TreeNode(branch[0].ToString());
                    branch.Remove(branch[0]);

                    var treeNodes = branch.Select(x => new TreeNode(x.ToString())).ToArray();
                    root.Nodes.AddRange(treeNodes);
                    result.Add(root);
                }
            }

            return result.ToArray();
        }

        private List<List<Granule>> _getBranches(Granule granule)
        {
            var branch = new List<Granule>(new[] {granule});
            var result = new List<List<Granule>>();

            if (granule.Children.Count == 0)
            {
                result.Add(branch);
                return result;
            }

            foreach (var child in granule.Children)
            {
                var partBranches = _getBranches(child);

                foreach (var partBranch in partBranches)
                {
                    var fullBranch = new List<Granule>(branch);
                    fullBranch.AddRange(partBranch);
                    result.Add(fullBranch);
                }
            }

            return result;
        }
    }
}
