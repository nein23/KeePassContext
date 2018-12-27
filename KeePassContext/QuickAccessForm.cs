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
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Forms;

namespace KeePassContext
{
    public partial class QuickAccessForm : Form
    {
        private PwEntry entry = null;
        private IPluginHost host;
        private PwDatabase db;
        private bool tan;
        private ImageList imgList;
        private Options options;
        private Timer timer;
        private int ticks;

        private bool userCopied = false;
        private bool pwCopied = false;

        public QuickAccessForm(IPluginHost host, PwEntry entry, PwDatabase db, Options options, bool tan)
        {
            this.host = host;
            this.entry = entry;
            this.tan = tan;
            this.options = options;
            this.db = db;

            host.MainWindow.FileClosingPre += MainWindow_FileClosingPre;

            InitializeComponent();

            if (options.location != (int) Options.Locations.Center)
            {
                this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
                Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
                if (options.location == (int) Options.Locations.LowerRight)
                {
                    this.Left = workingArea.Right - this.Width;
                    this.Top = workingArea.Bottom - this.Height;
                }
                else if (options.location == (int) Options.Locations.UpperRight)
                {
                    this.Left = workingArea.Right - this.Width;
                    this.Top = workingArea.Top;
                }
                else if (options.location == (int) Options.Locations.LowerLeft)
                {
                    this.Left = workingArea.Left;
                    this.Top = workingArea.Bottom - this.Height;
                }
                else if (options.location == (int) Options.Locations.UpperLeft)
                {
                    this.Left = workingArea.Left;
                    this.Top = workingArea.Top;
                }
            }


            host.MainWindow.FileClosingPre += MainWindow_FileClosingPre;
            string title = entry.Strings.ReadSafe(PwDefs.TitleField);
            if (title != null) this.Text = title;

            imgList = new ImageList();
            imgList.Images.Add((System.Drawing.Bitmap) host.Resources.GetObject("B16x16_KTouch"));
            imgList.Images.Add((System.Drawing.Bitmap) host.Resources.GetObject("B16x16_Browser"));
            imgList.Images.Add((System.Drawing.Bitmap) host.Resources.GetObject("B16x16_Personal"));
            imgList.Images.Add((System.Drawing.Bitmap) host.Resources.GetObject("B16x16_KGPG_Info"));
            imgList.Images.Add(host.MainWindow.ClientIcons.Images[(int) PwIcon.Notepad]);
            imgList.Images.Add(host.MainWindow.ClientIcons.Images[(int) PwIcon.List]);
            imgList.Images.Add((System.Drawing.Bitmap) host.Resources.GetObject("B16x16_KeePass"));

            this.Icon = host.MainWindow.Icon;
            buttonAutoType.Image = imgList.Images[0];
            buttonUrl.Image = imgList.Images[1];
            buttonCopyUser.Image = tan ? imgList.Images[3] : imgList.Images[2];
            buttonCopyPw.Image = imgList.Images[3];
            buttonCopyNotes.Image = imgList.Images[4];
            buttonCopyFields.Image = imgList.Images[5];
            buttonMainWindow.Image = imgList.Images[6];

            buttonAutoType.Enabled = entry.GetAutoTypeEnabled();
            buttonUrl.Enabled = !entry.Strings.GetSafe(PwDefs.UrlField).IsEmpty;
            buttonCopyUser.Enabled = (!entry.Strings.GetSafe(PwDefs.UserNameField).IsEmpty && !tan) ||
                                     (!entry.Strings.GetSafe(PwDefs.PasswordField).IsEmpty && tan);
            buttonCopyPw.Enabled = !entry.Strings.GetSafe(PwDefs.PasswordField).IsEmpty && !tan;
            buttonCopyNotes.Enabled = !entry.Strings.GetSafe(PwDefs.NotesField).IsEmpty;
            buttonCopyFields.Enabled = Util.hasFields(entry);

            if (tan) buttonCopyUser.Text = "Copy TAN";
            buttonCopyPw.Visible = !tan;

            this.panel.Select();

            userCopied = !buttonCopyUser.Enabled;
            pwCopied = !buttonCopyPw.Enabled;

            this.Activated += QuickAccessForm_Activated;
            this.Deactivate += QuickAccessForm_Deactivate;
            this.FormClosing += QuickAccessForm_FormClosing;
        }

        private void QuickAccessForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (timer != null) timer.Enabled = false;
        }

        private void QuickAccessForm_Activated(object sender, EventArgs e)
        {
            if (timer != null)
            {
                timer.Enabled = false;
                ticks = 0;
            }
        }

