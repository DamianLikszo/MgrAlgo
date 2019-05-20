using System;
using System.Windows.Forms;
using magisterka.Interfaces;
using magisterka.Models;

namespace magisterka
{
    public partial class Form : System.Windows.Forms.Form
    {
        private readonly IGranuleSetPresenter _granuleSetPresenter;
        private readonly IActionService _actionService;

        public GranuleSetWithPath GranuleSetWithPath { get; set; }

        public Form(IGranuleSetPresenter granuleSetPresenter, IActionService actionService)
        {
            _granuleSetPresenter = granuleSetPresenter;
            _actionService = actionService;
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            GranuleSetWithPath = _actionService.Load(out var error);
            if (GranuleSetWithPath == null)
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
            if (!_actionService.SaveGranule(GranuleSetWithPath?.GranuleSet, out var error))
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
            GranuleSetWithPath = _actionService.OpenFileAndDeserializeGranuleSet(out var error);
            if (GranuleSetWithPath == null)
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
            if (!_actionService.SerializeGranuleSetAndSaveFile(GranuleSetWithPath?.GranuleSet, out var error))
            {
                if (error != null)
                {
                    MessageBox.Show(error);
                }
            }
        }

        private void RefreshSetFromService()
        {
            if (GranuleSetWithPath == null)
            {
                return;
            }

            btnSaveGran.Enabled = true;
            btnExportSet.Enabled = true;

            txtPath.Text = GranuleSetWithPath.Path;
            var treeNodes = _granuleSetPresenter.DrawTreeView(GranuleSetWithPath.GranuleSet);
            treeResult.Nodes.Clear();
            treeResult.Nodes.AddRange(treeNodes);
        }
    }
}
