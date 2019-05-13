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
        private readonly IMyStreamWriter _streamWriter;
        private readonly IMyMessageBox _myMessageBox;
        private readonly IMyOpenFileDialog _openFileDialog;
        private readonly IMySaveFileDialog _saveFileDialog;

        public static readonly string CsvFilter = "Pliki tekstowe|*.csv";
        public static readonly string JsonFilter = "Pliki tekstowe|*.json";
        
        public FileService(IMyStreamReader streamReader, IMyMessageBox messageBox, IMyOpenFileDialog openFileDialog,
            IMySaveFileDialog saveFileDialog, IMyStreamWriter streamWriter)
        {
            _streamReader = streamReader;
            _streamWriter = streamWriter;
            _myMessageBox = messageBox;
            _openFileDialog = openFileDialog;
            _saveFileDialog = saveFileDialog;
        }

        //TODO: maybe out error
        public List<string> ReadFile(string path)
        {
            var result = new List<string>();

            try
            {
                using (var reader = _streamReader.GetReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
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
        
        //TODO: add tests for filter
        public string GetPathFromOpenFileDialog(string filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                _openFileDialog.Filter = filter;
            }
            _openFileDialog.Title = "Wybierz plik";

            return _openFileDialog.ShowDialog() == DialogResult.OK ? _openFileDialog.FileName : null;
        }

        public bool SaveFile(string path, List<string> content)
        {
            if (string.IsNullOrEmpty(path))
            {
                _myMessageBox.Show("Wrong empty file path.");
                return false;
            }

            try
            {
                using (var writer = _streamWriter.GetStreamWriter(path))
                {
                    foreach (var line in content)
                    {
                        writer.WriteLine(line);
                    }

                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                _myMessageBox.Show(ex.Message);
                return false;
            }
            
            return true;
        }

        //TODO: add tests for filter
        public string GetPathFromSaveFileDialog(string filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                _saveFileDialog.Filter = filter;
            }
            _saveFileDialog.Title = "Wybierz plik";
            _saveFileDialog.RestoreDirectory = true;

            return _saveFileDialog.ShowDialog() == DialogResult.OK ? _saveFileDialog.FileName : null;
        }
    }
}
