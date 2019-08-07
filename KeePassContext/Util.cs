using KeePassLib;
using KeePassLib.Security;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace KeePassContext
{
    class Util
    {
        private static Timer clipboardTimer = null;
        private static int ticks = 0;
        private static int duration = 0;

        internal static Image GetIcon(PwDatabase db, ImageList imageList, PwUuid customIconId, PwIcon iconId)
        {
            if (!PwUuid.Zero.Equals(customIconId))
            {
                Image img = db.GetCustomIcon(customIconId, 16, 16);
                if (img != null) return img;
            }
            int id = (int)iconId;
            if(id >= 0 && id < imageList.Images.Count)
                return imageList.Images[(int)iconId];
            return null;
        }
    }
}
