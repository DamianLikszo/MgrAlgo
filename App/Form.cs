using System;
using System.Windows.Forms;
using App.Interfaces;
using App.Models;
using App.Wrappers;

namespace App
{
    public partial class Form : System.Windows.Forms.Form
    {
        private readonly IGranuleSetPresenter _granuleSetPresenter;
        private readonly IActionService _actionService;
        private readonly IMyMessageBox _myMessageBox;

        public GranuleSetWithPath GranuleSetWithPath { get; set; }

        public Form(IGranuleSetPresenter granuleSetPresenter, IActionService actionService,
            IMyMessageBox myMessageBox)
        {
            _granuleSetPresenter = granuleSetPresenter;
            _actionService = actionService;
            _myMessageBox = myMessageBox;
            InitializeComponent();
        }

        public void btnLoad_Click(object sender, EventArgs e)
        {
            var granuleSet = _actionService.Load(out var error);
            if (granuleSet == null)
            {
                _myMessageBox.Show(error);
                return;
            }

            GranuleSetWithPath = granuleSet;
            RefreshSetFromService();
        }

        public void btnEnd_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void btnSaveGran_Click(object sender, EventArgs e)
        {
            if (!_actionService.SaveGranule(GranuleSetWithPath?.GranuleSet, out var error))
            {
                _myMessageBox.Show(error);
            }
        }

        public void btnInfo_Click(object sender, EventArgs e)
        {
            //TODO: Information
        }

        public void btnImportSet_Click(object sender, EventArgs e)
        {
            var granuleSet = _actionService.OpenFileAndDeserializeGranuleSet(out var error);
            if (granuleSet == null)
            {
                _myMessageBox.Show(error);
                return;
            }

            GranuleSetWithPath = granuleSet;
            RefreshSetFromService();
        }

        public void btnExportSet_Click(object sender, EventArgs e)
        {
            if (!_actionService.SerializeGranuleSetAndSaveFile(GranuleSetWithPath?.GranuleSet, out var error))
            {
                _myMessageBox.Show(error);
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
