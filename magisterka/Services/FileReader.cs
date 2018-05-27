using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using magisterka.Interfaces;

namespace magisterka.Services
{
    public class FileReader : IFileReader
    {

        public List<List<int>> OpenAndReadFile()
        {
            var result = new List<List<int>>();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Plik tekstowe|*.csv";
            openFileDialog.Title = "Wybierz plik";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
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

                        result.Add(row);
                    }
                }
            }

            return result;
        }
    }
}
