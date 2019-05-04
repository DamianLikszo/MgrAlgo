using System;
using System.Windows.Forms;
using magisterka.Interfaces;
using magisterka.Models;

namespace magisterka
{
    public partial class Form : System.Windows.Forms.Form
    {
        private readonly Interfaces.IFileReaderService _fileReaderService;
        private readonly IZbGranService _zbGranService;
        private readonly IFormData _formData;
        private readonly IActionService _actionService;

        private GranuleSet _granuleSet { get; set; }

        public Form(Interfaces.IFileReaderService fileReaderService, IZbGranService zbGranService, IFormData formData,
            IActionService actionService)
        {
            _fileReaderService = fileReaderService;
            _zbGranService = zbGranService;
            _actionService = actionService;
            _formData = formData;
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (!_actionService.Load())
                return;

            //TODO: tu chyba tez bedzie check null
            _granuleSet = _formData.GranuleSet;
            _zbGranService.SortZbGran(_granuleSet);

            var treeGran = _zbGranService.BuildSortedTree(_granuleSet);
            //TODO: change path
            //txtPath.Text = coverage?.Path ?? "";

            treeResult.Nodes.Clear();
            treeResult.Nodes.AddRange(_zbGranService.DrawTreeView(treeGran));
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSaveGran_Click(object sender, EventArgs e)
        {
            if (_granuleSet == null)
                return;

            //TODO: fix
            //_fileReaderService.SaveFile(_zbGran);
        }
    }
}
