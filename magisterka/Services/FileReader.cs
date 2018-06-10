using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using magisterka.Interfaces;
using magisterka.Models;

namespace magisterka.Services
{
    public class FileReader : IFileReader
    {
        private readonly char _separator = ';';

        public CoverageFile OpenAndReadFile()
        {
            var result = new CoverageFile();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Plik tekstowe|*.csv";
            openFileDialog.Title = "Wybierz plik";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                result.Path = openFileDialog.FileName;
                //try
                using (StreamReader sr = new StreamReader(openFileDialog.FileName))
                {
                    while (!sr.EndOfStream)
                    {
                        // maybe header
                        // check length row
                        var aLine = sr.ReadLine().Split(_separator);
                        var row = new List<int>();

                        for (int i = 0; i < aLine.Length - 1; i++)
                        {
                            row.Add(int.Parse(aLine[i]));
                        }

                        result.Insert(row);
                    }
                }
            }

            return result;
        }

        public bool SaveFile(List<Granula> data)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Plik tekstowe|*.csv";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                //try
                using (StreamWriter writer = new StreamWriter(saveFileDialog.OpenFile()))
                {
                    printHeader(writer, data);
                    
                    if(data.Count != 0)
                    {
                        var length = data[0].Count();

                        for (int i = 0; i < length; i++)
                        {
                            writer.Write($"u{i+1}");

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

        private void printHeader(StreamWriter writer, List<Granula> data)
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
