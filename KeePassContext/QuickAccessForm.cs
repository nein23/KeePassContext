using KeePass.Plugins;
using KeePass.UI;
using KeePass.Util;
using KeePassLib;
using KeePassLib.Security;
using KeePassLib.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace KeePassContext
{
    public partial class QuickAccessForm : Form
    {
        public static bool IS_OPEN = false;
        private PwEntry entry = null;
        private IPluginHost host;
        private bool tan;
        private ImageList imgList;
        private Options options;
        private Timer timer;
        private int ticks;
        private Point mouseDownPos = Point.Empty;

        private bool userCopied = false;
        private bool pwCopied = false;

        public QuickAccessForm(IPluginHost host, PwEntry entry, Options options, bool tan)
        {
            this.host = host;
            this.entry = entry;
            this.tan = tan;
            this.options = options;

            InitializeComponent();

            if (options.Location != (int) Options.Locations.Center)
            {
                this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
                Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;
                if (options.Location == (int) Options.Locations.LowerRight)
                {
                    this.Left = workingArea.Right - this.Width;
                    this.Top = workingArea.Bottom - this.Height;
                }
                else if (options.Location == (int) Options.Locations.UpperRight)
                {
                    this.Left = workingArea.Right - this.Width;
                    this.Top = workingArea.Top;
                }
                else if (options.Location == (int) Options.Locations.LowerLeft)
                {
                    this.Left = workingArea.Left;
                    this.Top = workingArea.Bottom - this.Height;
                }
                else if (options.Location == (int) Options.Locations.UpperLeft)
                {
                    this.Left = workingArea.Left;
                    this.Top = workingArea.Top;
                }
            }


            host.MainWindow.FileClosingPre += MainWindow_FileClosingPre;
            host.MainWindow.VisibleChanged += MainWindow_VisibleChanged;

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
            buttonCopyFields.Enabled = HasFields(entry);

            if (tan) buttonCopyUser.Text = "Copy TAN";
            buttonCopyPw.Visible = !tan;

            this.panel.Select();

            userCopied = !buttonCopyUser.Enabled;
            pwCopied = !buttonCopyPw.Enabled;

            this.Activated += QuickAccessForm_Activated;
            this.Deactivate += QuickAccessForm_Deactivate;
            this.FormClosing += QuickAccessForm_FormClosing;
        }

        internal static bool HasFields(PwEntry entry)
        {
            foreach (KeyValuePair<string, ProtectedString> kvpStr in entry.Strings)
            {
                if (!PwDefs.IsStandardField(kvpStr.Key)) return true;
            }

            return false;
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
            if (options.CloseAfterTime)
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
            if (ticks >= options.CloseTime)
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
                if (options.CloseAfterAutoType) this.Close();
            }
            catch (Exception ex)
            {
                MessageService.ShowWarning(ex);
            }
        }

        private void buttonUrl_Click(object sender, EventArgs e)
        {
            WinUtil.OpenEntryUrl(entry);
        }

        private void buttonUrl_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownPos = e.Location;
        }

        private void buttonUrl_MouseMove(object sender, MouseEventArgs e)
        {
            if (checkDrag(e, buttonUrl))
            {
                string data = entry.Strings.ReadSafe(PwDefs.UrlField);
                buttonUrl.DoDragDrop(data, DragDropEffects.Copy);
            }
        }

        private void buttonCopyUser_Click(object sender, EventArgs e)
        {
            mouseDownPos = Point.Empty;
            if (tan)
            {
                readEntryStringAndCopyToClipboard(PwDefs.PasswordField);
                if (entry.Expires && entry.ExpiryTime <= DateTime.Now) return;
                EntryUtil.ExpireTanEntryIfOption(entry, host.MainWindow.ActiveDatabase);
                host.MainWindow.UpdateUI(false, null, false, null, true, null, true);
            }
            else
            {
                readEntryStringAndCopyToClipboard(PwDefs.UserNameField);
            }

            userCopied = true;
            if (options.CloseAfterCopy && pwCopied) this.Close();
        }

        private void buttonCopyUser_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownPos = e.Location;
        }

        private void buttonCopyUser_MouseMove(object sender, MouseEventArgs e)
        {
            if (checkDrag(e, buttonCopyUser))
            {
                string data = entry.Strings.ReadSafe(tan ? PwDefs.PasswordField : PwDefs.UserNameField);
                buttonCopyUser.DoDragDrop(data, DragDropEffects.Copy);
            }
        }

        private void buttonCopyPw_Click(object sender, EventArgs e)
        {
            mouseDownPos = Point.Empty;
            readEntryStringAndCopyToClipboard(PwDefs.PasswordField);
            pwCopied = true;
            if (options.CloseAfterCopy && userCopied) this.Close();
        }

        private void buttonCopyPw_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownPos = e.Location;
        }

        private void buttonCopyPw_MouseMove(object sender, MouseEventArgs e)
        {
            if (checkDrag(e, buttonCopyPw))
            {
                string data = entry.Strings.ReadSafe(PwDefs.PasswordField);
                buttonCopyPw.DoDragDrop(data, DragDropEffects.Copy);
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
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem item = (ToolStripMenuItem) sender;
                readEntryStringAndCopyToClipboard(item.Text);
            }
        }

        protected void MainWindow_FileClosingPre(object sender, KeePass.Forms.FileClosingEventArgs e)
        {
            this.Close();
        }

        private void MainWindow_VisibleChanged(object sender, EventArgs e)
        {
            if (host.MainWindow.Visible)
            {
                Close();
            }
        }

        private void buttonMainWindow_Click(object sender, EventArgs e)
        {
            this.Close();

            host.MainWindow.EnsureVisibleForegroundWindow(true, true);
            Control[] gc = host.MainWindow.Controls.Find("m_tvGroups", true);
            Control[] ec = host.MainWindow.Controls.Find("m_lvEntries", true);
            if (gc != null && gc.Length == 1 && ec != null && ec.Length == 1)
            {
                if (gc[0] is CustomTreeViewEx && ec[0] is CustomListViewEx)
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
                    if (node.Tag is PwGroup)
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
            ClipboardUtil.CopyAndMinimize(entry.Strings.ReadSafe(name),
                true,
                KeePass.Program.Config.MainWindow.MinimizeAfterClipboardCopy ? host.MainWindow : null,
                entry, host.MainWindow.ActiveDatabase);
            host.MainWindow.StartClipboardCountdown();
        }

        private bool checkDrag(MouseEventArgs current, Control control, int delta = 10)
        {
            if (!mouseDownPos.IsEmpty)
            {
                int dx = Math.Abs(mouseDownPos.X - current.X);
                int dy = Math.Abs(mouseDownPos.Y - current.Y);
                if (current.X < 0 || current.Y < 0 || current.X >= control.Size.Width ||
                    current.Y >= control.Size.Height || dx > 10 || dy > 10)
                {
                    mouseDownPos = Point.Empty;
                    return true;
                }
            }

            return false;
        }
    }
}