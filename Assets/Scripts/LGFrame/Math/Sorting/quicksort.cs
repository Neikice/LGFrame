using UnityEngine;
using System.Collections.Generic;

namespace Sorting
{
    public class quicksort
    {

        List<int> list;

        public void quickSort(int left, int right)
        {
            int i, j, t, temp;

            if (left > right) return;

            temp = list[left];

            i = left;
            j = right;

            while (i != j)
            {
                //顺序很重要，要先从右往左找
                while (list[j] >= temp && i < j)
                    j--;

                //顺序很重要，要先从右往左找
                while (list[i] <= temp && i < j)
                    i++;

                //交换两个数在数组中的位置
                if (i < j) // 当哨兵I和哨兵J没有相遇的时候
                {
                    t = list[i];
                    list[i] = list[j];
                    list[j] = t;
                }
            }

            //最终将基准数归为
            list[left] = list[i];
            list[i] = temp;

            quickSort(left, i-1); // 继续处理左边的，这里是一个递归的过程
            quickSort(i + 1, right);// 继续处理右边的，这里是一个递归的过程
            return;
        }

        public void quickSortList(List<int> list, int left, int right)
        {
            this.list = list;
            quickSort(left, right);
        }
    }
}
