using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Test.Helpers
{
    public class EnumerableTreeNodeComparer : IEqualityComparer<IEnumerable<TreeNode>>
    {
        public bool Equals(IEnumerable<TreeNode> x, IEnumerable<TreeNode> y)
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
                if (xList[i].Text != yList[i].Text || xList[i].Nodes.Count != yList[i].Nodes.Count)
                {
                    return false;
                }

                var xArrayNodes = xList[i].Nodes.Cast<TreeNode>().ToArray();
                var yArrayNodes = yList[i].Nodes.Cast<TreeNode>().ToArray();
                if (xArrayNodes.Length != yArrayNodes.Length)
                {
                    return false;
                }

                if (xArrayNodes.Where((t, j) => t.Text != yArrayNodes[j].Text).Any())
                {
                    return false;
                }
            }

            return true;
        }

        public int GetHashCode(IEnumerable<TreeNode> obj)
        {
            return obj.GetHashCode();
        }

        private int CalculateItemOrder(TreeNode x)
        {
            var value = x.Text.GetHashCode();
            foreach (TreeNode treeNode in x.Nodes)
            {
                value += CalculateItemOrder(treeNode);
            }

            return value;
        }
    }
}
