using System.Windows.Forms;

namespace magisterka.Wrappers
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
