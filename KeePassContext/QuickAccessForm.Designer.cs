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
            this.buttonCopyPw = new System.Windows.Forms.Button();
            this.buttonCopyUser = new System.Windows.Forms.Button();
            this.buttonUrl = new System.Windows.Forms.Button();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel.Controls.Add(this.buttonCopyFields);
            this.panel.Controls.Add(this.buttonCopyNotes);
            this.panel.Controls.Add(this.buttonMainWindow);
            this.panel.Controls.Add(this.buttonAutoType);
            this.panel.Controls.Add(this.buttonCopyPw);
            this.panel.Controls.Add(this.buttonCopyUser);
            this.panel.Controls.Add(this.buttonUrl);
            this.panel.Location = new System.Drawing.Point(6, 6);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(194, 106);
            this.panel.TabIndex = 0;
            // 
            // buttonCopyFields
            // 
            this.buttonCopyFields.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCopyFields.Location = new System.Drawing.Point(100, 70);
            this.buttonCopyFields.Name = "buttonCopyFields";
            this.buttonCopyFields.Size = new System.Drawing.Size(82, 24);
            this.buttonCopyFields.TabIndex = 4;
            this.buttonCopyFields.Text = "Fields";
            this.buttonCopyFields.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonCopyFields.UseVisualStyleBackColor = true;
            this.buttonCopyFields.Click += new System.EventHandler(this.buttonCopyFields_Click);
            // 
            // buttonCopyNotes
            // 
            this.buttonCopyNotes.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCopyNotes.Location = new System.Drawing.Point(12, 70);
            this.buttonCopyNotes.Name = "buttonCopyNotes";
            this.buttonCopyNotes.Size = new System.Drawing.Size(82, 24);
            this.buttonCopyNotes.TabIndex = 3;
            this.buttonCopyNotes.Text = "Notes";
            this.buttonCopyNotes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonCopyNotes.UseVisualStyleBackColor = true;
            this.buttonCopyNotes.Click += new System.EventHandler(this.buttonCopyNotes_Click);
            // 
            // buttonMainWindow
            // 
            this.buttonMainWindow.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonMainWindow.Location = new System.Drawing.Point(159, 12);
            this.buttonMainWindow.Name = "buttonMainWindow";
            this.buttonMainWindow.Padding = new System.Windows.Forms.Padding(0, 0, 2, 2);
            this.buttonMainWindow.Size = new System.Drawing.Size(24, 24);
            this.buttonMainWindow.TabIndex = 6;
            this.buttonMainWindow.UseVisualStyleBackColor = true;
            this.buttonMainWindow.Click += new System.EventHandler(this.buttonMainWindow_Click);
            // 
            // buttonAutoType
            // 
            this.buttonAutoType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonAutoType.Location = new System.Drawing.Point(130, 12);
            this.buttonAutoType.Name = "buttonAutoType";
            this.buttonAutoType.Padding = new System.Windows.Forms.Padding(0, 0, 2, 2);
            this.buttonAutoType.Size = new System.Drawing.Size(24, 24);
            this.buttonAutoType.TabIndex = 5;
            this.buttonAutoType.UseVisualStyleBackColor = true;
            this.buttonAutoType.Click += new System.EventHandler(this.buttonAutoType_Click);
            // 
            // buttonCopyPw
            // 
            this.buttonCopyPw.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCopyPw.Location = new System.Drawing.Point(100, 41);
            this.buttonCopyPw.Name = "buttonCopyPw";
            this.buttonCopyPw.Size = new System.Drawing.Size(82, 24);
            this.buttonCopyPw.TabIndex = 2;
            this.buttonCopyPw.Text = "Password";
            this.buttonCopyPw.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonCopyPw.UseVisualStyleBackColor = true;
            this.buttonCopyPw.Click += new System.EventHandler(this.buttonCopyPw_Click);
            this.buttonCopyPw.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonCopyPw_MouseDown);
            this.buttonCopyPw.MouseMove += new System.Windows.Forms.MouseEventHandler(this.buttonCopyPw_MouseMove);
            // 
            // buttonCopyUser
            // 
            this.buttonCopyUser.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCopyUser.Location = new System.Drawing.Point(12, 41);
            this.buttonCopyUser.Name = "buttonCopyUser";
            this.buttonCopyUser.Size = new System.Drawing.Size(82, 24);
            this.buttonCopyUser.TabIndex = 1;
            this.buttonCopyUser.Text = "Username";
            this.buttonCopyUser.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonCopyUser.UseVisualStyleBackColor = true;
            this.buttonCopyUser.Click += new System.EventHandler(this.buttonCopyUser_Click);
            this.buttonCopyUser.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonCopyUser_MouseDown);
            this.buttonCopyUser.MouseMove += new System.Windows.Forms.MouseEventHandler(this.buttonCopyUser_MouseMove);
            // 
            // buttonUrl
            // 
            this.buttonUrl.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonUrl.Location = new System.Drawing.Point(12, 12);
            this.buttonUrl.Name = "buttonUrl";
            this.buttonUrl.Size = new System.Drawing.Size(55, 24);
            this.buttonUrl.TabIndex = 0;
            this.buttonUrl.Text = "URL";
            this.buttonUrl.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonUrl.UseVisualStyleBackColor = true;
            this.buttonUrl.Click += new System.EventHandler(this.buttonOpenUrl_Click);
            this.buttonUrl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.buttonUrl_MouseDown);
            this.buttonUrl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.buttonUrl_MouseMove);
            // 
            // QuickAccessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(206, 118);
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuickAccessForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QuickAccess";
            this.TopMost = true;
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonUrl;
        private System.Windows.Forms.Button buttonCopyUser;
        private System.Windows.Forms.Button buttonCopyPw;
        private System.Windows.Forms.Button buttonCopyFields;
        private System.Windows.Forms.Button buttonCopyNotes;


        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button buttonMainWindow;
        private System.Windows.Forms.Button buttonAutoType;
    }
}