using KeePass.Plugins;
using KeePass.UI;
using KeePass.Util;
using KeePassLib;
using KeePassLib.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KeePassContext
{
    public partial class TANQuickAccessForm : AbstractQuickAccessForm
    {

        public TANQuickAccessForm(IPluginHost host, PwEntry entry) : base(host, entry)
        {
            InitializeComponent();
            host.MainWindow.FileClosingPre += MainWindow_FileClosingPre;
            string title = entry.Strings.ReadSafe(PwDefs.TitleField);
            if (title != null) this.Text = title;

            ImageList imgList = new ImageList();
            imgList.Images.Add((System.Drawing.Bitmap)host.Resources.GetObject("B16x16_KTouch"));
            imgList.Images.Add((System.Drawing.Bitmap)host.Resources.GetObject("B16x16_KGPG_Info"));
            imgList.Images.Add((System.Drawing.Bitmap)host.Resources.GetObject("B16x16_KeePass"));

            this.Icon = host.MainWindow.Icon;
            buttonAutoType.Image = imgList.Images[0];
            buttonCopyTAN.Image = imgList.Images[1];
            buttonMainWindow.Image = imgList.Images[2];

            buttonAutoType.Enabled = entry.GetAutoTypeEnabled();
            buttonCopyTAN.Enabled = !entry.Strings.GetSafe(PwDefs.PasswordField).IsEmpty;

            toolStripStatusLabel.Visible = false;
            toolStripProgressBar.Visible = false;
        }

        private void buttonAutoType_Click(object sender, EventArgs e)
        {
            handle_buttonAutoType_Click();
        }

        protected void buttonMainWindow_Click(object sender, EventArgs e)
        {
            handle_buttonMainWindow_Click();
        }
        private void buttonCopyTAN_Click(object sender, EventArgs e)
        {
            entry.Touch(false);
            EntryUtil.ExpireTanEntryIfOption(entry, host.MainWindow.ActiveDatabase);
            host.MainWindow.UpdateUI(false, null, false, entry.ParentGroup, true, entry.ParentGroup, false);
            readEntryStringAndCopyToClipboard(PwDefs.PasswordField);
        }
    }
}
