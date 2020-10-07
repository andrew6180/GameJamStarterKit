using UnityEngine;

namespace GameJamStarterKit
{
	/// <summary>
	/// Extension methods for Rects
	/// </summary>
	public static class RectExtensions
	{
		/// <summary>
		/// gets a random point inside the given rect
		/// </summary>
		/// <param name="rect">the rect to return a point inside of</param>
		/// <returns>returns a random point inside of the given rect</returns>
		public static Vector2 RandomPoint(this Rect rect)
		{
			return new Vector2(Random.Range(rect.xMin, rect.xMax), Random.Range(rect.xMin, rect.xMax));
		}
	}
}