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

        public static bool hasFields(PwEntry entry)
        {
            foreach (KeyValuePair<string, ProtectedString> kvpStr in entry.Strings)
            {
                if (!PwDefs.IsStandardField(kvpStr.Key)) return true;
            }
            return false;
        }

        public static string byteArrToStr(byte[] arr)
        {
            string str = "";
            for (int i = 0; i < arr.Length; i++)
            {
                str += (int)arr[i];
                if (i < arr.Length - 1) str += ".";
            }
            return str;
        }
    }
}
