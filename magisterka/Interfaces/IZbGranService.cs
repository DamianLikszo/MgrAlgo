using magisterka.Models;
using System.Windows.Forms;

namespace magisterka.Interfaces
{
    public interface IZbGranService
    {
        string ReadResult(GranuleSet treeGran);
        TreeNode[] DrawTreeView(GranuleSet granuleSet);
    }
}
