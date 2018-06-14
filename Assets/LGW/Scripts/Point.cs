using UnityEngine;
using UnityEngine.Assertions;

namespace LGW
{
    /// <summary>
    /// 
    /// </summary>
    public struct Point
    {
        public int x;

        public int y;

        public override string ToString()
        {
            return $"({x}, {y})";
        }
    }
}
