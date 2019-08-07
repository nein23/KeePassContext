using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace KeePassContext
{

    public class CheckBoxTreeView : TreeView
    {

        public enum CheckedState : int { UnInitialised = -1, UnChecked, Checked, Mixed };
        public enum ChildState : int { None = 0, AllUnChecked = 1, AllChecked = 2, Mixed = 3 };
        
        public CheckBoxTreeView() : base()
        {
            CheckBoxes = false;

            StateImageList = new ImageList();
            
            for (int i = 0; i < 3; i++)
            {
                Bitmap bmp = new Bitmap(16, 16);
                Graphics g = Graphics.FromImage(bmp);
                switch (i)
                {
                    case 0:
                        CheckBoxRenderer.DrawCheckBox(g, new Point(0, 1), CheckBoxState.UncheckedNormal);
                        break;
                    case 1:
                        CheckBoxRenderer.DrawCheckBox(g, new Point(0, 1), CheckBoxState.CheckedNormal);
                        break;
                    case 2:
                        CheckBoxRenderer.DrawCheckBox(g, new Point(0, 1), CheckBoxState.MixedNormal);
                        break;
                }

                StateImageList.Images.Add(bmp);
            }
        }

        protected override void OnAfterCheck(TreeViewEventArgs e)
        {
            TreeNode node = e.Node;
            ChildState childState = getChildState(node);

            if(node.StateImageIndex == (int)CheckedState.Checked)
            {
                if (childState == ChildState.None) node.StateImageIndex = (int)CheckedState.UnChecked;
                else if (childState == ChildState.AllChecked) node.StateImageIndex = (int)CheckedState.UnChecked;
                else if (childState == ChildState.AllUnChecked) node.StateImageIndex = (int)CheckedState.Mixed;//Never
                else if (childState == ChildState.Mixed) node.StateImageIndex = (int)CheckedState.Mixed;//Never
            }
            else if(node.StateImageIndex == (int)CheckedState.UnChecked)
            {
                if (childState == ChildState.None) node.StateImageIndex = (int)CheckedState.Checked;
                else if (childState == ChildState.AllChecked) node.StateImageIndex = (int)CheckedState.Checked;//Never
                else if (childState == ChildState.AllUnChecked) node.StateImageIndex = (int)CheckedState.Mixed;
                else if (childState == ChildState.Mixed) node.StateImageIndex = (int)CheckedState.Mixed;//Never
            }
            else
            {
                if (childState == ChildState.None) node.StateImageIndex = (int)CheckedState.Checked;//Never
                else if (childState == ChildState.AllChecked) node.StateImageIndex = (int)CheckedState.Checked;//Never
                else if (childState == ChildState.AllUnChecked) node.StateImageIndex = (int)CheckedState.Checked;//Never
                else if (childState == ChildState.Mixed) node.StateImageIndex = (int)CheckedState.Checked;//Never
            }
            
            UpdateChildren(node.Nodes, node.StateImageIndex);
            
            UpdateParent(node.Parent);

            base.OnAfterCheck(e);
        }
        
        protected void UpdateChildren(TreeNodeCollection nodes, int stateImageIndex)
        {
            if (nodes != null)
            {
                foreach (TreeNode child in nodes)
                {
                    if (stateImageIndex == (int)CheckedState.Checked || stateImageIndex == (int)CheckedState.UnChecked)
                    {
                        child.StateImageIndex = stateImageIndex;
                        UpdateChildren(child.Nodes, stateImageIndex);
                    }
                }
            }
        }

        public void UpdateParent(TreeNode node)
        {
            if (node == null)
                return;

            int oldState = node.StateImageIndex;
            ChildState childState = getChildState(node);

            if (childState == ChildState.AllChecked)
            {
                node.StateImageIndex = (int)CheckedState.Checked;
            }
            else if (childState == ChildState.AllUnChecked)
            {
                if(oldState != (int)CheckedState.UnChecked)
                    node.StateImageIndex = (int)CheckedState.Mixed;
            }
            else if (childState == ChildState.Mixed)
            {
                node.StateImageIndex = (int)CheckedState.Mixed;
            }
            else if (childState == ChildState.None && node.StateImageIndex != (int)CheckedState.UnChecked)
            {
                node.StateImageIndex = (int)CheckedState.Checked;
            }

            if (oldState != node.StateImageIndex)
            {
                UpdateParent(node.Parent);
            }
        }
        
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e != null)
            {
                if (Keys.Space == e.KeyCode && SelectedNode != null)
                    SelectedNode.Checked = !SelectedNode.Checked;
            }
        }

        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            base.OnNodeMouseClick(e);
            if (e != null) {
                TreeViewHitTestInfo info = HitTest(e.X, e.Y);
                if (e.Node != null && info != null && TreeViewHitTestLocations.StateImage == info.Location)
                    e.Node.Checked = !e.Node.Checked;
            }
        }

        private ChildState getChildState(TreeNode node)
        {
            if (node.Nodes.Count == 0) return ChildState.None;
            int CheckedNodes = 0;
            int MixedNodes = 0;
            int UnCheckedNodes = 0;
            foreach (TreeNode tnChild in node.Nodes)
            {
                if (tnChild.StateImageIndex == (int)CheckedState.Checked)
                    CheckedNodes++;
                else if (tnChild.StateImageIndex == (int)CheckedState.Mixed)
                {
                    MixedNodes++;
                    break;
                }
                else
                    UnCheckedNodes++;
            }
            if (CheckedNodes == node.Nodes.Count) return ChildState.AllChecked;
            if (UnCheckedNodes == node.Nodes.Count) return ChildState.AllUnChecked;
            return ChildState.Mixed;
        }
    }
}
