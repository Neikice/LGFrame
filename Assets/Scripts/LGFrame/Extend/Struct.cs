using UnityEngine;
using System.Collections;

namespace LGFrame
{
    [System.Serializable]
    public struct Vector2Int
    {
        public int x, y;
        public Vector2Int(int x, int y ) {
            this.x = x;
            this.y = y;
        }

    }
}