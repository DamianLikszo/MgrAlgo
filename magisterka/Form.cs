using System;
using System.Windows.Forms;
using magisterka.Interfaces;
using magisterka.Models;

namespace magisterka
{
    public partial class Form : System.Windows.Forms.Form
    {
        private readonly Interfaces.IFileReaderService _fileReaderService;
        private readonly IGranuleSetPresenter _granuleSetPresenter;
        private readonly IFormData _formData;
        private readonly IActionService _actionService;

        private GranuleSet _granuleSet { get; set; }

        public Form(Interfaces.IFileReaderService fileReaderService, IGranuleSetPresenter granuleSetPresenter, IFormData formData,
            IActionService actionService)
        {
            _fileReaderService = fileReaderService;
            _granuleSetPresenter = granuleSetPresenter;
            _actionService = actionService;
            _formData = formData;
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (!_actionService.Load())
                return;

            _granuleSet = _formData.GranuleSet;

            txtPath.Text = _formData?.PathSource ?? "";

            treeResult.Nodes.Clear();
            var treeNodes = _granuleSetPresenter.DrawTreeView(_granuleSet);
            treeResult.Nodes.AddRange(treeNodes);
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
