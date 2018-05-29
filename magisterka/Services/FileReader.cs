using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using magisterka.Interfaces;
using magisterka.Models;

namespace magisterka.Services
{
    public class FileReader : IFileReader
    {

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
                        var aLine = sr.ReadLine().Split(';');
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
    }
}