        private void QuickAccessForm_Deactivate(object sender, EventArgs e)
        {
            if (options.closeAfterTime)
            {
                if (timer != null) timer.Enabled = false;
                ticks = 0;
                timer = new Timer();
                timer.Interval = 1000;
                timer.Tick += Timer_Tick;
                timer.Enabled = true;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ticks++;
            if (ticks >= options.closeTime)
            {
                timer.Enabled = false;
                this.Close();
            }
        }

        private void buttonAutoType_Click(object sender, EventArgs e)
        {
            try
            {
                AutoType.PerformIntoPreviousWindow(this, entry, host.MainWindow.ActiveDatabase);
                if (options.closeAfterAutoType) this.Close();
            }
            catch (Exception ex)
            {
                MessageService.ShowWarning(ex);
            }
        }

        private bool buttonUrlDown = false;
        private Point buttonUrlDownPos = Point.Empty;

        private void buttonOpenUrl_Click(object sender, EventArgs e)
        {
            buttonUrlDown = false;
            buttonUrlDownPos = Point.Empty;
            WinUtil.OpenEntryUrl(entry);
        }

        private void buttonUrl_MouseDown(object sender, MouseEventArgs e)
        {
            buttonUrlDown = true;
            buttonUrlDownPos = e.Location;
        }

        private void buttonUrl_MouseMove(object sender, MouseEventArgs e)
        {
            if (buttonUrlDown)
            {
                int dx = Math.Abs(buttonUrlDownPos.X - e.X);
                int dy = Math.Abs(buttonUrlDownPos.Y - e.Y);
                if (e.X < 0 || e.Y < 0  || e.X >= buttonUrl.Size.Width || e.Y >= buttonUrl.Size.Height || dx > 10 || dy > 10)
                {

                    ProtectedString pstr = entry.Strings.GetSafe(PwDefs.UrlField);
                    buttonUrl.DoDragDrop(pstr.ReadString(), DragDropEffects.Copy);
                    buttonUrlDown = false;
                    buttonUrlDownPos = Point.Empty;
                }
            }
        }

        private bool buttonCopyUserDown = false;
        private Point buttonCopyUserDownPos = Point.Empty;

        private void buttonCopyUser_Click(object sender, EventArgs e)
        {
            buttonCopyUserDown = false;
            buttonCopyUserDownPos = Point.Empty;
            if (tan)
            {
                readEntryStringAndCopyToClipboard(PwDefs.PasswordField);
                if (entry.Expires && entry.ExpiryTime <= DateTime.Now) return;
                entry.Touch(false);
                EntryUtil.ExpireTanEntryIfOption(entry, host.MainWindow.ActiveDatabase);
                host.MainWindow.UpdateUI(false, null, false, entry.ParentGroup, true, entry.ParentGroup, false);
            }
            else readEntryStringAndCopyToClipboard(PwDefs.UserNameField);

            userCopied = true;
            if (options.closeAfterCopy && userCopied && pwCopied) this.Close();
        }

        private void buttonCopyUser_MouseDown(object sender, MouseEventArgs e)
        {
            if (!tan)
            {
                buttonCopyUserDown = true;
                buttonCopyUserDownPos = e.Location;
            }
        }

        private void buttonCopyUser_MouseMove(object sender, MouseEventArgs e)
        {
            if (!tan && buttonCopyUserDown)
            {
                int dx = Math.Abs(buttonCopyUserDownPos.X - e.X);
                int dy = Math.Abs(buttonCopyUserDownPos.Y - e.Y);
                if (e.X < 0 || e.Y < 0 || e.X >= buttonCopyUser.Size.Width || e.Y >= buttonCopyUser.Size.Height || dx > 10 || dy > 10)
                {

                    ProtectedString pstr = entry.Strings.GetSafe(PwDefs.UserNameField);
                    buttonCopyUser.DoDragDrop(pstr.ReadString(), DragDropEffects.Copy);
                    buttonCopyUserDown = false;
                    buttonCopyUserDownPos = Point.Empty;
                }
            }
        }

        private bool buttonCopyPwDown = false;
        private Point buttonCopyPwDownPos = Point.Empty;

        private void buttonCopyPw_Click(object sender, EventArgs e)
        {
            buttonCopyPwDown = false;
            buttonCopyPwDownPos = Point.Empty;
            readEntryStringAndCopyToClipboard(PwDefs.PasswordField);
            pwCopied = true;
            if (options.closeAfterCopy && userCopied && pwCopied) this.Close();
        }

        private void buttonCopyPw_MouseDown(object sender, MouseEventArgs e)
        {
            buttonCopyPwDown = true;
            buttonCopyPwDownPos = e.Location;
        }

        private void buttonCopyPw_MouseMove(object sender, MouseEventArgs e)
        {
            if (buttonCopyPwDown)
            {
                int dx = Math.Abs(buttonCopyPwDownPos.X - e.X);
                int dy = Math.Abs(buttonCopyPwDownPos.Y - e.Y);
                if (e.X < 0 || e.Y < 0 || e.X >= buttonCopyPw.Size.Width || e.Y >= buttonCopyPw.Size.Height || dx > 10 || dy > 10)
                {

                    ProtectedString pstr = entry.Strings.GetSafe(PwDefs.PasswordField);
                    buttonCopyPw.DoDragDrop(pstr.ReadString(), DragDropEffects.Copy);
                    buttonCopyPwDown = false;
                    buttonCopyPwDownPos = Point.Empty;
                }
            }
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
                    item.Image = host.MainWindow.ClientIcons.Images[(int) pwIcon];
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
                ToolStripMenuItem item = (ToolStripMenuItem) sender;
                readEntryStringAndCopyToClipboard(item.Text);
            }
        }

