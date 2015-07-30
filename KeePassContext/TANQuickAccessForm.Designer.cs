namespace KeePassContext
{
    partial class TANQuickAccessForm
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
            this.panel = new System.Windows.Forms.Panel();
            this.buttonMainWindow = new System.Windows.Forms.Button();
            this.buttonAutoType = new System.Windows.Forms.Button();
            this.buttonCopyTAN = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.panel.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.AutoSize = true;
            this.panel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel.Controls.Add(this.buttonMainWindow);
            this.panel.Controls.Add(this.buttonAutoType);
            this.panel.Controls.Add(this.buttonCopyTAN);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(212, 98);
            this.panel.TabIndex = 0;
            // 
            // buttonMainWindow
            // 
            this.buttonMainWindow.AutoSize = true;
            this.buttonMainWindow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonMainWindow.Location = new System.Drawing.Point(102, 12);
            this.buttonMainWindow.Name = "buttonMainWindow";
            this.buttonMainWindow.Size = new System.Drawing.Size(82, 23);
            this.buttonMainWindow.TabIndex = 11;
            this.buttonMainWindow.Text = "Main Window";
            this.buttonMainWindow.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonMainWindow.UseVisualStyleBackColor = true;
            this.buttonMainWindow.Click += new System.EventHandler(this.buttonMainWindow_Click);
            // 
            // buttonAutoType
            // 
            this.buttonAutoType.AutoSize = true;
            this.buttonAutoType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonAutoType.Location = new System.Drawing.Point(12, 12);
            this.buttonAutoType.Name = "buttonAutoType";
            this.buttonAutoType.Size = new System.Drawing.Size(66, 23);
            this.buttonAutoType.TabIndex = 10;
            this.buttonAutoType.Text = "Auto-Type";
            this.buttonAutoType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonAutoType.UseVisualStyleBackColor = true;
            this.buttonAutoType.Click += new System.EventHandler(this.buttonAutoType_Click);
            // 
            // buttonCopyPw
            // 
            this.buttonCopyTAN.AutoSize = true;
            this.buttonCopyTAN.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonCopyTAN.Location = new System.Drawing.Point(12, 41);
            this.buttonCopyTAN.Name = "buttonCopyPw";
            this.buttonCopyTAN.Size = new System.Drawing.Size(66, 23);
            this.buttonCopyTAN.TabIndex = 2;
            this.buttonCopyTAN.Text = "Copy TAN";
            this.buttonCopyTAN.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonCopyTAN.UseVisualStyleBackColor = true;
            this.buttonCopyTAN.Click += new System.EventHandler(this.buttonCopyTAN_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripProgressBar});
            this.statusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip.Location = new System.Drawing.Point(0, 76);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(212, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(59, 17);
            this.toolStripStatusLabel.Text = "Clipboard";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // TANQuickAccessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(212, 98);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.panel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TANQuickAccessForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QuickAccess";
            this.TopMost = true;
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCopyTAN;
        //private System.Windows.Forms.Panel panel;
        //private System.Windows.Forms.Button buttonMainWindow;
        //private System.Windows.Forms.Button buttonAutoType;
        //private System.Windows.Forms.StatusStrip statusStrip;
        //private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        //private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
    }
}