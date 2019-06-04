using System.Windows.Forms;

namespace magisterka.Wrappers
{
    public interface IMyOpenFileDialog
    {
        string FileName { get; set; }
        string Filter { get; set; }
        string Title { get; set; }

        DialogResult ShowDialog();
    }
}
