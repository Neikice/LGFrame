using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MathTool
{
    public static float piDivide180 = Mathf.PI / 180;

    public static bool IsFacingRight(Transform t)
    {
        if (t.localEulerAngles.y > 0) return false;
        else return true;
    }

    public static void FacingRight(Transform t)
    {
        t.localEulerAngles = new Vector3(0, 0, 0);
    }

    public static void FacingLeft(Transform t)
    {
        t.localEulerAngles = new Vector3(0, 180, 0);
    }

    public static Vector2 GetVector2(Vector3 a)
    {
        Vector2 posA = new Vector2(a.x, a.z);
        return posA;
    }
    /// <summary>
    /// 忽略Y轴
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static float GetDistance(Transform a, Transform b)
    {
        Vector2 posA = GetVector2(a.position);
        Vector2 posB = GetVector2(b.position);
        return Vector2.Distance(posA, posB);
    }
    /// <summary>
    /// 忽略Y轴
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static float GetDistance(Transform a, Vector3 b)
    {
        Vector2 posA = GetVector2(a.position);
        Vector2 posB = GetVector2(b);
        return Vector2.Distance(posA, posB);
    }
    public static Vector2 GetDirection(Transform a, Transform b)
    {
        Vector2 posA = GetVector2(a.position);
        Vector2 posB = GetVector2(b.position);
        return posB - posA;
    }

    public enum Calculate
    {
        Add,
        Multiply,
        Power,
    }
    public static int calculate(Calculate calType, int origin, int value, out float delta) 
    {
        int temp=0;
        switch (calType) {
            case Calculate.Add:
                temp = origin + value;
                break;
            case Calculate.Multiply:
                temp = origin * value;
                break;
            case Calculate.Power:
                temp = (int)Mathf.Pow(origin, value);
                break;
        }

        delta = temp - origin;
        return temp;
    }
    public static int calculate(Calculate calType, int origin, int value, ref Stack<float> deltaList)
    {
        int temp = 0;
        switch (calType)
        {
            case Calculate.Add:
                temp = origin + value;
                break;
            case Calculate.Multiply:
                temp = origin * value;
                break;
            case Calculate.Power:
                temp = (int)Mathf.Pow(origin, value);
                break;
        }

        deltaList.Push( temp - origin);
        return temp;
    }
    public static float calculate(Calculate calType, float origin, float value, ref Stack<float> deltaList)
    {
        float temp = 0;
        switch (calType)
        {
            case Calculate.Add:
                temp = origin + value;
                break;
            case Calculate.Multiply:
                temp = origin * value;
                break;
            case Calculate.Power:
                temp = (int)Mathf.Pow(origin, value);
                break;
        }

        deltaList.Push(temp - origin);
        return temp;
    }
    public static int calculate(int origin, ref Stack<float> deltaList)
    {       
        return (int)(origin - deltaList.Pop());
    }
    public static float calculate(float origin, ref Stack<float> deltaList)
    {
        return origin - deltaList.Pop();
    }
    public static float calculate(Calculate calType, float origin, float value, out float delta)
    {
        float temp = 0;
        switch (calType)
        {
            case Calculate.Add:
                temp = origin + value;
                break;
            case Calculate.Multiply:
                temp = origin * value;
                break;
            case Calculate.Power:
                temp = (float)Mathf.Pow(origin, value);
                break;
        }
        delta = temp - origin;
        return temp;
    }
}