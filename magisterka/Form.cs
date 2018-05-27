using System;
using System.Windows.Forms;
using System.Collections.Generic;
using magisterka.Models;
using magisterka.Interfaces;
using magisterka.Services;

namespace magisterka
{
    public partial class Form : System.Windows.Forms.Form
    {
        public List<List<int>> Data { get; set; }
        public ZbGran ZbGran { get; set; }

        public readonly IFileReader FileReader = new FileReader();
        public readonly IGranuleService GranuleService = new GranuleService();

        public Form()
        {
            Restart();
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            Restart();
            Data = FileReader.OpenAndReadFile();
            ZbGran = GranuleService.GenerateGran(Data);
        }

        private void Restart()
        {
            Data = new List<List<int>>();
            ZbGran = new ZbGran();
        }
        
        private void btnEnd_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
