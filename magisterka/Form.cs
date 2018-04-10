using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace magisterka
{
    public partial class Form : System.Windows.Forms.Form
    {
        public List<List<int>> Data { get; set; }
        public List<int> Decission { get; set; }

        public Form()
        {
            Data = new List<List<int>>();
            Decission = new List<int>();

            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            Data = new List<List<int>>();
            Decission = new List<int>();



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
                        var last = aLine.Length - 1;

                        Decission.Add(int.Parse(aLine[last]));

                        for (int i = 0; i < last; i++)
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
