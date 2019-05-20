using System;
using System.Windows.Forms;
using magisterka.Interfaces;

namespace magisterka
{
    public partial class Form : System.Windows.Forms.Form
    {
        private readonly IGranuleSetPresenter _granuleSetPresenter;
        private readonly IFormData _formData;
        private readonly IActionService _actionService;

        public Form(IGranuleSetPresenter granuleSetPresenter, IFormData formData, IActionService actionService)
        {
            _granuleSetPresenter = granuleSetPresenter;
            _actionService = actionService;
            _formData = formData;
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (!_actionService.Load(out var error))
            {
                if (error != null)
                {
                    MessageBox.Show(error);
                }

                return;
            }

            RefreshSetFromService();
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSaveGran_Click(object sender, EventArgs e)
        {
            if (!_actionService.SaveGranule(out var error))
            {
                if (error != null)
                {
                    MessageBox.Show(error);
                }
            }
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            //TODO: Information
        }

        private void btnImportSet_Click(object sender, EventArgs e)
        {
            if (!_actionService.OpenFileAndDeserializeGranuleSet(out var error))
            {
                if (error != null)
                {
                    MessageBox.Show(error);
                }

                return;
            }

            RefreshSetFromService();
        }

        private void btnExportSet_Click(object sender, EventArgs e)
        {
            if (!_actionService.SerializeGranuleSetAndSaveFile(out var error))
            {
                if (error != null)
                {
                    MessageBox.Show(error);
                }
            }
        }

        private void RefreshSetFromService()
        {
            if (_formData.GranuleSet == null || string.IsNullOrEmpty(_formData.PathSource))
            {
                return;
            }

            btnSaveGran.Enabled = true;
            btnExportSet.Enabled = true;

            txtPath.Text = _formData.PathSource;
            var treeNodes = _granuleSetPresenter.DrawTreeView(_formData.GranuleSet);
            treeResult.Nodes.Clear();
            treeResult.Nodes.AddRange(treeNodes);
        }
    }
}
