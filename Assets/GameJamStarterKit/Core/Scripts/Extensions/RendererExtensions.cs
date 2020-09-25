using UnityEngine;

namespace GameJamStarterKit
{
	/// <summary>
	/// Useful renderer extensions
	/// </summary>
	public static class RendererExtensions
	{
		/// <summary>
		///  Check if a renderer can be seen by the Camera
		/// </summary>
		/// <param name="renderer">the renderer to check</param>
		/// <param name="cam">the camera to test against</param>
		/// <returns>returns true if the renderer's bounds are within the cameras frustum</returns>
		public static bool IsVisibleTo(this Renderer renderer, Camera cam)
		{
			var planes = GeometryUtility.CalculateFrustumPlanes(cam);
			return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
		}
	}
}