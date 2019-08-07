using KeePass.Forms;
using KeePass.Plugins;
using KeePass.UI;
using KeePassLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace KeePassContext
{
    public class KeePassContextExt : Plugin
    {
        private IPluginHost host;
        private MainForm main;
        private ImageList images;
        private ContextMenuStrip contextMenu;

        private PwDatabase db
        {
            get { return host.MainWindow.ActiveDatabase; }
        }

        private List<ToolStripItem> contextItems;
        private List<ToolStripItem> standardItems;
        private OptionsForm optionsForm;
        private Options options;

        public override bool Initialize(IPluginHost host)
        {
            this.host = host;
            this.main = host.MainWindow;
            this.images = host.MainWindow.ClientIcons;
            this.contextMenu = host.MainWindow.TrayContextMenu;
            this.options = new Options(host);

            host.MainWindow.TrayContextMenu.Opening += ContextMenu_Opening;
            host.MainWindow.TrayContextMenu.Closed += ContextMenu_Closed;
            CreateOptionsMenu();
            return true;
        }

        public override string UpdateUrl
        {
            get { return "https://raw.githubusercontent.com/nein23/KeePassContext/master/version"; }
        }

        private void ContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            contextItems = new List<ToolStripItem>();
            if (db != null && db.IsOpen)
            {
                CreateContextMenu();
            }
        }

        private void ContextMenu_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            ClearContextMenu();
        }

        public void ClearContextMenu()
        {
            if (contextItems != null)
            {
                foreach (var item in contextItems)
                {
                    contextMenu.Items.Remove(item);
                }

                contextItems.Clear();
            }
        }

        private void CreateContextMenu()
        {
            contextItems = new List<ToolStripItem>();
            List<string> filter = options.GetGroupFilter();
            contextItems.AddRange(CreateContextItems(db.RootGroup, filter));
            contextItems.Add(new ToolStripSeparator());

            standardItems = new List<ToolStripItem>();
            foreach (ToolStripItem item in contextMenu.Items)
            {
                standardItems.Add(item);
            }

            contextMenu.Items.Clear();
            contextMenu.Items.AddRange(contextItems.ToArray());
            contextMenu.Items.AddRange(standardItems.ToArray());
        }

        private List<ToolStripItem> CreateContextItems(PwGroup parent, List<string> filter)
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
                        if (db.RecycleBinEnabled && db.RecycleBinUuid.Equals(group.Uuid) && !options.ShowRecycleBin)
                            continue;
                        List<ToolStripItem> subGroupItems = CreateContextItems(group, filter);
                        if (subGroupItems.Count > 0 ||
                            (options.ShowEmptyGroups && !filter.Contains(group.Uuid.ToHexString())))
                        {
                            ToolStripMenuItem groupItem = new ToolStripMenuItem();
                            groupItem.Text = group.Name;
                            groupItem.Image = Util.GetIcon(db, host.MainWindow.ClientIcons, group.CustomIconUuid,
                                group.IconId);
                            groupItem.Tag = group;
                            groupItem.DropDownDirection = ToolStripDropDownDirection.Left;
                            foreach (ToolStripItem subGroupItem in subGroupItems)
                            {
                                if (subGroupItem != null) groupItem.DropDown.Items.Add(subGroupItem);
                            }

                            groupItems.Add(groupItem);
                        }
                    }
                }

                //Create entry items
                if (groupItems.Count > 0 || filter == null || !filter.Contains(parent.Uuid.ToHexString()))
                {
                    foreach (PwEntry entry in parent.Entries)
                    {
                        ToolStripMenuItem entryItem = new ToolStripMenuItem();
                        string title = entry.Strings.ReadSafe(PwDefs.TitleField);
                        entryItem.Text = title;
                        bool expired = false;
                        if (entry.Expires)
                        {
                            expired = entry.ExpiryTime.CompareTo(DateTime.Now) < 0;
                        }

                        if (expired)
                        {
                            if (!options.ShowExpiredEntries) continue;
                            entryItem.Image = Util.GetIcon(db, host.MainWindow.ClientIcons, PwUuid.Zero,
                                PwIcon.Expired);
                            entryItem.Font = new Font(entryItem.Font, FontStyle.Strikeout);
                        }
                        else
                            entryItem.Image = Util.GetIcon(db, host.MainWindow.ClientIcons, entry.CustomIconUuid,
                                entry.IconId);

                        entryItem.Tag = entry;
                        entryItem.Click += EntryItem_Click;
                        entryItems.Add(entryItem);
                    }
                }

                //order entryItems by name
                Func<string, object> convert = str =>
                {
                    try { return int.Parse(str); }
                    catch { return str; }
                };
                entryItems = entryItems.OrderBy(
                    item => Regex.Split(item.Text.Replace(" ", ""), "([0-9]+)").Select(convert),
                    new EnumerableComparer<object>()).ToList();

                //Merge lists and add separator if needed
                items.AddRange(groupItems);
                if (groupItems.Count > 0 && entryItems.Count > 0) items.Add(new ToolStripSeparator());
                items.AddRange(entryItems);
            }

            return items;
        }

        private void EntryItem_Click(object sender, EventArgs e)
        {
            if (!QuickAccessForm.IS_OPEN && sender is ToolStripMenuItem)
            {
                ToolStripMenuItem item = (ToolStripMenuItem) sender;
                if (item.Tag != null && item.Tag is PwEntry)
                {
                    PwEntry entry = (PwEntry) item.Tag;
                    QuickAccessForm.IS_OPEN = true;
                    new QuickAccessForm(host, entry, options, PwDefs.IsTanEntry(entry)).ShowDialog();
                    QuickAccessForm.IS_OPEN = false;
                }
            }
        }

        private void CreateOptionsMenu()
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