        protected void MainWindow_FileClosingPre(object sender, KeePass.Forms.FileClosingEventArgs e)
        {
            this.Close();
        }

        private void buttonMainWindow_Click(object sender, EventArgs e)
        {
            this.Close();

            host.MainWindow.EnsureVisibleForegroundWindow(true, true);

            Control[] gc = host.MainWindow.Controls.Find("m_tvGroups", true);
            Control[] ec = host.MainWindow.Controls.Find("m_lvEntries", true);
            if (gc != null && gc.Length == 1 && ec != null && ec.Length == 1)
            {
                if (gc[0].GetType() == typeof(CustomTreeViewEx) && ec[0].GetType() == typeof(CustomListViewEx))
                {
                    CustomTreeViewEx groupTree = (CustomTreeViewEx) gc[0];
                    CustomListViewEx entryList = (CustomListViewEx) ec[0];


                    List<PwGroup> groupPath = getGroupPath(entry);
                    if (groupPath.Count > 0)
                    {
                        groupTree.CollapseAll();
                        TreeNode node = findNode(groupTree.Nodes, groupPath[0]);
                        PwGroup group = groupPath[0];
                        if (node != null)
                        {
                            node.Expand();
                            for (int i = 1; i < groupPath.Count; i++)
                            {
                                group = groupPath[i];
                                node = findNode(node.Nodes, group);
                                if (node == null) return;
                                if (i < groupPath.Count - 1)
                                {
                                    node.Expand();
                                }
                            }

                            groupTree.SelectedNode = node;
                            host.MainWindow.UpdateUI(false, null, false, group, true, group, false);

                            foreach (ListViewItem lvi in entryList.Items)
                            {
                                if (lvi.Tag.GetType() == typeof(PwListItem))
                                {
                                    PwListItem pwli = (PwListItem) lvi.Tag;
                                    PwEntry listEntry = pwli.Entry;
                                    if (listEntry != null && listEntry.Uuid.Equals(entry.Uuid))
                                    {
                                        lvi.Selected = true;
                                        continue;
                                    }
                                }

                                lvi.Selected = false;
                            }

                            entryList.Focus();
                            entryList.Select();

                            host.MainWindow.EnsureVisibleEntry(entry.Uuid);
                        }
                    }
                    else
                    {
                        groupTree.CollapseAll();
                        groupTree.Focus();
                        TreeNode node = groupTree.SelectedNode;
                        node.Expand();
                    }
                }
            }
        }


        protected TreeNode findNode(TreeNodeCollection nodes, PwGroup group)
        {
            if (group != null && nodes != null)
            {
                foreach (TreeNode node in nodes)
                {
                    if (node.Tag.GetType() == typeof(PwGroup))
                    {
                        PwGroup nodeGroup = (PwGroup) node.Tag;
                        if (nodeGroup != null && nodeGroup.Uuid.Equals(group.Uuid))
                        {
                            return node;
                        }
                    }
                }
            }

            return null;
        }

        private List<PwGroup> getGroupPath(PwEntry entry)
        {
            List<PwGroup> groupPath = new List<PwGroup>();
            PwGroup group = entry.ParentGroup;
            while (group != null)
            {
                groupPath.Insert(0, group);
                group = group.ParentGroup;
            }

            return groupPath;
        }

        protected void readEntryStringAndCopyToClipboard(string name)
        {
            ProtectedString pstr = entry.Strings.GetSafe(name);
            if (ClipboardUtil.Copy(pstr.ReadString(), true, true, entry, db, IntPtr.Zero))
            {
                host.MainWindow.StartClipboardCountdown();
            }
        }
    }
}