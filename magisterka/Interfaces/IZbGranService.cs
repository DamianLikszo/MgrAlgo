using magisterka.Models;
using System.Windows.Forms;

namespace magisterka.Interfaces
{
    public interface IZbGranService
    {
        Granula SearchMin(ZbGran zbGran);
        void SortZbGran(ZbGran zbGran);
        ZbGran BuildSortedTree(ZbGran zbGranOrg);
        string ReadResult(ZbGran treeGran);
        TreeNode[] DrawTreeView(ZbGran zbGran);
    }
}
