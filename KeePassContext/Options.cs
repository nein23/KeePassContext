using KeePass.App.Configuration;
using KeePass.Plugins;
using KeePassLib;
using KeePassLib.Collections;
using System;
using System.Collections.Generic;

namespace KeePassContext
{
    public class Options
    {
        private static readonly string CONFIG_PLUGINNAME = "KeePassContext";
        private static readonly string CONFIG_SHOW_EMPTY_GROUPS = CONFIG_PLUGINNAME + ".show.empty.groups";
        private static readonly string CONFIG_SHOW_EXPIRED = CONFIG_PLUGINNAME + ".show.expired.entries";
        private static readonly string CONFIG_SHOW_RECYCLE_BIN = CONFIG_PLUGINNAME + ".show.recycle.bin";
        private static readonly string CONFIG_CLEAR_CLIPBOARD = CONFIG_PLUGINNAME + ".clear.clipboard";
        private static readonly string CONFIG_CLEAR_CLIPBOARD_TIME = CONFIG_PLUGINNAME + ".clear.clipboard.time";
        private static readonly string CONFIG_LOCATION = CONFIG_PLUGINNAME + ".location";
        private static readonly string CONFIG_CLOSE_AFTER_COPY = CONFIG_PLUGINNAME + ".close.after.copy";
        private static readonly string CONFIG_CLOSE_AFTER_AUTOTYPE = CONFIG_PLUGINNAME + ".close.after.autotype";
        private static readonly string CONFIG_CLOSE_AFTER_TIME = CONFIG_PLUGINNAME + ".close.after.time";
        private static readonly string CONFIG_CLOSE_TIME = CONFIG_PLUGINNAME + ".close.time";
        private static readonly string CONFIG_GROUPFILTER = CONFIG_PLUGINNAME + ".groupfilter";

        public enum Locations : int { Center = 0, LowerRight = 1, UpperRight = 2, LowerLeft = 3, UpperLeft = 4 }

        private IPluginHost host;
        private AceCustomConfig config;

        public bool ShowEmptyGroups
        {
            get { return config.GetBool(CONFIG_SHOW_EMPTY_GROUPS, false); }
            set { config.SetBool(CONFIG_SHOW_EMPTY_GROUPS, value); }
        }

        public bool ShowExpiredEntries
        {
            get { return config.GetBool(CONFIG_SHOW_EXPIRED, false); }
            set { config.SetBool(CONFIG_SHOW_EXPIRED, value); }
        }

        public bool ShowRecycleBin
        {
            get { return config.GetBool(CONFIG_SHOW_RECYCLE_BIN, false); }
            set { config.SetBool(CONFIG_SHOW_RECYCLE_BIN, value); }
        }

        public bool ClearClipboard
        {
            get { return config.GetBool(CONFIG_CLEAR_CLIPBOARD, true); }
            set { config.SetBool(CONFIG_CLEAR_CLIPBOARD, value); }
        }

        public int ClearClipboardTime
        {
            get { return Convert.ToInt32(config.GetString(CONFIG_CLEAR_CLIPBOARD_TIME, "12")); }
            set { config.SetString(CONFIG_CLEAR_CLIPBOARD_TIME, Convert.ToString(value)); }
        }

        public int Location
        {
            get { return Convert.ToInt32(config.GetString(CONFIG_LOCATION, Convert.ToString((int)Locations.Center))); }
            set { config.SetString(CONFIG_LOCATION, Convert.ToString(value)); }
        }

        public bool CloseAfterCopy
        {
            get { return config.GetBool(CONFIG_CLOSE_AFTER_COPY, false); }
            set { config.SetBool(CONFIG_CLOSE_AFTER_COPY, value); }
        }

        public bool CloseAfterAutoType
        {
            get { return config.GetBool(CONFIG_CLOSE_AFTER_AUTOTYPE, false); }
            set { config.SetBool(CONFIG_CLOSE_AFTER_AUTOTYPE, value); }
        }

