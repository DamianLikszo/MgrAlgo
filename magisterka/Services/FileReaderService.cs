using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using magisterka.Interfaces;
using magisterka.Models;
using magisterka.Validators;

namespace magisterka.Services
{
    public class FileReaderService : Interfaces.IFileReaderService
    {
        private readonly IStreamReader _streamReader;
        private readonly IMyMessageBox _myMessageBox;
        private readonly ICoverageFileValidator _coverageFileValidator;
        private readonly char _separator = ';';

        public FileReaderService(IStreamReader streamReader, IMyMessageBox myMessageBox,
            ICoverageFileValidator coverageFileValidator)
        {
            _streamReader = streamReader;
            _myMessageBox = myMessageBox;
            _coverageFileValidator = coverageFileValidator;
        }

        public CoverageFile OpenAndReadFile()
        {
            CoverageFile result = null;

            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Plik tekstowe|*.csv";
            openFileDialog.Title = "Wybierz plik";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var path = openFileDialog.FileName;
                var content = ReadFile(path);
                var data = ConvertContentToData(content);

                result = new CoverageFile(path, data);

                if (!_coverageFileValidator.ValidAndShow(result, _myMessageBox))
                {
                    return null;
                }
            }

            return result;
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

        public List<List<int>> ConvertContentToData(List<string> content)
        {
            var result = new List<List<int>>();
           
            foreach (var line in content)
            {
                var data = line.Split(_separator);
                var row = new List<int>();

                foreach (var item in data)
                {
                    if (!int.TryParse(item, out var number))
                    {
                        _myMessageBox.Show("Nieprawidłowy zestaw danych. Wiersze zawierają inne dane niż liczby.");
                        return null;
                    }

                    row.Add(number);
                }

                result.Add(row);
            }
            
            return result;
        }

        //TODO: other File
        public bool SaveFile(List<Granula> data)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Plik tekstowe|*.csv";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.OpenFile()))
                {
                    _printHeader(writer, data);

                    if (data.Count != 0)
                    {
                        var length = data[0].Count();

                        for (int i = 0; i < length; i++)
                        {
                            writer.Write($"u{i + 1}");

                            foreach (var granula in data)
                            {
                                writer.Write(_separator + granula.Inside[i].ToString());
                            }

                            writer.WriteLine();
                        }
                    }

                    writer.Close();
                }
            }

            return true;
        }

        private void _printHeader(StreamWriter writer, List<Granula> data)
        {
            writer.Write("obiekt/g(obiekt)");

            for (int i = 0; i < data.Count; i++)
            {
                writer.Write(_separator + $"g(u{i+1})");
            }

            writer.WriteLine();
        }
    }
}
