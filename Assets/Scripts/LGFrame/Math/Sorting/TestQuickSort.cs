using UnityEngine;
using System;
using System.Collections.Generic;

public class TestQuickSort : MonoBehaviour
{
    public List<int> list;
    private int left;
    private int right;
    public Sorting.quicksort quicksort;


    // Use this for initialization
    void Start()
    {
        this.left = 1;
        this.right = list.Count - 1;
        string text = String.Empty;
        foreach (var item in this.list)
            text = String.Format("{0} => {1}", text, item);
        Debug.Log(text);
        quicksort = new Sorting.quicksort();
        quicksort.quickSortList(this.list, this.left, this.right);

        text = String.Empty;
        foreach (var item in this.list)
            text = String.Format("{0} => {1}", text, item);
        Debug.Log(text);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
