using magisterka.Interfaces;
using System.Windows.Forms;

namespace magisterka.Services
{
    public class MyMessageBox : IMyMessageBox
    {
        public bool Show(string message)
        {
            MessageBox.Show(message);
            return true;
        }
    }
}
