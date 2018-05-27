using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using magisterka.Models;

namespace magisterka
{
    public partial class Form : System.Windows.Forms.Form
    {
        public List<List<int>> Data { get; set; }
        public ZbGran ZbGran { get; set; }

        public Form()
        {
            Restart();
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            Restart();
            OpenAndReadFile();
            GenerateGran();
        }

        private void Restart()
        {
            Data = new List<List<int>>();
            ZbGran = new ZbGran();
        }

        private void GenerateGran()
        {
            // sprawdzenie po kórych indeksach sprawdzamy
            List<List<int>> indexSelect = new List<List<int>>();
            foreach (var row in Data)
            {
                var rowIndexSelect = new List<int>();
                for (int i = 0; i < row.Count; i++)
                {
                    if (row[i] == 1)
                        rowIndexSelect.Add(i);
                }
                indexSelect.Add(rowIndexSelect);
            }

            // porównywanie min
            // ? czy indexSelect moze byc 0 ?
            for (int u = 0; u < Data.Count; u++)
            {
                int result;
                var granule = new Granula();
                for (int i = 0; i < Data.Count; i++)
                {
                    result = 1;
                    foreach (var index in indexSelect[i])
                    {
                        result = Math.Min(result, Data[u][index]);
                    }
                    granule.AddToInside(result);
                }
                ZbGran.Add(granule);
            }
        }

        private void OpenAndReadFile()
        {
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

                        Data.Add(row);
                    }
                }
            }
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
