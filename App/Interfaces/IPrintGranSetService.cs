using System.Collections.Generic;
using System.Windows.Forms;

namespace App.Interfaces
{
    public interface IPrintGranSetService
    {
        List<string> Print(TreeNode[] chains);
    }
}
