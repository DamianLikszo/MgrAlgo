using System;
using System.Windows.Forms;
using magisterka.Interfaces;
using magisterka.Services;

namespace magisterka
{
    public partial class Form : System.Windows.Forms.Form
    {
        public readonly IFileReader FileReader = new FileReader();
        public readonly IGranuleService GranuleService = new GranuleService();
        public readonly IZbGranService ZbGranService = new ZbGranService();

        public Form()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var data = FileReader.OpenAndReadFile();
            var zbGran = GranuleService.GenerateGran(data);
            var treeGran = ZbGranService.BuildSortedTree(zbGran);
            lResult.Text = ZbGranService.ReadResult(treeGran);
        }
        
        private void btnEnd_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
