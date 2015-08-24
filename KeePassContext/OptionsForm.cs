using KeePass.Forms;
using KeePassLib;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KeePassContext
{
    public partial class OptionsForm : Form
    {
        private MainForm mainWindow;
        private Options options;
        private Dictionary<string, int> filterSettings;
        private bool edited = false;

        public OptionsForm(Options options, MainForm mainWindow)
        {
            this.mainWindow = mainWindow;
            this.options = options;
            this.filterSettings = options.getGroupFilter();

            InitializeComponent();

            checkBox_showEmpty.Checked = options.showEmptyGroups;
            checkBox_showExpired.Checked = options.showExpiredEntries;
            checkBox_showRecycleBin.Checked = options.showRecycleBin;
            checkBox_clearClipboard.Checked = options.clearClipboard;
            numericUpDown_clearClipboard.Value = options.clearClipboardTime;
            comboBox_location.SelectedIndex = options.location;
            buttonclear.Enabled = !options.isOptionsEmpty();
            checkBox_closeQAUserPw.Checked = options.closeAfterCopy;
            checkBox_closeQAAutoType.Checked = options.closeAfterAutoType;
            checkBox_closeQAAfterTime.Checked = options.closeAfterTime;
            numericUpDown_closeQATime.Value = options.closeTime;

            checkBoxTreeView_groupFilter.ImageList = mainWindow.ClientIcons;
            PwDatabase db = mainWindow.ActiveDatabase;
            if (db != null && db.IsOpen)
            {
                createTree(checkBoxTreeView_groupFilter.Nodes, db.RootGroup);
            }
            else {
                checkBoxTreeView_groupFilter.Enabled = false;
            }
            checkBoxTreeView_groupFilter.ExpandAll();
            checkBoxTreeView_groupFilter.AfterCheck += TreeView_groupFilter_AfterCheck;
        }

        private void TreeView_groupFilter_AfterCheck(object sender, TreeViewEventArgs e)
        {
            edited = true;
        }

        private void createTree(TreeNodeCollection nodes, PwGroup group)
        {
            if (group != null && nodes != null)
            {
                TreeNode node = new TreeNode(group.Name);
                node.ImageIndex = node.SelectedImageIndex = (int)group.IconId;
                string id = Util.byteArrToStr(group.Uuid.UuidBytes);
                node.Tag = id;
                node.StateImageIndex = (int)CheckBoxTreeView.CheckedState.UnChecked;
                nodes.Add(node);
                bool checkedState = false;
                if (filterSettings != null && filterSettings.ContainsKey((string)node.Tag))
                {
                    if (filterSettings[(string)node.Tag] == (int)CheckBoxTreeView.CheckedState.Checked || filterSettings[(string)node.Tag] == (int)CheckBoxTreeView.CheckedState.Mixed)
                    {
                        node.Checked = !node.Checked;
                        checkedState = true;
                    }

                }
                else
                {
                    node.Checked = !node.Checked;
                    checkedState = true;
                }
                if (!checkedState) checkBoxTreeView_groupFilter.UpdateParent(node.Parent);
                foreach (PwGroup child in group.Groups)
                {
                    createTree(node.Nodes, child);
                }
            }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            options.showEmptyGroups = checkBox_showEmpty.Checked;
            options.showExpiredEntries = checkBox_showExpired.Checked;
            options.showRecycleBin = checkBox_showRecycleBin.Checked;
            options.clearClipboard = checkBox_clearClipboard.Checked;
            options.clearClipboardTime = Convert.ToInt32(numericUpDown_clearClipboard.Value);
            options.location = comboBox_location.SelectedIndex;
            options.closeAfterCopy = checkBox_closeQAUserPw.Checked;
            options.closeAfterAutoType = checkBox_closeQAAutoType.Checked;
            options.closeAfterTime = checkBox_closeQAAfterTime.Checked;
            options.closeTime = Convert.ToInt32(numericUpDown_closeQATime.Value);
            string toSave = getFilterSettings(checkBoxTreeView_groupFilter.Nodes).TrimEnd(';');
            options.setGroupFilter(toSave);
            if (edited)
            {
                mainWindow.UpdateUI(false, null, false, mainWindow.GetSelectedGroup(), false, mainWindow.GetSelectedGroup(), true);
            }
        }

        private string getFilterSettings(TreeNodeCollection nodes)
        {
            string str = "";
            foreach(TreeNode node in nodes)
            {
                str += getFilterSettings(node.Nodes);
                str += (string)node.Tag + ":" + node.StateImageIndex + ";";
            }
            return str;
        }

        private void buttonclear_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure?", "Clear KeePassContext Data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(res == DialogResult.Yes)
            {
                options.clear();
                this.Close();
            }
        }
    }
}
