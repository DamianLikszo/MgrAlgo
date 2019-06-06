using System.Collections.Generic;
using System.Windows.Forms;
using App.Interfaces;

namespace App.Services
{
    public class PrintGranSetService : IPrintGranSetService
    {
        public List<string> Print(TreeNode[] chains)
        {
            var content = new List<string>();

            foreach (var item in chains)
            {
                var line = "{" + item.Text;
                foreach (TreeNode node in item.Nodes)
                {
                    line += ", " + node.Text;
                }

                line += "}";
                content.Add(line);
            }

            return content;
        }
    }
}
