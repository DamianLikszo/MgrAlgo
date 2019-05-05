using magisterka.Models;
using System.Windows.Forms;

namespace magisterka.Interfaces
{
    public interface IZbGranService
    {
        Granule SearchMin(GranuleSet granuleSet);
        GranuleSet BuildSortedTree(GranuleSet granuleSetOrg);
        string ReadResult(GranuleSet treeGran);
        TreeNode[] DrawTreeView(GranuleSet granuleSet);
    }
}
