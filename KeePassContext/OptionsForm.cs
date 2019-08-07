using KeePass.Forms;
using KeePassLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace KeePassContext
{
    public partial class OptionsForm : Form
    {
        private MainForm mainWindow;
        private Options options;
        private List<string> filterSettings;
        private bool edited = false;

        public OptionsForm(Options options, MainForm mainWindow)
        {
            this.mainWindow = mainWindow;
            this.options = options;
            this.filterSettings = options.GetGroupFilter();

            InitializeComponent();

            checkBox_showEmpty.Checked = options.ShowEmptyGroups;
            checkBox_showExpired.Checked = options.ShowExpiredEntries;
            checkBox_showRecycleBin.Checked = options.ShowRecycleBin;
            checkBox_clearClipboard.Checked = options.ClearClipboard;
            numericUpDown_clearClipboard.Value = options.ClearClipboardTime;
            comboBox_location.SelectedIndex = options.Location;
            buttonclear.Enabled = !options.IsOptionsEmpty();
            checkBox_closeQAUserPw.Checked = options.CloseAfterCopy;
            checkBox_closeQAAutoType.Checked = options.CloseAfterAutoType;
            checkBox_closeQAAfterTime.Checked = options.CloseAfterTime;
            numericUpDown_closeQATime.Value = options.CloseTime;

            checkBoxTreeView_groupFilter.ImageList = mainWindow.ClientIcons;
            PwDatabase db = mainWindow.ActiveDatabase;
            if (db != null && db.IsOpen)
            {
                CreateTree(checkBoxTreeView_groupFilter.Nodes, db.RootGroup, checkBoxTreeView_groupFilter.ImageList, new Dictionary<PwUuid, int>());
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

        private void CreateTree(TreeNodeCollection nodes, PwGroup group, ImageList images, Dictionary<PwUuid, int> addedCustomIcons)
        {
            if (group != null && nodes != null)
            {
                TreeNode node = new TreeNode(group.Name);
                int iconId = (int)group.IconId;
                if (addedCustomIcons.ContainsKey(group.CustomIconUuid))
                {
                    iconId = addedCustomIcons[group.CustomIconUuid];
                }
                else if (!PwUuid.Zero.Equals(group.CustomIconUuid))
                {
                    PwDatabase db = mainWindow.ActiveDatabase;
                    if (db != null && db.IsOpen)
                    {
                        Image img = mainWindow.ActiveDatabase.GetCustomIcon(group.CustomIconUuid, 16, 16);
                        if (img != null)
                        {
                            iconId = images.Images.Count;
                            addedCustomIcons.Add(group.CustomIconUuid, iconId);
                            images.Images.Add(img);
                        }
                    }
                }

                node.ImageIndex = node.SelectedImageIndex = iconId;
                string id = group.Uuid.ToHexString();
                node.Tag = id;
                node.StateImageIndex = (int)CheckBoxTreeView.CheckedState.UnChecked;
                nodes.Add(node);
                if (filterSettings == null || !filterSettings.Contains((string)node.Tag))
                {
                    node.Checked = !node.Checked;
                }
                else
                {
                    checkBoxTreeView_groupFilter.UpdateParent(node.Parent);
                }
                foreach (PwGroup child in group.Groups)
                {
                    CreateTree(node.Nodes, child, images, addedCustomIcons);
                }
            }
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            options.ShowEmptyGroups = checkBox_showEmpty.Checked;
            options.ShowExpiredEntries = checkBox_showExpired.Checked;
            options.ShowRecycleBin = checkBox_showRecycleBin.Checked;
            options.ClearClipboard = checkBox_clearClipboard.Checked;
            options.ClearClipboardTime = Convert.ToInt32(numericUpDown_clearClipboard.Value);
            options.Location = comboBox_location.SelectedIndex;
            options.CloseAfterCopy = checkBox_closeQAUserPw.Checked;
            options.CloseAfterAutoType = checkBox_closeQAAutoType.Checked;
            options.CloseAfterTime = checkBox_closeQAAfterTime.Checked;
            options.CloseTime = Convert.ToInt32(numericUpDown_closeQATime.Value);
            string toSave = getFilterSettings(checkBoxTreeView_groupFilter.Nodes);
            options.SetGroupFilter(toSave);
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
                if(node.StateImageIndex == (int)CheckBoxTreeView.CheckedState.UnChecked)
                    str += (string)node.Tag + ";";
            }
            return str;
        }

        private void buttonclear_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure?", "Clear KeePassContext Data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(res == DialogResult.Yes)
            {
                options.Clear();
                this.Close();
            }
        }
    }
}
