﻿namespace magisterka
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
            this.btnInfo = new System.Windows.Forms.Button();
            this.btnEnd = new System.Windows.Forms.Button();
            this.gbGraph = new System.Windows.Forms.GroupBox();
            this.lNameProgram = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gbAbout = new System.Windows.Forms.GroupBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.lPath = new System.Windows.Forms.Label();
            this.gbAbout.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoad
            // 
            this.btnLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold);
            this.btnLoad.Location = new System.Drawing.Point(12, 12);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(277, 82);
            this.btnLoad.TabIndex = 0;
            this.btnLoad.Text = "Wczytaj";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnInfo
            // 
            this.btnInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold);
            this.btnInfo.Location = new System.Drawing.Point(12, 115);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(277, 82);
            this.btnInfo.TabIndex = 1;
            this.btnInfo.Text = "Instrukcja";
            this.btnInfo.UseVisualStyleBackColor = true;
            // 
            // btnEnd
            // 
            this.btnEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold);
            this.btnEnd.Location = new System.Drawing.Point(12, 269);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(277, 82);
            this.btnEnd.TabIndex = 2;
            this.btnEnd.Text = "Koniec";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // gbGraph
            // 
            this.gbGraph.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold);
            this.gbGraph.Location = new System.Drawing.Point(300, 48);
            this.gbGraph.Name = "gbGraph";
            this.gbGraph.Size = new System.Drawing.Size(832, 443);
            this.gbGraph.TabIndex = 1;
            this.gbGraph.TabStop = false;
            this.gbGraph.Text = "Graf:";
            // 
            // lNameProgram
            // 
            this.lNameProgram.AutoSize = true;
            this.lNameProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Italic);
            this.lNameProgram.Location = new System.Drawing.Point(6, 18);
            this.lNameProgram.MaximumSize = new System.Drawing.Size(244, 60);
            this.lNameProgram.MinimumSize = new System.Drawing.Size(244, 60);
            this.lNameProgram.Name = "lNameProgram";
            this.lNameProgram.Size = new System.Drawing.Size(244, 60);
            this.lNameProgram.TabIndex = 2;
            this.lNameProgram.Text = "Wyznaczanie elementów sumowo nieredukowalnych oraz porządku zbiorów otwartych.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Italic);
            this.label1.Location = new System.Drawing.Point(6, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "v. 1.0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Italic);
            this.label2.Location = new System.Drawing.Point(131, 98);
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
            this.gbAbout.Location = new System.Drawing.Point(12, 357);
            this.gbAbout.Name = "gbAbout";
            this.gbAbout.Size = new System.Drawing.Size(277, 134);
            this.gbAbout.TabIndex = 3;
            this.gbAbout.TabStop = false;
            // 
            // txtPath
            // 
            this.txtPath.Enabled = false;
            this.txtPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtPath.Location = new System.Drawing.Point(474, 12);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(658, 30);
            this.txtPath.TabIndex = 4;
            // 
            // lPath
            // 
            this.lPath.AutoSize = true;
            this.lPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lPath.Location = new System.Drawing.Point(295, 13);
            this.lPath.Name = "lPath";
            this.lPath.Size = new System.Drawing.Size(160, 25);
            this.lPath.TabIndex = 0;
            this.lPath.Text = "Ścieżka do pliku:";
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 503);
            this.Controls.Add(this.lPath);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.gbAbout);
            this.Controls.Add(this.gbGraph);
            this.Controls.Add(this.btnEnd);
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.btnLoad);
            this.Name = "Form";
            this.Text = "Wyznaczanie elementów sumowo nieredukowalnych oraz porządku zbiorów otwartych";
            this.gbAbout.ResumeLayout(false);
            this.gbAbout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.GroupBox gbGraph;
        private System.Windows.Forms.Label lNameProgram;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gbAbout;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label lPath;
    }
}
