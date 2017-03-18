using UnityEngine;
using System.Collections;

public class angel
{

    public static float LeftOrRight(Transform enity, Transform target)
    {

        Vector3 toTarget = target.position - enity.position;
        Vector3 forward = enity.forward;
        float leftOrright = AngleDir(forward, toTarget, enity.up);
        float angle = Vector3.Angle(forward, toTarget);
        return leftOrright * angle;

    }

    public static float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0f)
        {
            return 1f;
        }
        else if (dir < 0f)
        {
            return -1f;
        }
        else {
            return 0f;
        }

    }
}