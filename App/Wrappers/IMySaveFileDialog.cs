﻿using System.Windows.Forms;

namespace App.Wrappers
{
    public interface IMySaveFileDialog
    {
        string FileName { get; set; }
        string Filter { get; set; }
        string Title { get; set; }
        bool RestoreDirectory { get; set; }

        DialogResult ShowDialog();
    }
}
