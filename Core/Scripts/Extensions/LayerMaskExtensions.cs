using UnityEngine;

namespace GameJamStarterKit
{
	/// <summary>
	/// Useful extensions for LayerMasks
	/// </summary>
	public static class LayerMaskExtensions
	{
		/// <summary>
		/// Determine if a LayerMask has the layer given
		/// </summary>
		/// <param name="mask">the mask to check</param>
		/// <param name="layer">the layer to check for</param>
		/// <returns>returns true if the layer has the mask given</returns>
		public static bool HasLayer(this LayerMask mask, int layer)
		{
			return mask.value == (mask.value | 1 << layer);
		}

		/// <summary>
		/// Determine if a LayerMask has the layer given, by name.
		/// </summary>
		/// <param name="mask">the mask to check</param>
		/// <param name="layer">the layer to check for</param>
		/// <returns>returns true if the layer has the mask given</returns>
		public static bool HasLayer(this LayerMask mask, string layer)
		{
			var layerId = LayerMask.NameToLayer(layer);
			return HasLayer(mask, layerId);
		}
	}
}