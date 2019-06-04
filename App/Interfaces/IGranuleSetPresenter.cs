using magisterka.Models;
using System.Windows.Forms;

namespace magisterka.Interfaces
{
    public interface IGranuleSetPresenter
    {
        TreeNode[] DrawTreeView(GranuleSet granuleSet);
    }
}
