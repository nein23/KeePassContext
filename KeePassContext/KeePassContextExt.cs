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
        private PwDatabase db { get { return host.MainWindow.ActiveDatabase; } }
        private List<ToolStripItem> contextItems = new List<ToolStripItem>();
        private OptionsForm optionsForm;
        private Options options;

        public override bool Initialize(IPluginHost host)
        {
            this.host = host;
            this.main = host.MainWindow;
            this.images = host.MainWindow.ClientIcons;
            this.contextMenu = host.MainWindow.TrayContextMenu;
            this.options = new Options(host);

            host.MainWindow.TrayContextMenu.Opening += contextMenu_Opening;
            host.MainWindow.TrayContextMenu.Closed += contextMenu_Closed;

            createOptionsMenu();
            return true;
        }

        public override string UpdateUrl { get { return "https://kaesdorf.com/keepasscontext/version.txt"; } }

        private void contextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
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
            Dictionary<string, int> filter = options.getGroupFilter();
            contextItems = createContextItems(db.RootGroup, filter);
            if (contextItems.Count > 0) contextItems.Add(new ToolStripSeparator());
            int i = 0;
            foreach (ToolStripItem contextItem in contextItems)
            {
                contextMenu.Items.Insert(i, contextItem);
                i++;
            }
        }

        private List<ToolStripItem> createContextItems(PwGroup parent, Dictionary<string, int> filter)
        {
            List<ToolStripItem> items = new List<ToolStripItem>();
            if (parent != null)
            {
                List<ToolStripItem> groupItems = new List<ToolStripItem>();
                List<ToolStripItem> entryItems = new List<ToolStripItem>();

                //Create group items
                foreach (PwGroup group in parent.Groups)
                {
                    if (group != null)
                    {
                        if (db.RecycleBinEnabled && db.RecycleBinUuid.Equals(group.Uuid) && !options.showRecycleBin) continue;
                        List<ToolStripItem> subGroupItems = createContextItems(group, filter);
                        if (subGroupItems.Count > 0 || options.showEmptyGroups)
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
                            //add if this group or a subgroup is in filter
                            if(filter == null || isCheckedInFilter(Util.byteArrToStr(group.Uuid.UuidBytes), filter)
                                || itemsContainFilter(ToolStipItemCollectionToList(groupItem.DropDown.Items), filter))
                                groupItems.Add(groupItem);
                        }
                    }
                }

                //Create entry items
                // add if parent or a added group is in filter
                if (filter == null || isCheckedInFilter(Util.byteArrToStr(parent.Uuid.UuidBytes), filter) || itemsContainFilter(groupItems, filter))
                {
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
                            if (!options.showExpiredEntries) continue;
                            entryItem.Image = Util.getIcon(db, host.MainWindow.ClientIcons, PwUuid.Zero, PwIcon.Expired);
                            entryItem.Font = new Font(entryItem.Font, FontStyle.Strikeout);
                        }
                        else entryItem.Image = Util.getIcon(db, host.MainWindow.ClientIcons, entry.CustomIconUuid, entry.IconId);
                        entryItem.Tag = entry;
                        entryItem.Click += entryItem_Click;
                        entryItems.Add(entryItem);
                    }
                }

                //Merge lists and add separate if needed
                items.AddRange(groupItems);
                if (groupItems.Count > 0 && entryItems.Count > 0) items.Add(new ToolStripSeparator());
                items.AddRange(entryItems);
            }
            return items;
        }

        private bool itemsContainFilter(List<ToolStripItem> items, Dictionary<string, int> filter)
        {
            if (filter == null) return true;
            foreach(ToolStripItem item in items)
            {
                if(item is ToolStripMenuItem && item.Tag != null && item.Tag is PwGroup)
                {
                    PwGroup group = (PwGroup)item.Tag;
                    string id = Util.byteArrToStr(group.Uuid.UuidBytes);
                    List<ToolStripItem> subItems = ToolStipItemCollectionToList(((ToolStripMenuItem)item).DropDown.Items);
                    if (isCheckedInFilter(id, filter) || itemsContainFilter(subItems, filter)) return true;
                }
            }
            return false;
        }

        private bool isCheckedInFilter(string id, Dictionary<string, int> filter)
        {
            if (filter.ContainsKey(id) && (filter[id] == (int)CheckBoxTreeView.CheckedState.Checked || filter[id] == (int)CheckBoxTreeView.CheckedState.Mixed))
                return true;
            else return false;
        }

        private List<ToolStripItem> ToolStipItemCollectionToList(ToolStripItemCollection items)
        {
            List<ToolStripItem> list = new List<ToolStripItem>();
            foreach (ToolStripItem item in items)
            {
                list.Add(item);
            }
            return list;
        }

        private void entryItem_Click(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(ToolStripMenuItem))
            {
                ToolStripMenuItem item = (ToolStripMenuItem)sender;
                if (item.Tag != null && item.Tag.GetType() == typeof(PwEntry))
                {
                    PwEntry entry = (PwEntry)item.Tag;
                    new QuickAccessForm(host, entry, options, PwDefs.IsTanEntry(entry)).Show();
                }
            }
            
        }
        
        private void createOptionsMenu()
        {
            ToolStripItemCollection menuItems = host.MainWindow.ToolsMenu.DropDownItems;
            ToolStripMenuItem kpcMenuItem = new ToolStripMenuItem();
            kpcMenuItem.Text = "KeePassContext Options";
            kpcMenuItem.Click += KpcMenuItem_Click;
            menuItems.Add(kpcMenuItem);
        }

        private void KpcMenuItem_Click(object sender, EventArgs e)
        {
            optionsForm = new OptionsForm(options, host.MainWindow);
            optionsForm.ShowDialog();
        }
    }
}
