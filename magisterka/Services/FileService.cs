using System;
using System.Collections.Generic;
using System.Windows.Forms;
using magisterka.Interfaces;
using magisterka.Wrappers;

namespace magisterka.Services
{
    public class FileService : IFileService
    {
        private readonly IMyStreamReader _streamReader;
        private readonly IMyMessageBox _myMessageBox;
        private readonly IMyOpenFileDialog _openFileDialog;

        public FileService(IMyStreamReader streamReader, IMyMessageBox messageBox, IMyOpenFileDialog openFileDialog)
        {
            _streamReader = streamReader;
            _myMessageBox = messageBox;
            _openFileDialog = openFileDialog;
        }

        public List<string> ReadFile(string path)
        {
            var result = new List<string>();

            try
            {
                using (var sr = _streamReader.GetReader(path))
                {
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        result.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                _myMessageBox.Show(ex.Message);
                return null;
            }

            return result;
        }

        public string SelectFile()
        {
            string filePath = null;

            _openFileDialog.Filter = "Plik tekstowe|*.csv";
            _openFileDialog.Title = "Wybierz plik";

            if (_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = _openFileDialog.FileName;
            }

            return filePath;
        }
    }
}
