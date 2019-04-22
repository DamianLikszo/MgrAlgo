using System.Windows.Forms;

namespace magisterka.Wrappers
{
    public class MyOpenFileDialog : IMyOpenFileDialog
    {
        public string FileName
        {
            get => OpenFileDialog.FileName;
            set => OpenFileDialog.FileName = value;
        }

        public string Filter
        {
            get => OpenFileDialog.Filter;
            set => OpenFileDialog.Filter = value;
        }

        public string Title
        {
            get => OpenFileDialog.Title;
            set => OpenFileDialog.Title = value;
        }

        private OpenFileDialog OpenFileDialog { get; }

        public MyOpenFileDialog()
        {
            OpenFileDialog = new OpenFileDialog();
        }

        public DialogResult ShowDialog()
        {
            return OpenFileDialog.ShowDialog();
        }
    }
}
