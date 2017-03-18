using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LGFrame
{
    public class CloneGeneric
    {
        public static T Clone<T>(T tocopy) where T : ICloneGeneric<T>
        {
            return tocopy.Clone();
        }

        public static List<T> Clone<T>(List<T> list) where T : ICloneGeneric<T>
        {
            if (list == null) return null;

            List<T> templist = new List<T>(list.Count);

            for (int i = 0; i < list.Count; i++)
                templist.Add(list[i].Clone());

            return templist;
        }

    }

}
