using System.Collections.Generic;
using System.Windows.Forms;

namespace Test.Helpers
{
    public class SortTreeNodeInSameOrderComparer : IComparer<TreeNode>
    {
        public int Compare(TreeNode x, TreeNode y)
        {
            var xValue = GetHashCodeFromTree(x);
            var yValue = GetHashCodeFromTree(y);

            if (xValue == yValue)
            {
                return 0;
            }
            if (xValue > yValue)
            {
                return 1;
            }

            return -1;
        }

        private int GetHashCodeFromTree(TreeNode x)
        {
            var value = x.Text.GetHashCode();
            foreach (TreeNode treeNode in x.Nodes)
            {
                value += GetHashCodeFromTree(treeNode);
            }

            return value;
        }
    }
}
