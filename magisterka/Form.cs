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

        private GranuleSet GranuleSet { get; set; }

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
            CleanForm();
            if (!_actionService.Load(out var error))
            {
                //TODO: show error
                return;
            }

            GranuleSet = _formData.GranuleSet;
            txtPath.Text = _formData.PathSource;
            btnSaveGran.Enabled = true;

            var treeNodes = _granuleSetPresenter.DrawTreeView(GranuleSet);
            treeResult.Nodes.AddRange(treeNodes);
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSaveGran_Click(object sender, EventArgs e)
        {
            if (GranuleSet == null)
            {
                return;
            }

            _fileReaderService.SaveFile(GranuleSet, out var error);
            //TODO: show error
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            //TODO: Information
        }

        private void CleanForm()
        {
            _formData.GranuleSet = null;
            _formData.PathSource = null;
            btnSaveGran.Enabled = false;
            treeResult.Nodes.Clear();
            txtPath.Text = "";
        }
    }
}
