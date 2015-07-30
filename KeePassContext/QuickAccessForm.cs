using KeePass.Forms;
using KeePass.Plugins;
using KeePass.UI;
using KeePass.Util;
using KeePassLib;
using KeePassLib.Security;
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
    public partial class QuickAccessForm : AbstractQuickAccessForm
    {

        public QuickAccessForm(IPluginHost host, PwEntry entry) : base(host, entry)
        {
            InitializeComponent();
            host.MainWindow.FileClosingPre += MainWindow_FileClosingPre;
            string title = entry.Strings.ReadSafe(PwDefs.TitleField);
            if (title != null) this.Text = title;

            ImageList imgList = new ImageList();
            imgList.Images.Add((System.Drawing.Bitmap)host.Resources.GetObject("B16x16_KTouch"));
            imgList.Images.Add((System.Drawing.Bitmap)host.Resources.GetObject("B16x16_Browser"));
            imgList.Images.Add((System.Drawing.Bitmap)host.Resources.GetObject("B16x16_Personal"));
            imgList.Images.Add((System.Drawing.Bitmap)host.Resources.GetObject("B16x16_KGPG_Info"));
            imgList.Images.Add(host.MainWindow.ClientIcons.Images[(int)PwIcon.Notepad]);
            imgList.Images.Add(host.MainWindow.ClientIcons.Images[(int)PwIcon.List]);
            imgList.Images.Add((System.Drawing.Bitmap)host.Resources.GetObject("B16x16_KeePass"));

            this.Icon = host.MainWindow.Icon;
            buttonAutoType.Image = imgList.Images[0];
            buttonOpenUrl.Image = imgList.Images[1];
            buttonCopyUrl.Image = imgList.Images[1];
            buttonCopyUser.Image = imgList.Images[2];
            buttonCopyPw.Image = imgList.Images[3];
            buttonCopyNotes.Image = imgList.Images[4];
            buttonCopyFields.Image = imgList.Images[5];
            buttonMainWindow.Image = imgList.Images[6];

            buttonAutoType.Enabled = entry.GetAutoTypeEnabled();
            buttonOpenUrl.Enabled = !entry.Strings.GetSafe(PwDefs.UrlField).IsEmpty;
            buttonCopyUrl.Enabled = !entry.Strings.GetSafe(PwDefs.UrlField).IsEmpty;
            buttonCopyUser.Enabled = !entry.Strings.GetSafe(PwDefs.UserNameField).IsEmpty;
            buttonCopyPw.Enabled = !entry.Strings.GetSafe(PwDefs.PasswordField).IsEmpty;
            buttonCopyNotes.Enabled = !entry.Strings.GetSafe(PwDefs.NotesField).IsEmpty;
            buttonCopyFields.Enabled = Util.hasFields(entry);

            toolStripStatusLabel.Visible = false;
            toolStripProgressBar.Visible = false;
        }

        private void buttonAutoType_Click(object sender, EventArgs e)
        {
            handle_buttonAutoType_Click();
        }

        private void buttonOpenUrl_Click(object sender, EventArgs e)
        {
            WinUtil.OpenEntryUrl(entry);
        }

        private void buttonCopyUrl_Click(object sender, EventArgs e)
        {
            readEntryStringAndCopyToClipboard(PwDefs.UrlField);
        }

        private void buttonCopyUser_Click(object sender, EventArgs e)
        {
            readEntryStringAndCopyToClipboard(PwDefs.UserNameField);
        }

        private void buttonCopyPw_Click(object sender, EventArgs e)
        {
            readEntryStringAndCopyToClipboard(PwDefs.PasswordField);
        }

        private void buttonCopyNotes_Click(object sender, EventArgs e)
        {
            readEntryStringAndCopyToClipboard(PwDefs.NotesField);
        }

        private void buttonCopyFields_Click(object sender, EventArgs e)
        {
            Point ptLowerLeft = new Point(0, buttonCopyFields.Height);
            ptLowerLeft = buttonCopyFields.PointToScreen(ptLowerLeft);

            ContextMenuStrip fcon = new ContextMenuStrip();

            foreach (KeyValuePair<string, ProtectedString> kvpStr in entry.Strings)
            {
                if (!PwDefs.IsStandardField(kvpStr.Key))
                {
                    PwIcon pwIcon = (kvpStr.Value.IsProtected ? PwIcon.PaperLocked : PwIcon.PaperNew);

                    ToolStripMenuItem item = new ToolStripMenuItem();
                    item.Image = host.MainWindow.ClientIcons.Images[(int)pwIcon];
                    item.Text = kvpStr.Key;
                    item.Click += fieldsContextMenu_Click;
                    fcon.Items.Add(item);
                }
            }
            fcon.Show(ptLowerLeft);
        }

        private void fieldsContextMenu_Click(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(ToolStripMenuItem))
            {
                ToolStripMenuItem item = (ToolStripMenuItem)sender;
                readEntryStringAndCopyToClipboard(item.Text);
            }
        }

        private void buttonMainWindow_Click(object sender, EventArgs e)
        {
            handle_buttonMainWindow_Click();
        }

    }
}
