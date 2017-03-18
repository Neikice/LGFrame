using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class UIDepth : MonoBehaviour
{
    public int order;
    public bool isUI = true;
    void Start()
    {
        if (isUI)
        {
            var canvas = GetComponent<Canvas>();
            if (canvas == null)
            {
                canvas = gameObject.AddComponent<Canvas>();
            }
            canvas.overrideSorting = true;
            canvas.sortingOrder = order;
        }
        else
        {
            var renders = GetComponentsInChildren<Renderer>();

            foreach (Renderer render in renders)
            {
                render.sortingOrder = order;
            }
        }
    }
}