        public bool CloseAfterTime
        {
            get { return config.GetBool(CONFIG_CLOSE_AFTER_TIME, false); }
            set { config.SetBool(CONFIG_CLOSE_AFTER_TIME, value); }
        }

        public int CloseTime
        {
            get { return Convert.ToInt32(config.GetString(CONFIG_CLOSE_TIME, "30")); }
            set { config.SetString(CONFIG_CLOSE_TIME, Convert.ToString(value)); }
        }

        public Options(IPluginHost host)
        {
            this.host = host;
            this.config = host.CustomConfig;
            
        }

        public void Clear()
        {
            config.SetString(CONFIG_SHOW_EMPTY_GROUPS, null);
            config.SetString(CONFIG_SHOW_EXPIRED, null);
            config.SetString(CONFIG_SHOW_RECYCLE_BIN, null);
            config.SetString(CONFIG_CLEAR_CLIPBOARD, null);
            config.SetString(CONFIG_CLEAR_CLIPBOARD_TIME, null);
            config.SetString(CONFIG_LOCATION, null);
            config.SetString(CONFIG_CLOSE_AFTER_COPY, null);
            config.SetString(CONFIG_CLOSE_AFTER_AUTOTYPE, null);
            config.SetString(CONFIG_CLOSE_AFTER_TIME, null);
            config.SetString(CONFIG_CLOSE_TIME, null);
            PwDatabase db = host.MainWindow.ActiveDatabase;
            if (db != null && db.IsOpen)
            {
                StringDictionaryEx dbConfig = db.CustomData;
                dbConfig.Remove(CONFIG_GROUPFILTER);
            }
        }

        public bool IsOptionsEmpty()
        {
            object o = config.GetString(CONFIG_SHOW_EMPTY_GROUPS, null);
            if (o != null) return false;
            o = config.GetString(CONFIG_SHOW_EXPIRED, null);
            if (o != null) return false;
            o = config.GetString(CONFIG_SHOW_RECYCLE_BIN, null);
            if (o != null) return false;
            o = config.GetString(CONFIG_CLEAR_CLIPBOARD, null);
            if (o != null) return false;
            o = config.GetString(CONFIG_CLEAR_CLIPBOARD_TIME, null);
            if (o != null) return false;
            o = config.GetString(CONFIG_LOCATION, null);
            if (o != null) return false;
            o = config.GetString(CONFIG_CLOSE_AFTER_COPY, null);
            if (o != null) return false;
            o = config.GetString(CONFIG_CLOSE_AFTER_AUTOTYPE, null);
            if (o != null) return false;
            o = config.GetString(CONFIG_CLOSE_AFTER_TIME, null);
            if (o != null) return false;
            o = config.GetString(CONFIG_CLOSE_TIME, null);
            if (o != null) return false;
            PwDatabase db = host.MainWindow.ActiveDatabase;
            if (db != null && db.IsOpen)
                return !db.CustomData.Exists(CONFIG_GROUPFILTER);
            return false;
        }

        internal List<string> GetGroupFilter()
        {
            StringDictionaryEx dbConfig = host.MainWindow.ActiveDatabase.CustomData;
            List<string> ids = new List<string>();
            if (!dbConfig.Exists(CONFIG_GROUPFILTER)) return null;
            string groupFilterStr = dbConfig.Get(CONFIG_GROUPFILTER);
            if (groupFilterStr == "") return ids;
            string[] groupFilterSplit = groupFilterStr.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach(string id in groupFilterSplit)
            {
                ids.Add(id);
            }
            return ids;
        }

        internal void SetGroupFilter(string ids)
        {
            StringDictionaryEx dbConfig = host.MainWindow.ActiveDatabase.CustomData;
            if (ids != null && !ids.Equals(""))
            {
                dbConfig.Set(CONFIG_GROUPFILTER, ids);
            }
            else dbConfig.Remove(CONFIG_GROUPFILTER);
        }
    }
}
