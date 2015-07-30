using KeePass.Forms;
using KeePass.Plugins;
using KeePass.UI;
using KeePassLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KeePassContext
{
    public class KeePassContextExt : Plugin
    {
        private IPluginHost host = null;
        private MainForm main = null;
        private ImageList images = null;
        private ContextMenuStrip contextMenu = null;
        private PwDatabase db = null;
        private List<ToolStripItem> contextItems = new List<ToolStripItem>();
        private AbstractQuickAccessForm quickAccessForm;

        public override bool Initialize(IPluginHost host)
        {
            this.host = host;
            this.main = host.MainWindow;
            this.images = host.MainWindow.ClientIcons;
            this.contextMenu = host.MainWindow.TrayContextMenu;

            host.MainWindow.TrayContextMenu.Opening += contextMenu_Opening;
            host.MainWindow.TrayContextMenu.Closed += contextMenu_Closed;

            return true;
        }

        public override string UpdateUrl { get { return "https://kaesdorf.com/keepasscontext/version.txt"; } }

        private void contextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db = host.MainWindow.ActiveDatabase;
            clearContextMenu();
            if (db != null && db.IsOpen)
            {
                createContextMenu();
            }
        }

        private void contextMenu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            clearContextMenu();
        }

        private void clearContextMenu()
        {
            foreach (ToolStripItem item in contextItems)
            {
                contextMenu.Items.Remove(item);
            }
            contextItems = new List<ToolStripItem>();
        }

        private void createContextMenu()
        {
            contextItems = createContextItems(db.RootGroup);
            if (contextItems.Count > 0) contextItems.Add(new ToolStripSeparator());
            int i = 0;
            foreach (ToolStripItem contextItem in contextItems)
            {
                contextMenu.Items.Insert(i, contextItem);
                i++;
            }
        }

        private List<ToolStripItem> createContextItems(PwGroup parent)
        {
            List<ToolStripItem> items = new List<ToolStripItem>();
            if (parent != null)
            {
                List<ToolStripMenuItem> groupItems = new List<ToolStripMenuItem>();
                List<ToolStripMenuItem> entryItems = new List<ToolStripMenuItem>();

                //Create group items
                foreach (PwGroup group in parent.Groups)
                {
                    if (db.RecycleBinEnabled && db.RecycleBinUuid.Equals(group.Uuid)) continue;
                    List<ToolStripItem> subGroupItems = createContextItems(group);
                    if (subGroupItems.Count > 0)
                    {
                        ToolStripMenuItem groupItem = new ToolStripMenuItem();
                        groupItem.Text = group.Name;
                        groupItem.Image = Util.getIcon(db, host.MainWindow.ClientIcons, group.CustomIconUuid, group.IconId);
                        groupItem.Tag = group;
                        groupItem.DropDownDirection = ToolStripDropDownDirection.Left;
                        foreach (ToolStripItem subGroupItem in subGroupItems)
                        {
                            if (subGroupItem != null) groupItem.DropDown.Items.Add(subGroupItem);
                        }
                        groupItems.Add(groupItem);
                    }
                }

                //Create entry items
                foreach (PwEntry entry in parent.Entries)
                {
                    ToolStripMenuItem entryItem = new ToolStripMenuItem();
                    string title = entry.Strings.ReadSafe(PwDefs.TitleField);
                    if (title == null || "".Equals(title)) title = "Unnamed";
                    entryItem.Text = title;
                    bool expired = false;
                    if (entry.Expires)
                    {
                        expired = entry.ExpiryTime.CompareTo(DateTime.Now) < 0;
                    }
                    if (expired)
                    {
                        entryItem.Image = Util.getIcon(db, host.MainWindow.ClientIcons, PwUuid.Zero, PwIcon.Expired);
                        entryItem.Font = new Font(entryItem.Font, FontStyle.Strikeout);
                    }
                    else entryItem.Image = Util.getIcon(db, host.MainWindow.ClientIcons, entry.CustomIconUuid, entry.IconId);
                    entryItem.Tag = entry;
                    entryItem.Click += entryItem_Click;
                    entryItems.Add(entryItem);
                }

                //Merge lists and add separate if needed
                items.AddRange(groupItems);
                if (groupItems.Count > 0 && entryItems.Count > 0) items.Add(new ToolStripSeparator());
                items.AddRange(entryItems);
            }
            return items;
        }

        private void entryItem_Click(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(ToolStripMenuItem))
            {
                ToolStripMenuItem item = (ToolStripMenuItem)sender;
                if (item.Tag != null && item.Tag.GetType() == typeof(PwEntry))
                {
                    if (quickAccessForm != null && !quickAccessForm.IsDisposed) quickAccessForm.Close();
                    PwEntry entry = (PwEntry)item.Tag;
                    if (PwDefs.IsTanEntry(entry)) quickAccessForm = new TANQuickAccessForm(host, entry);
                    else quickAccessForm = new QuickAccessForm(host, entry);
                    quickAccessForm.Show();
                }
            }
            
        }
    }
}
