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
            txtSource = new TextBox();
            txtArchiveName = new TextBox();
            txtDestination = new TextBox();
            SuspendLayout();
            // 
            // btnPAKRStart
            // 
            btnPAKRStart.Location = new Point(12, 12);
            btnPAKRStart.Name = "btnPAKRStart";
            btnPAKRStart.Size = new Size(108, 23);
            btnPAKRStart.TabIndex = 0;
            btnPAKRStart.Text = "Start P.A.K.R.";
            btnPAKRStart.UseVisualStyleBackColor = true;
            btnPAKRStart.Click += btnPAKRStart_Click;
            // 
            // txtSource
            // 
            txtSource.Location = new Point(126, 12);
            txtSource.Name = "txtSource";
            txtSource.Size = new Size(100, 23);
            txtSource.TabIndex = 1;
            // 
            // txtArchiveName
            // 
            txtArchiveName.Location = new Point(338, 12);
            txtArchiveName.Name = "txtArchiveName";
            txtArchiveName.Size = new Size(100, 23);
            txtArchiveName.TabIndex = 3;
            // 
            // txtDestination
            // 
            txtDestination.Location = new Point(232, 12);
            txtDestination.Name = "txtDestination";
            txtDestination.Size = new Size(100, 23);
            txtDestination.TabIndex = 4;
            // 
            // ContentGenerator
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtDestination);
            Controls.Add(txtArchiveName);
            Controls.Add(txtSource);
            Controls.Add(btnPAKRStart);
            Name = "ContentGenerator";
            Text = "Project Methylamine Content Generator";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnPAKRStart;
        private TextBox txtSource;
        private TextBox txtArchiveName;
        private TextBox txtDestination;
    }
}
