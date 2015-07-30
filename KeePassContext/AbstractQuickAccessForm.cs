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
    public partial class AbstractQuickAccessForm : Form
    {
        protected Panel panel;
        protected Button buttonMainWindow;
        protected Button buttonAutoType;
        protected ImageList imageList;
        protected StatusStrip statusStrip;
        protected ToolStripStatusLabel toolStripStatusLabel;
        protected ToolStripProgressBar toolStripProgressBar;

        protected PwEntry entry = null;
        protected Timer timer = null;
        protected IPluginHost host;

        public AbstractQuickAccessForm() { }

        public AbstractQuickAccessForm(IPluginHost host, PwEntry entry)
        {
            this.host = host;
            this.entry = entry;
            host.MainWindow.FileClosingPre += MainWindow_FileClosingPre;
        }

        protected void MainWindow_FileClosingPre(object sender, KeePass.Forms.FileClosingEventArgs e)
        {
            this.Close();
        }

        protected void handle_buttonAutoType_Click()
        {
            try
            {
                AutoType.PerformIntoPreviousWindow(this, entry, host.MainWindow.ActiveDatabase);
            }
            catch (Exception ex) { MessageService.ShowWarning(ex); }
        }

        protected void handle_buttonMainWindow_Click()
        {
            this.Close();

            host.MainWindow.EnsureVisibleForegroundWindow(true, true);

            Control[] gc = host.MainWindow.Controls.Find("m_tvGroups", true);
            Control[] ec = host.MainWindow.Controls.Find("m_lvEntries", true);
            if (gc != null && gc.Length == 1 && ec != null && ec.Length == 1)
            {
                if (gc[0].GetType() == typeof(CustomTreeViewEx) && ec[0].GetType() == typeof(CustomListViewEx))
                {
                    CustomTreeViewEx groupTree = (CustomTreeViewEx)gc[0];
                    CustomListViewEx entryList = (CustomListViewEx)ec[0];


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
                                    PwListItem pwli = (PwListItem)lvi.Tag;
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
                        PwGroup nodeGroup = (PwGroup)node.Tag;
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
            string str = entry.Strings.ReadSafe(name);
            if (str != null)
            {
                Util.CopyToClipboard(str);
                showClipboardCountdown();
            }
        }



        private void showClipboardCountdown()
        {
            toolStripStatusLabel.Visible = true;
            toolStripProgressBar.Visible = true;
            toolStripProgressBar.Maximum = 12;
            toolStripProgressBar.Minimum = 0;
            toolStripProgressBar.Step = -1;
            toolStripProgressBar.Value = 12;
            if (timer != null)
            {
                timer.Stop();
            }
            else
            {
                timer = new Timer();
                timer.Interval = 1000;
                timer.Tick += timer_Tick;
            }
            timer.Enabled = true;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (this.IsDisposed)
            {
                timer.Stop();
                timer.Enabled = false;
                timer = null;
                return;
            }
            toolStripProgressBar.PerformStep();
            string label = "Clipboard";
            int count = ((12 - toolStripProgressBar.Value) % 4);
            for (int i = 0; i < count; i++)
            {
                label += ".";
            }
            toolStripStatusLabel.Text = label;
            if (toolStripProgressBar.Value <= 0)
            {
                timer.Stop();
                timer.Enabled = false;
                timer = null;
                toolStripStatusLabel.Visible = false;
                toolStripProgressBar.Visible = false;
            }
        }
    }
}
