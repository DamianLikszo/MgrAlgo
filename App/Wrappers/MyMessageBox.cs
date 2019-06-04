using System.Windows.Forms;

namespace magisterka.Wrappers
{
    public class MyMessageBox : IMyMessageBox
    {
        public bool Show(string message)
        {
            if (message == null)
            {
                return false;
            }

            MessageBox.Show(message, "Problem");
            return true;
        }
    }
}
