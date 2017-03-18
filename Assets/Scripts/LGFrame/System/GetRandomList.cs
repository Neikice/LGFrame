using System;
using System.Collections.Generic;

namespace LGFrame
{
    public class ListEx
    {
        public static List<T> GetRandomList<T>(List<T> inputList)
        {
            //Copy to a array
            T[] copyArray = new T[inputList.Count];
            inputList.CopyTo(copyArray);

            //Add range
            List<T> copyList = new List<T>();
            copyList.AddRange(copyArray);

            //Set outputList and random
            List<T> outputList = new List<T>();
            Random rd = new Random(DateTime.Now.Millisecond);

            while (copyList.Count > 0)
            {
                //Select an index and item 
                int rdIndex = rd.Next(0, copyList.Count - 1);
                T remove = copyList[rdIndex];

                //remove it from copyList and add it to output
                copyList.Remove(remove);
                outputList.Add(remove);
            }
            return outputList;
        }

        public static List<T> ListFilterList<T>(List<T> orgin, List<T> filter)
        {
            return ListFilterList(orgin, filter, null);
        }

        public static List<T> ListFilterList<T>(List<T> orgin, List<T> filter, Action<T> addEvent)
        {
            if (filter == null) filter = new List<T>(orgin.Count);

            if (orgin == null || orgin.Count == 0)
                return filter;

            for (int i = 0; i < orgin.Count; i++)
            {
                if (!filter.Contains(orgin[i]))
                {
                    filter.Add(orgin[i]);
                    if (addEvent != null)
                        addEvent(orgin[i]);
                }
            }

            return filter;
        }
    }
}
