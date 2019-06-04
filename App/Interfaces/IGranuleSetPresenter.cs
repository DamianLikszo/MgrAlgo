using App.Models;
using System.Windows.Forms;

namespace App.Interfaces
{
    public interface IGranuleSetPresenter
    {
        TreeNode[] DrawTreeView(GranuleSet granuleSet);
    }
}
