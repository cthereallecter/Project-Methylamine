namespace ProjectMethylamine
{
    partial class ContentGenerator
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnPAKRStart = new Button();
            txtArchiveName = new TextBox();
            txtExtractionSource = new TextBox();
            btnUNPAKRStart = new Button();
            lsLogBox = new ListBox();
            grpPAKR = new GroupBox();
            chkDecrypt = new CheckBox();
            chkEncrypt = new CheckBox();
            gr = new GroupBox();
            btnMAPPRDelete = new Button();
            nmdMapSize = new NumericUpDown();
            txtMapName = new TextBox();
            chkMapSeasonal = new CheckBox();
            btnMAPPRMake = new Button();
            lstDataPacks = new ListBox();
            listBox1 = new ListBox();
            grpPAKR.SuspendLayout();
            gr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nmdMapSize).BeginInit();
            SuspendLayout();
            // 
            // btnPAKRStart
            // 
            btnPAKRStart.Location = new Point(78, 47);
            btnPAKRStart.Name = "btnPAKRStart";
            btnPAKRStart.Size = new Size(144, 23);
            btnPAKRStart.TabIndex = 0;
            btnPAKRStart.Text = "Start P.A.K.R.";
            btnPAKRStart.UseVisualStyleBackColor = true;
            btnPAKRStart.Click += btnPAKRStart_Click;
            // 
            // txtArchiveName
            // 
            txtArchiveName.Location = new Point(6, 22);
            txtArchiveName.Name = "txtArchiveName";
            txtArchiveName.Size = new Size(216, 23);
            txtArchiveName.TabIndex = 3;
            // 
            // txtExtractionSource
            // 
            txtExtractionSource.Location = new Point(6, 80);
            txtExtractionSource.Name = "txtExtractionSource";
            txtExtractionSource.Size = new Size(216, 23);
            txtExtractionSource.TabIndex = 6;
            // 
            // btnUNPAKRStart
            // 
            btnUNPAKRStart.Location = new Point(78, 105);
            btnUNPAKRStart.Name = "btnUNPAKRStart";
            btnUNPAKRStart.Size = new Size(144, 23);
            btnUNPAKRStart.TabIndex = 5;
            btnUNPAKRStart.Text = "Start U.N.P.A.K.R.";
            btnUNPAKRStart.UseVisualStyleBackColor = true;
            btnUNPAKRStart.Click += btnUNPAKRStart_Click;
            // 
            // lsLogBox
            // 
            lsLogBox.FormattingEnabled = true;
            lsLogBox.ItemHeight = 15;
            lsLogBox.Location = new Point(12, 154);
            lsLogBox.Name = "lsLogBox";
            lsLogBox.Size = new Size(501, 229);
            lsLogBox.TabIndex = 9;
            // 
            // grpPAKR
            // 
            grpPAKR.Controls.Add(chkDecrypt);
            grpPAKR.Controls.Add(chkEncrypt);
            grpPAKR.Controls.Add(txtArchiveName);
            grpPAKR.Controls.Add(btnPAKRStart);
            grpPAKR.Controls.Add(txtExtractionSource);
            grpPAKR.Controls.Add(btnUNPAKRStart);
            grpPAKR.Location = new Point(12, 12);
            grpPAKR.Name = "grpPAKR";
            grpPAKR.Size = new Size(228, 139);
            grpPAKR.TabIndex = 10;
            grpPAKR.TabStop = false;
            grpPAKR.Text = "P.A.K.R. ";
            // 
            // chkDecrypt
            // 
            chkDecrypt.AutoSize = true;
            chkDecrypt.Location = new Point(6, 109);
            chkDecrypt.Name = "chkDecrypt";
            chkDecrypt.Size = new Size(67, 19);
            chkDecrypt.TabIndex = 3;
            chkDecrypt.Text = "Decrypt";
            chkDecrypt.UseVisualStyleBackColor = true;
            // 
            // chkEncrypt
            // 
            chkEncrypt.AutoSize = true;
            chkEncrypt.Location = new Point(6, 50);
            chkEncrypt.Name = "chkEncrypt";
            chkEncrypt.Size = new Size(66, 19);
            chkEncrypt.TabIndex = 4;
            chkEncrypt.Text = "Encrypt";
            chkEncrypt.UseVisualStyleBackColor = true;
            // 
            // gr
            // 
            gr.Controls.Add(btnMAPPRDelete);
            gr.Controls.Add(nmdMapSize);
            gr.Controls.Add(txtMapName);
            gr.Controls.Add(chkMapSeasonal);
            gr.Controls.Add(btnMAPPRMake);
            gr.Location = new Point(246, 12);
            gr.Name = "gr";
            gr.Size = new Size(267, 139);
            gr.TabIndex = 11;
            gr.TabStop = false;
            gr.Text = "M.A.P.P.R.";
            // 
            // btnMAPPRDelete
            // 
            btnMAPPRDelete.Location = new Point(63, 80);
            btnMAPPRDelete.Name = "btnMAPPRDelete";
            btnMAPPRDelete.Size = new Size(117, 23);
            btnMAPPRDelete.TabIndex = 5;
            btnMAPPRDelete.Text = "Delete Map";
            btnMAPPRDelete.UseVisualStyleBackColor = true;
            btnMAPPRDelete.Click += btnMAPPRDelete_Click;
            // 
            // nmdMapSize
            // 
            nmdMapSize.Location = new Point(84, 50);
            nmdMapSize.Name = "nmdMapSize";
            nmdMapSize.Size = new Size(77, 23);
            nmdMapSize.TabIndex = 3;
            // 
            // txtMapName
            // 
            txtMapName.Location = new Point(6, 22);
            txtMapName.Name = "txtMapName";
            txtMapName.Size = new Size(255, 23);
            txtMapName.TabIndex = 2;
            // 
            // chkMapSeasonal
            // 
            chkMapSeasonal.AutoSize = true;
            chkMapSeasonal.Location = new Point(6, 52);
            chkMapSeasonal.Name = "chkMapSeasonal";
            chkMapSeasonal.Size = new Size(72, 19);
            chkMapSeasonal.TabIndex = 1;
            chkMapSeasonal.Text = "Seasonal";
            chkMapSeasonal.UseVisualStyleBackColor = true;
            // 
            // btnMAPPRMake
            // 
            btnMAPPRMake.Location = new Point(167, 50);
            btnMAPPRMake.Name = "btnMAPPRMake";
            btnMAPPRMake.Size = new Size(94, 23);
            btnMAPPRMake.TabIndex = 0;
            btnMAPPRMake.Text = "Make Map";
            btnMAPPRMake.UseVisualStyleBackColor = true;
            btnMAPPRMake.Click += btnMAPPRMake_Click;
            // 
            // lstDataPacks
            // 
            lstDataPacks.FormattingEnabled = true;
            lstDataPacks.ItemHeight = 15;
            lstDataPacks.Location = new Point(519, 19);
            lstDataPacks.Name = "lstDataPacks";
            lstDataPacks.Size = new Size(126, 364);
            lstDataPacks.TabIndex = 12;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 15;
            listBox1.Location = new Point(651, 19);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(137, 364);
            listBox1.TabIndex = 13;
            // 
            // ContentGenerator
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 392);
            Controls.Add(listBox1);
            Controls.Add(lstDataPacks);
            Controls.Add(gr);
            Controls.Add(grpPAKR);
            Controls.Add(lsLogBox);
            Name = "ContentGenerator";
            Text = "Project Methylamine Content Generator";
            grpPAKR.ResumeLayout(false);
            grpPAKR.PerformLayout();
            gr.ResumeLayout(false);
            gr.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nmdMapSize).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnPAKRStart;
        private TextBox txtArchiveName;
        private TextBox textBox2;
        private TextBox txtExtractionSource;
        private Button btnUNPAKRStart;
        private ListBox lsLogBox;
        private GroupBox grpPAKR;
        private GroupBox gr;
        private CheckBox chkMapSeasonal;
        private Button btnMAPPRMake;
        private CheckBox chkDecrypt;
        private CheckBox chkEncrypt;
        private TextBox txtMapName;
        private NumericUpDown nmdMapSize;
        private ListBox lstDataPacks;
        private ListBox listBox1;
        private Button btnMAPPRDelete;
    }
}
