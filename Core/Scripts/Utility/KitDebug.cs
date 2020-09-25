using UnityEngine;

namespace GameJamStarterKit
{
    /// <summary>
    /// Provides some useful debugging tools.
    /// </summary>
    public static class KitDebug
    {
        /// <summary>
        /// Draws a point with a color specified 
        /// </summary>
        /// <param name="point">point to draw</param>
        /// <param name="color"></param>
        public static void DrawPoint(Vector3 point, Color color)
        {
            Debug.DrawLine(point + Vector3.forward, point + Vector3.back, color);
            Debug.DrawLine(point + Vector3.left, point + Vector3.right, color);
            Debug.DrawLine(point + Vector3.up, point + Vector3.down, color);
        }

        /// <summary>
        /// Draws a point
        /// </summary>
        /// <param name="point">point to draw</param>
        public static void DrawPoint(Vector3 point)
        {
            Debug.DrawLine(point + Vector3.forward, point + Vector3.back, Color.red);
            Debug.DrawLine(point + Vector3.left, point + Vector3.right, Color.red);
            Debug.DrawLine(point + Vector3.up, point + Vector3.down, Color.red);
        }

        /// <summary>
        /// runs Debug.Log("KitDebug.PrintText"), useful for checking if code is reached or events are fired.
        /// </summary>
        public static void PrintText() => Debug.Log("KitDebug.PrintText");
    }
}