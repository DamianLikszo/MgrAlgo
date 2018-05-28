using System;
using System.Windows.Forms;
using magisterka.Models;
using magisterka.Interfaces;
using magisterka.Services;

namespace magisterka
{
    public partial class Form : System.Windows.Forms.Form
    {
        public ZbGran TreeGran { get; set; }

        public readonly IFileReader FileReader = new FileReader();
        public readonly IGranuleService GranuleService = new GranuleService();

        public Form()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var data = FileReader.OpenAndReadFile();
            var zbGran = GranuleService.GenerateGran(data);
            TreeGran = GranuleService.BuildSortedTree(zbGran);
        }
        
        private void btnEnd_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
