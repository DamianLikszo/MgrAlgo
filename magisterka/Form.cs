using System;
using System.Windows.Forms;
using magisterka.Interfaces;
using magisterka.Models;

namespace magisterka
{
    public partial class Form : System.Windows.Forms.Form
    {
        private readonly Interfaces.IFileReaderService fileReaderService;
        private readonly IGranuleService granuleService;
        private readonly IZbGranService zbGranService;
        private readonly IDevService devService;

        private ZbGran _zbGran { get; set; }

        public Form(Interfaces.IFileReaderService fileReaderService, IGranuleService granuleService, IZbGranService zbGranService,
            IDevService devService)
        {
            this.fileReaderService = fileReaderService;
            this.granuleService = granuleService;
            this.zbGranService = zbGranService;
            this.devService = devService;
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var coverage = fileReaderService.OpenAndReadFile();
            if(coverage == null)
                return;

            //TODO: tu chyba tez bedzie check null
            _zbGran = granuleService.GenerateGran(coverage.Data);
            zbGranService.SortZbGran(_zbGran);

            var treeGran = zbGranService.BuildSortedTree(_zbGran);
            txtPath.Text = coverage?.Path ?? "";

            treeResult.Nodes.Clear();
            treeResult.Nodes.AddRange(zbGranService.DrawTreeView(treeGran));
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSaveGran_Click(object sender, EventArgs e)
        {
            if (_zbGran == null)
                return;

            fileReaderService.SaveFile(_zbGran.Granules);
        }
    }
}
