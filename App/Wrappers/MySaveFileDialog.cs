using System.Windows.Forms;

namespace magisterka.Wrappers
{
    public class MySaveFileDialog : IMySaveFileDialog
    {
        public string FileName
        {
            get => SaveFileDialog.FileName;
            set => SaveFileDialog.FileName = value;
        }

        public string Filter
        {
            get => SaveFileDialog.Filter;
            set => SaveFileDialog.Filter = value;
        }

        public string Title
        {
            get => SaveFileDialog.Title;
            set => SaveFileDialog.Title = value;
        }

        public bool RestoreDirectory
        {
            get => SaveFileDialog.RestoreDirectory;
            set => SaveFileDialog.RestoreDirectory = value;
        }

        private SaveFileDialog SaveFileDialog { get; }

        public MySaveFileDialog()
        {
            SaveFileDialog = new SaveFileDialog();
        }

        public DialogResult ShowDialog()
        {
            return SaveFileDialog.ShowDialog();
        }
    }
}
