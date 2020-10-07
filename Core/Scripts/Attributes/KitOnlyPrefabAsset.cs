using System;
using UnityEngine;

namespace GameJamStarterKit.Attributes
{
	/// <summary>
	/// Ensures a field can only be a prefab asset
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class KitOnlyPrefabAsset : PropertyAttribute
	{
	}
}
