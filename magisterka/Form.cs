using System;
using System.Windows.Forms;
using magisterka.Interfaces;
using magisterka.Models;
using magisterka.Services;

namespace magisterka
{
    public partial class Form : System.Windows.Forms.Form
    {
        public readonly IFileReader FileReader = new FileReader();
        public readonly IGranuleService GranuleService = new GranuleService();
        public readonly IZbGranService ZbGranService = new ZbGranService();
        public readonly IDevService devService = new DevService();

        private ZbGran _zbGran { get; set; }

        public Form()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var coverage = FileReader.OpenAndReadFile();
            _zbGran = GranuleService.GenerateGran(coverage.Data);
            //DEV 
            _zbGran = devService.pushGran();
            ZbGranService.SortZbGran(_zbGran);
            var treeGran = ZbGranService.BuildSortedTree(_zbGran);
            txtPath.Text = coverage.Path;
            treeResult.Nodes.Clear();
            treeResult.Nodes.AddRange(ZbGranService.DrawTreeView(treeGran));
        }
        
        private void btnEnd_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSaveGran_Click(object sender, EventArgs e)
        {
            if (_zbGran == null)
                return;

            FileReader.SaveFile(_zbGran.Granules);
        }
    }
}
