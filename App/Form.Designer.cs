﻿namespace App
{
    partial class Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnEnd = new System.Windows.Forms.Button();
            this.gbGraph = new System.Windows.Forms.GroupBox();
            this.treeResult = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gbAbout = new System.Windows.Forms.GroupBox();
            this.lNameProgram = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.lPath = new System.Windows.Forms.Label();
            this.btnSaveGran = new System.Windows.Forms.Button();
            this.btnSaveChains = new System.Windows.Forms.Button();
            this.gbGraph.SuspendLayout();
            this.gbAbout.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold);
            this.btnLoad.Location = new System.Drawing.Point(12, 12);
            this.btnLoad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(277, 82);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "Wczytaj";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnEnd
            // 
            this.btnEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold);
            this.btnEnd.Location = new System.Drawing.Point(12, 467);
            this.btnEnd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(277, 82);
            this.btnEnd.TabIndex = 5;
            this.btnEnd.Text = "Koniec";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // gbGraph
            // 
            this.gbGraph.Controls.Add(this.treeResult);
            this.gbGraph.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold);
            this.gbGraph.Location = new System.Drawing.Point(300, 48);
            this.gbGraph.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbGraph.Name = "gbGraph";
            this.gbGraph.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbGraph.Size = new System.Drawing.Size(832, 615);
            this.gbGraph.TabIndex = 1;
            this.gbGraph.TabStop = false;
            this.gbGraph.Text = "Wynik:";
            // 
            // treeResult
            // 
            this.treeResult.Location = new System.Drawing.Point(7, 33);
            this.treeResult.Margin = new System.Windows.Forms.Padding(4);
            this.treeResult.Name = "treeResult";
            this.treeResult.Size = new System.Drawing.Size(820, 576);
            this.treeResult.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Italic);
            this.label1.Location = new System.Drawing.Point(6, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "v. 1.0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Italic);
            this.label2.Location = new System.Drawing.Point(136, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Damian Likszo";
            // 
            // gbAbout
            // 
            this.gbAbout.Controls.Add(this.label2);
            this.gbAbout.Controls.Add(this.label1);
            this.gbAbout.Controls.Add(this.lNameProgram);
            this.gbAbout.Location = new System.Drawing.Point(12, 557);
            this.gbAbout.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbAbout.Name = "gbAbout";
            this.gbAbout.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbAbout.Size = new System.Drawing.Size(277, 106);
            this.gbAbout.TabIndex = 3;
            this.gbAbout.TabStop = false;
            // 
            // lNameProgram
            // 
            this.lNameProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Italic);
            this.lNameProgram.Location = new System.Drawing.Point(5, 18);
            this.lNameProgram.Name = "lNameProgram";
            this.lNameProgram.Size = new System.Drawing.Size(266, 34);
            this.lNameProgram.TabIndex = 2;
            this.lNameProgram.Text = "Praca magisterska";
            this.lNameProgram.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPath
            // 
            this.txtPath.Enabled = false;
            this.txtPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtPath.Location = new System.Drawing.Point(475, 12);
            this.txtPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(657, 30);
            this.txtPath.TabIndex = 6;
            // 
            // lPath
            // 
            this.lPath.AutoSize = true;
            this.lPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lPath.Location = new System.Drawing.Point(295, 14);
            this.lPath.Name = "lPath";
            this.lPath.Size = new System.Drawing.Size(160, 25);
            this.lPath.TabIndex = 0;
            this.lPath.Text = "Ścieżka do pliku:";
            // 
            // btnSaveGran
            // 
            this.btnSaveGran.Enabled = false;
            this.btnSaveGran.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold);
            this.btnSaveGran.Location = new System.Drawing.Point(12, 107);
            this.btnSaveGran.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaveGran.Name = "btnSaveGran";
            this.btnSaveGran.Size = new System.Drawing.Size(277, 82);
            this.btnSaveGran.TabIndex = 1;
            this.btnSaveGran.Text = "Zapisz Tabele Granul";
            this.btnSaveGran.UseVisualStyleBackColor = true;
            this.btnSaveGran.Click += new System.EventHandler(this.btnSaveGran_Click);
            // 
            // btnSaveChains
            // 
            this.btnSaveChains.Enabled = false;
            this.btnSaveChains.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold);
            this.btnSaveChains.Location = new System.Drawing.Point(12, 203);
            this.btnSaveChains.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaveChains.Name = "btnSaveChains";
            this.btnSaveChains.Size = new System.Drawing.Size(277, 82);
            this.btnSaveChains.TabIndex = 1;
            this.btnSaveChains.Text = "Zapisz Wynik";
            this.btnSaveChains.UseVisualStyleBackColor = true;
            this.btnSaveChains.Click += new System.EventHandler(this.btnSaveGranSet_Click);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 668);
            this.Controls.Add(this.lPath);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.gbAbout);
            this.Controls.Add(this.gbGraph);
            this.Controls.Add(this.btnEnd);
            this.Controls.Add(this.btnSaveChains);
            this.Controls.Add(this.btnSaveGran);
            this.Controls.Add(this.btnLoad);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "Form";
            this.Text = "Praca magisterska - Damian Likszo v. 1.0";
            this.gbGraph.ResumeLayout(false);
            this.gbAbout.ResumeLayout(false);
            this.gbAbout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.GroupBox gbGraph;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbAbout;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label lPath;
        private System.Windows.Forms.Button btnSaveGran;
        private System.Windows.Forms.TreeView treeResult;
        private System.Windows.Forms.Label lNameProgram;
        private System.Windows.Forms.Button btnSaveChains;
    }
}

