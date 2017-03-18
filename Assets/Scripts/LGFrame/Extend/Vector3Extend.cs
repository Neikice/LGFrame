using UnityEngine;
using System.Collections;

namespace LGFrame
{
    public static class ExtendVector3
    {
        public static Vector3 offesetVector(this Vector3 center, float X = 0, float Y = 0, float Z = 0)
        {
            return new Vector3(center.x + X, center.y + Y, center.z + Z);
        }
    }
}
