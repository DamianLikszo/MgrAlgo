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
        private readonly IMyOpenFileDialog _openFileDialog;
        private readonly IMySaveFileDialog _saveFileDialog;

        public static readonly string CsvFilter = "Pliki tekstowe|*.csv";
        public static readonly string JsonFilter = "Pliki tekstowe|*.json";
        
        public FileService(IMyStreamReader streamReader, IMyOpenFileDialog openFileDialog,
            IMySaveFileDialog saveFileDialog, IMyStreamWriter streamWriter)
        {
            _streamReader = streamReader;
            _streamWriter = streamWriter;
            _openFileDialog = openFileDialog;
            _saveFileDialog = saveFileDialog;
        }

        public List<string> ReadFile(string path, out string error)
        {
            error = null;
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
                error = ex.Message;
                return null;
            }

            return result;
        }
        
        public string GetPathFromOpenFileDialog(string filter = null)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                _openFileDialog.Filter = filter;
            }
            _openFileDialog.Title = "Wybierz plik";
            _saveFileDialog.RestoreDirectory = true;

            return _openFileDialog.ShowDialog() == DialogResult.OK ? _openFileDialog.FileName : null;
        }

        public bool SaveFile(string path, List<string> content, out string error)
        {
            error = null;

            if (string.IsNullOrEmpty(path))
            {
                error = "Pusta ścieżka do pliku.";
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
                error = ex.Message;
                return false;
            }
            
            return true;
        }

        public string GetPathFromSaveFileDialog(string filter = null)
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
