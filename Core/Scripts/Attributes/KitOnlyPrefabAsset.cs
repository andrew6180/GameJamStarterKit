using System;
using UnityEngine;

/// <summary>
/// Ensures a field can only be a prefab asset
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class KitOnlyPrefabAsset : PropertyAttribute
{
}
