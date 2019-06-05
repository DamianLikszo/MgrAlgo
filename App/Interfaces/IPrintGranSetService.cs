using System.Collections.Generic;
using System.Windows.Forms;

namespace magisterka.Interfaces
{
    public interface IPrintGranSetService
    {
        List<string> Print(TreeNode[] chains);
    }
}
