using System;
using System.Collections.Generic;
using UnityEngine;



public class DisplayScriptableObjectPropertiesAttribute : PropertyAttribute { }

public class intGroupAttribue : PropertyAttribute
{
    public GUIContent[] _GUIContent;
    public int[] intValues;

    public intGroupAttribue(Type type)
    {
        var temp = type.GetFields();
        _GUIContent = new GUIContent[temp.Length];
        intValues = new int[temp.Length];
        int count = 0;
        foreach (var item in temp)
        {
            _GUIContent[count] = new GUIContent(item.Name);
            intValues[count] = count;
            count++;
        }


    }
}
