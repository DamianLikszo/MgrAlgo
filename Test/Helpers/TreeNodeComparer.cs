using System.Collections.Generic;
using System.Windows.Forms;

namespace Test.Helpers
{
    public class TreeNodeComparer : IEqualityComparer<TreeNode>
    {
        public bool Equals(TreeNode x, TreeNode y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null || x.Text != y.Text || x.Nodes.Count != y.Nodes.Count)
            {
                return false;
            }

            var count = x.Nodes.Count;
            for (var i = 0; i < count; i++)
            {
                var equal = Equals(x.Nodes[i], y.Nodes[i]);
                if(!equal)
                {
                    return false;
                }
            }

            return true;
        }
        
        public int GetHashCode(TreeNode obj)
        {
            return obj.GetHashCode();
        }
    }
}
