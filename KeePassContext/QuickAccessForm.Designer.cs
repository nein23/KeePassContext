using System.Drawing;
namespace KeePassContext
{
    partial class QuickAccessForm
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
            this.buttonCopyFields = new System.Windows.Forms.Button();
            this.buttonCopyNotes = new System.Windows.Forms.Button();
            this.buttonMainWindow = new System.Windows.Forms.Button();
            this.buttonAutoType = new System.Windows.Forms.Button();
            this.buttonCopyUrl = new System.Windows.Forms.Button();
            this.buttonCopyPw = new System.Windows.Forms.Button();
            this.buttonCopyUser = new System.Windows.Forms.Button();
            this.buttonOpenUrl = new System.Windows.Forms.Button();
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
            this.panel.Controls.Add(this.buttonCopyFields);
            this.panel.Controls.Add(this.buttonCopyNotes);
            this.panel.Controls.Add(this.buttonMainWindow);
            this.panel.Controls.Add(this.buttonAutoType);
            this.panel.Controls.Add(this.buttonCopyUrl);
            this.panel.Controls.Add(this.buttonCopyPw);
            this.panel.Controls.Add(this.buttonCopyUser);
            this.panel.Controls.Add(this.buttonOpenUrl);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(212, 156);
            this.panel.TabIndex = 0;
            // 
            // buttonCopyFields
            // 
            this.buttonCopyFields.AutoSize = true;
            this.buttonCopyFields.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonCopyFields.Location = new System.Drawing.Point(102, 99);
            this.buttonCopyFields.Name = "buttonCopyFields";
            this.buttonCopyFields.Size = new System.Drawing.Size(44, 23);
            this.buttonCopyFields.TabIndex = 13;
            this.buttonCopyFields.Text = "Fields";
            this.buttonCopyFields.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonCopyFields.UseVisualStyleBackColor = true;
            this.buttonCopyFields.Click += new System.EventHandler(this.buttonCopyFields_Click);
            // 
            // buttonCopyNotes
            // 
            this.buttonCopyNotes.AutoSize = true;
            this.buttonCopyNotes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonCopyNotes.Location = new System.Drawing.Point(12, 99);
            this.buttonCopyNotes.Name = "buttonCopyNotes";
            this.buttonCopyNotes.Size = new System.Drawing.Size(45, 23);
            this.buttonCopyNotes.TabIndex = 12;
            this.buttonCopyNotes.Text = "Notes";
            this.buttonCopyNotes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonCopyNotes.UseVisualStyleBackColor = true;
            this.buttonCopyNotes.Click += new System.EventHandler(this.buttonCopyNotes_Click);
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
            // buttonCopyUrl
            // 
            this.buttonCopyUrl.AutoSize = true;
            this.buttonCopyUrl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonCopyUrl.Location = new System.Drawing.Point(102, 41);
            this.buttonCopyUrl.Name = "buttonCopyUrl";
            this.buttonCopyUrl.Size = new System.Drawing.Size(66, 23);
            this.buttonCopyUrl.TabIndex = 3;
            this.buttonCopyUrl.Text = "Copy URL";
            this.buttonCopyUrl.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonCopyUrl.UseVisualStyleBackColor = true;
            this.buttonCopyUrl.Click += new System.EventHandler(this.buttonCopyUrl_Click);
            // 
            // buttonCopyPw
            // 
            this.buttonCopyPw.AutoSize = true;
            this.buttonCopyPw.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonCopyPw.Location = new System.Drawing.Point(102, 70);
            this.buttonCopyPw.Name = "buttonCopyPw";
            this.buttonCopyPw.Size = new System.Drawing.Size(63, 23);
            this.buttonCopyPw.TabIndex = 2;
            this.buttonCopyPw.Text = "Password";
            this.buttonCopyPw.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonCopyPw.UseVisualStyleBackColor = true;
            this.buttonCopyPw.Click += new System.EventHandler(this.buttonCopyPw_Click);
            // 
            // buttonCopyUser
            // 
            this.buttonCopyUser.AutoSize = true;
            this.buttonCopyUser.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonCopyUser.Location = new System.Drawing.Point(12, 70);
            this.buttonCopyUser.Name = "buttonCopyUser";
            this.buttonCopyUser.Size = new System.Drawing.Size(65, 23);
            this.buttonCopyUser.TabIndex = 1;
            this.buttonCopyUser.Text = "Username";
            this.buttonCopyUser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonCopyUser.UseVisualStyleBackColor = true;
            this.buttonCopyUser.Click += new System.EventHandler(this.buttonCopyUser_Click);
            // 
            // buttonOpenUrl
            // 
            this.buttonOpenUrl.AutoSize = true;
            this.buttonOpenUrl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonOpenUrl.Location = new System.Drawing.Point(12, 41);
            this.buttonOpenUrl.Name = "buttonOpenUrl";
            this.buttonOpenUrl.Size = new System.Drawing.Size(68, 23);
            this.buttonOpenUrl.TabIndex = 0;
            this.buttonOpenUrl.Text = "Open URL";
            this.buttonOpenUrl.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonOpenUrl.UseVisualStyleBackColor = true;
            this.buttonOpenUrl.Click += new System.EventHandler(this.buttonOpenUrl_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripProgressBar});
            this.statusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip.Location = new System.Drawing.Point(0, 134);
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
            // QuickAccessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(212, 156);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.panel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuickAccessForm";
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

        private System.Windows.Forms.Button buttonOpenUrl;
        private System.Windows.Forms.Button buttonCopyUser;
        private System.Windows.Forms.Button buttonCopyUrl;
        private System.Windows.Forms.Button buttonCopyPw;
        private System.Windows.Forms.Button buttonCopyFields;
        private System.Windows.Forms.Button buttonCopyNotes;
        //private System.Windows.Forms.Panel panel;
        //private System.Windows.Forms.Button buttonMainWindow;
        //private System.Windows.Forms.Button buttonAutoType;
        //private System.Windows.Forms.StatusStrip statusStrip;
        //private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        //private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
    }
}