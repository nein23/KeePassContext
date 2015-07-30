using KeePassLib;
using KeePassLib.Security;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KeePassContext
{
    class Util
    {
        private static Timer clipboardTimer = null;
        private static int ticks = 0;

        public static Image getIcon(PwDatabase db, ImageList imageList, PwUuid customIconId, PwIcon iconId)
        {
            if (!PwUuid.Zero.Equals(customIconId))
            {
                return db.GetCustomIcon(customIconId, 16, 16);
            }
            else
            {
                return imageList.Images[(int)iconId];
            }
        }

        public static void CopyToClipboard(string str)
        {
            if (str != null)
            {
                if (clipboardTimer != null)
                {
                    clipboardTimer.Stop();
                    clipboardTimer.Enabled = false;
                    Clipboard.Clear();
                }
                Clipboard.SetText(str);
                clipboardTimer = new Timer();
                clipboardTimer.Interval = 1000;
                clipboardTimer.Tick += timer_Tick;
                ticks = 0;
                clipboardTimer.Enabled = true;
                clipboardTimer.Start();
            }
        }

        private static void timer_Tick(object sender, EventArgs e)
        {
            ticks++;
            if (ticks >= 12)
            {
                clipboardTimer.Stop();
                clipboardTimer.Enabled = false;
                clipboardTimer = null;
                Clipboard.Clear();
            }
        }

        public static bool hasFields(PwEntry entry)
        {
            foreach (KeyValuePair<string, ProtectedString> kvpStr in entry.Strings)
            {
                if (!PwDefs.IsStandardField(kvpStr.Key)) return true;
            }
            return false;
        }
    }
}
