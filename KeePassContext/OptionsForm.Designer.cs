namespace KeePassContext
{
    partial class OptionsForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
            this.checkBox_showEmpty = new System.Windows.Forms.CheckBox();
            this.checkBox_showExpired = new System.Windows.Forms.CheckBox();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.checkBox_showRecycleBin = new System.Windows.Forms.CheckBox();
            this.checkBox_clearClipboard = new System.Windows.Forms.CheckBox();
            this.numericUpDown_clearClipboard = new System.Windows.Forms.NumericUpDown();
            this.comboBox_location = new System.Windows.Forms.ComboBox();
            this.label_location = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage_general = new System.Windows.Forms.TabPage();
            this.groupBox_QAWindow = new System.Windows.Forms.GroupBox();
            this.groupBox_QAWindowClose = new System.Windows.Forms.GroupBox();
            this.numericUpDown_closeQATime = new System.Windows.Forms.NumericUpDown();
            this.checkBox_closeQAAfterTime = new System.Windows.Forms.CheckBox();
            this.checkBox_closeQAAutoType = new System.Windows.Forms.CheckBox();
            this.checkBox_closeQAUserPw = new System.Windows.Forms.CheckBox();
            this.groupBox_contextMenu = new System.Windows.Forms.GroupBox();
            this.tabPage_filter = new System.Windows.Forms.TabPage();
            this.label_filter = new System.Windows.Forms.Label();
            this.checkBoxTreeView_groupFilter = new KeePassContext.CheckBoxTreeView();
            this.tabPage_advanced = new System.Windows.Forms.TabPage();
            this.groupBox_clear = new System.Windows.Forms.GroupBox();
            this.buttonclear = new System.Windows.Forms.Button();
            this.label_clear = new System.Windows.Forms.Label();
            this.panel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_clearClipboard)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPage_general.SuspendLayout();
            this.groupBox_QAWindow.SuspendLayout();
            this.groupBox_QAWindowClose.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_closeQATime)).BeginInit();
            this.groupBox_contextMenu.SuspendLayout();
            this.tabPage_filter.SuspendLayout();
            this.tabPage_advanced.SuspendLayout();
            this.groupBox_clear.SuspendLayout();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBox_showEmpty
            // 
            this.checkBox_showEmpty.AutoSize = true;
            this.checkBox_showEmpty.Location = new System.Drawing.Point(6, 19);
            this.checkBox_showEmpty.Name = "checkBox_showEmpty";
            this.checkBox_showEmpty.Size = new System.Drawing.Size(119, 17);
            this.checkBox_showEmpty.TabIndex = 0;
            this.checkBox_showEmpty.Text = "Show empty groups";
            this.checkBox_showEmpty.UseVisualStyleBackColor = true;
            // 
            // checkBox_showExpired
            // 
            this.checkBox_showExpired.AutoSize = true;
            this.checkBox_showExpired.Location = new System.Drawing.Point(6, 42);
            this.checkBox_showExpired.Name = "checkBox_showExpired";
            this.checkBox_showExpired.Size = new System.Drawing.Size(124, 17);
            this.checkBox_showExpired.TabIndex = 1;
            this.checkBox_showExpired.Text = "Show expired entries";
            this.checkBox_showExpired.UseVisualStyleBackColor = true;
            // 
            // button_OK
            // 
            this.button_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button_OK.Location = new System.Drawing.Point(108, 309);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 7;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cancel.Location = new System.Drawing.Point(189, 309);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 8;
            this.button_Cancel.Text = "Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            // 
            // checkBox_showRecycleBin
            // 
            this.checkBox_showRecycleBin.AutoSize = true;
            this.checkBox_showRecycleBin.Location = new System.Drawing.Point(6, 65);
            this.checkBox_showRecycleBin.Name = "checkBox_showRecycleBin";
            this.checkBox_showRecycleBin.Size = new System.Drawing.Size(113, 17);
            this.checkBox_showRecycleBin.TabIndex = 2;
            this.checkBox_showRecycleBin.Text = "Show Recycle Bin";
            this.checkBox_showRecycleBin.UseVisualStyleBackColor = true;
            // 
            // checkBox_clearClipboard
            // 
            this.checkBox_clearClipboard.AutoSize = true;
            this.checkBox_clearClipboard.Location = new System.Drawing.Point(6, 19);
            this.checkBox_clearClipboard.Name = "checkBox_clearClipboard";
            this.checkBox_clearClipboard.Size = new System.Drawing.Size(172, 17);
            this.checkBox_clearClipboard.TabIndex = 3;
            this.checkBox_clearClipboard.Text = "Clipboard auto-clear (seconds):";
            this.checkBox_clearClipboard.UseVisualStyleBackColor = true;
            // 
            // numericUpDown_clearClipboard
            // 
            this.numericUpDown_clearClipboard.Location = new System.Drawing.Point(190, 16);
            this.numericUpDown_clearClipboard.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericUpDown_clearClipboard.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_clearClipboard.Name = "numericUpDown_clearClipboard";
            this.numericUpDown_clearClipboard.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_clearClipboard.TabIndex = 4;
            this.numericUpDown_clearClipboard.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown_clearClipboard.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // comboBox_location
            // 
            this.comboBox_location.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_location.FormattingEnabled = true;
            this.comboBox_location.Items.AddRange(new object[] {
            "Center",
            "Lower Right Corner",
            "Upper Right Corner",
            "Lower Left Corner",
            "Upper Left Corner"});
            this.comboBox_location.Location = new System.Drawing.Point(102, 42);
            this.comboBox_location.Name = "comboBox_location";
            this.comboBox_location.Size = new System.Drawing.Size(128, 21);
            this.comboBox_location.TabIndex = 6;
            // 
            // label_location
            // 
            this.label_location.AutoSize = true;
            this.label_location.Location = new System.Drawing.Point(6, 45);
            this.label_location.Name = "label_location";
            this.label_location.Size = new System.Drawing.Size(90, 13);
            this.label_location.TabIndex = 5;
            this.label_location.Text = "Window Location";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage_general);
            this.tabControl.Controls.Add(this.tabPage_filter);
            this.tabControl.Controls.Add(this.tabPage_advanced);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(256, 295);
            this.tabControl.TabIndex = 9;
            // 
            // tabPage_general
            // 
            this.tabPage_general.Controls.Add(this.groupBox_QAWindow);
            this.tabPage_general.Controls.Add(this.groupBox_contextMenu);
            this.tabPage_general.Location = new System.Drawing.Point(4, 22);
            this.tabPage_general.Name = "tabPage_general";
            this.tabPage_general.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_general.Size = new System.Drawing.Size(248, 269);
            this.tabPage_general.TabIndex = 0;
            this.tabPage_general.Text = "General";
            this.tabPage_general.UseVisualStyleBackColor = true;
            // 
            // groupBox_QAWindow
            // 
            this.groupBox_QAWindow.Controls.Add(this.groupBox_QAWindowClose);
            this.groupBox_QAWindow.Controls.Add(this.checkBox_clearClipboard);
            this.groupBox_QAWindow.Controls.Add(this.label_location);
            this.groupBox_QAWindow.Controls.Add(this.numericUpDown_clearClipboard);
            this.groupBox_QAWindow.Controls.Add(this.comboBox_location);
            this.groupBox_QAWindow.Location = new System.Drawing.Point(6, 100);
            this.groupBox_QAWindow.Name = "groupBox_QAWindow";
            this.groupBox_QAWindow.Size = new System.Drawing.Size(236, 163);
            this.groupBox_QAWindow.TabIndex = 8;
            this.groupBox_QAWindow.TabStop = false;
            this.groupBox_QAWindow.Text = "Quick Access Window";
            // 
            // groupBox_QAWindowClose
            // 
            this.groupBox_QAWindowClose.Controls.Add(this.numericUpDown_closeQATime);
            this.groupBox_QAWindowClose.Controls.Add(this.checkBox_closeQAAfterTime);
            this.groupBox_QAWindowClose.Controls.Add(this.checkBox_closeQAAutoType);
            this.groupBox_QAWindowClose.Controls.Add(this.checkBox_closeQAUserPw);
            this.groupBox_QAWindowClose.Location = new System.Drawing.Point(6, 69);
            this.groupBox_QAWindowClose.Name = "groupBox_QAWindowClose";
            this.groupBox_QAWindowClose.Size = new System.Drawing.Size(224, 88);
            this.groupBox_QAWindowClose.TabIndex = 7;
            this.groupBox_QAWindowClose.TabStop = false;
            this.groupBox_QAWindowClose.Text = "Auto-close";
            // 
            // numericUpDown_closeQATime
            // 
            this.numericUpDown_closeQATime.Location = new System.Drawing.Point(178, 62);
            this.numericUpDown_closeQATime.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericUpDown_closeQATime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_closeQATime.Name = "numericUpDown_closeQATime";
            this.numericUpDown_closeQATime.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown_closeQATime.TabIndex = 3;
            this.numericUpDown_closeQATime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown_closeQATime.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // checkBox_closeQAAfterTime
            // 
            this.checkBox_closeQAAfterTime.AutoSize = true;
            this.checkBox_closeQAAfterTime.Location = new System.Drawing.Point(6, 65);
            this.checkBox_closeQAAfterTime.Name = "checkBox_closeQAAfterTime";
            this.checkBox_closeQAAfterTime.Size = new System.Drawing.Size(159, 17);
            this.checkBox_closeQAAfterTime.TabIndex = 2;
            this.checkBox_closeQAAfterTime.Text = "After inactive time (seconds)";
            this.checkBox_closeQAAfterTime.UseVisualStyleBackColor = true;
            // 
            // checkBox_closeQAAutoType
            // 
            this.checkBox_closeQAAutoType.AutoSize = true;
            this.checkBox_closeQAAutoType.Location = new System.Drawing.Point(6, 42);
            this.checkBox_closeQAAutoType.Name = "checkBox_closeQAAutoType";
            this.checkBox_closeQAAutoType.Size = new System.Drawing.Size(100, 17);
            this.checkBox_closeQAAutoType.TabIndex = 1;
            this.checkBox_closeQAAutoType.Text = "After Auto-Type";
            this.checkBox_closeQAAutoType.UseVisualStyleBackColor = true;
            // 
            // checkBox_closeQAUserPw
            // 
            this.checkBox_closeQAUserPw.AutoSize = true;
            this.checkBox_closeQAUserPw.Location = new System.Drawing.Point(6, 19);
            this.checkBox_closeQAUserPw.Name = "checkBox_closeQAUserPw";
            this.checkBox_closeQAUserPw.Size = new System.Drawing.Size(206, 17);
            this.checkBox_closeQAUserPw.TabIndex = 0;
            this.checkBox_closeQAUserPw.Text = "After copying username and password";
            this.checkBox_closeQAUserPw.UseVisualStyleBackColor = true;
            // 
            // groupBox_contextMenu
            // 
            this.groupBox_contextMenu.Controls.Add(this.checkBox_showEmpty);
            this.groupBox_contextMenu.Controls.Add(this.checkBox_showRecycleBin);
            this.groupBox_contextMenu.Controls.Add(this.checkBox_showExpired);
            this.groupBox_contextMenu.Location = new System.Drawing.Point(6, 6);
            this.groupBox_contextMenu.Name = "groupBox_contextMenu";
            this.groupBox_contextMenu.Size = new System.Drawing.Size(236, 88);
            this.groupBox_contextMenu.TabIndex = 7;
            this.groupBox_contextMenu.TabStop = false;
            this.groupBox_contextMenu.Text = "Context Menu";
            // 
            // tabPage_filter
            // 
            this.tabPage_filter.Controls.Add(this.label_filter);
            this.tabPage_filter.Controls.Add(this.checkBoxTreeView_groupFilter);
            this.tabPage_filter.Location = new System.Drawing.Point(4, 22);
            this.tabPage_filter.Name = "tabPage_filter";
            this.tabPage_filter.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_filter.Size = new System.Drawing.Size(248, 269);
            this.tabPage_filter.TabIndex = 1;
            this.tabPage_filter.Text = "Groupfilter";
            this.tabPage_filter.UseVisualStyleBackColor = true;
            // 
            // label_filter
            // 
            this.label_filter.AutoSize = true;
            this.label_filter.Location = new System.Drawing.Point(6, 6);
            this.label_filter.Name = "label_filter";
            this.label_filter.Size = new System.Drawing.Size(212, 39);
            this.label_filter.TabIndex = 1;
            this.label_filter.Text = "This filter provides the possibility to filter the \r\ncontext menu entries of KeeP" +
    "assContext.\r\nOnly checked groups will be displayed.";
            // 
            // checkBoxTreeView_groupFilter
            // 
            this.checkBoxTreeView_groupFilter.Location = new System.Drawing.Point(6, 51);
            this.checkBoxTreeView_groupFilter.Name = "checkBoxTreeView_groupFilter";
            this.checkBoxTreeView_groupFilter.Size = new System.Drawing.Size(236, 212);
            this.checkBoxTreeView_groupFilter.TabIndex = 0;
            // 
            // tabPage_advanced
            // 
            this.tabPage_advanced.Controls.Add(this.groupBox_clear);
            this.tabPage_advanced.Location = new System.Drawing.Point(4, 22);
            this.tabPage_advanced.Name = "tabPage_advanced";
            this.tabPage_advanced.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_advanced.Size = new System.Drawing.Size(248, 269);
            this.tabPage_advanced.TabIndex = 2;
            this.tabPage_advanced.Text = "Advanced";
            this.tabPage_advanced.UseVisualStyleBackColor = true;
            // 
            // groupBox_clear
            // 
            this.groupBox_clear.Controls.Add(this.buttonclear);
            this.groupBox_clear.Controls.Add(this.label_clear);
            this.groupBox_clear.Location = new System.Drawing.Point(6, 6);
            this.groupBox_clear.Name = "groupBox_clear";
            this.groupBox_clear.Size = new System.Drawing.Size(236, 133);
            this.groupBox_clear.TabIndex = 0;
            this.groupBox_clear.TabStop = false;
            this.groupBox_clear.Text = "Clear Plugin Data";
            // 
            // buttonclear
            // 
            this.buttonclear.Location = new System.Drawing.Point(155, 104);
            this.buttonclear.Name = "buttonclear";
            this.buttonclear.Size = new System.Drawing.Size(75, 23);
            this.buttonclear.TabIndex = 1;
            this.buttonclear.Text = "Clear";
            this.buttonclear.UseVisualStyleBackColor = true;
            this.buttonclear.Click += new System.EventHandler(this.buttonclear_Click);
            // 
            // label_clear
            // 
            this.label_clear.AutoSize = true;
            this.label_clear.Location = new System.Drawing.Point(3, 20);
            this.label_clear.Name = "label_clear";
            this.label_clear.Size = new System.Drawing.Size(206, 78);
            this.label_clear.TabIndex = 0;
            this.label_clear.Text = resources.GetString("label_clear.Text");
            // 
            // panel
            // 
            this.panel.Controls.Add(this.tabControl);
            this.panel.Controls.Add(this.button_Cancel);
            this.panel.Controls.Add(this.button_OK);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(276, 344);
            this.panel.TabIndex = 9;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 344);
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "KeePassContext Options";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_clearClipboard)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPage_general.ResumeLayout(false);
            this.groupBox_QAWindow.ResumeLayout(false);
            this.groupBox_QAWindow.PerformLayout();
            this.groupBox_QAWindowClose.ResumeLayout(false);
            this.groupBox_QAWindowClose.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_closeQATime)).EndInit();
            this.groupBox_contextMenu.ResumeLayout(false);
            this.groupBox_contextMenu.PerformLayout();
            this.tabPage_filter.ResumeLayout(false);
            this.tabPage_filter.PerformLayout();
            this.tabPage_advanced.ResumeLayout(false);
            this.groupBox_clear.ResumeLayout(false);
            this.groupBox_clear.PerformLayout();
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_showEmpty;
        private System.Windows.Forms.CheckBox checkBox_showExpired;
        private CheckBoxTreeView checkBoxTreeView_groupFilter;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.CheckBox checkBox_showRecycleBin;
        private System.Windows.Forms.CheckBox checkBox_clearClipboard;
        private System.Windows.Forms.NumericUpDown numericUpDown_clearClipboard;
        private System.Windows.Forms.ComboBox comboBox_location;
        private System.Windows.Forms.Label label_location;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage_general;
        private System.Windows.Forms.TabPage tabPage_filter;
        private System.Windows.Forms.TabPage tabPage_advanced;
        private System.Windows.Forms.Label label_filter;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.GroupBox groupBox_clear;
        private System.Windows.Forms.Button buttonclear;
        private System.Windows.Forms.Label label_clear;
        private System.Windows.Forms.GroupBox groupBox_QAWindow;
        private System.Windows.Forms.GroupBox groupBox_contextMenu;
        private System.Windows.Forms.GroupBox groupBox_QAWindowClose;
        private System.Windows.Forms.NumericUpDown numericUpDown_closeQATime;
        private System.Windows.Forms.CheckBox checkBox_closeQAAfterTime;
        private System.Windows.Forms.CheckBox checkBox_closeQAAutoType;
        private System.Windows.Forms.CheckBox checkBox_closeQAUserPw;
    }
}