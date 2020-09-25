using UnityEngine;

namespace GameJamStarterKit
{
	/// <summary>
	/// Useful Rigidbody and Rigidbody2D Extensions
	/// </summary>
	public static class RigidbodyExtensions
	{
		/// <summary>
		/// Sets a Rigidbody to kinematic and calls <see cref="RigidbodyExtensions.Stop(Rigidbody)"/>
		/// </summary>
		/// <param name="rigidbody">the Rigidbody to freeze</param>
		public static void Freeze(this Rigidbody rigidbody)
		{
			rigidbody.isKinematic = true;
			rigidbody.Stop();
		}

		/// <summary>
		/// Sets a Rigidbody's IsKinematic to false.
		/// </summary>
		/// <param name="rigidbody">the Rigidbody to unfreeze</param>
		public static void Unfreeze(this Rigidbody rigidbody)
		{
			rigidbody.isKinematic = false;
		}

		/// <summary>
		/// Stops all velocity on a Rigidbody
		/// </summary>
		/// <param name="rigidbody">the Rigidbody to stop</param>
		public static void Stop(this Rigidbody rigidbody)
		{
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
		}
		
		/// <summary>
		/// Sets a Rigidbody2D to kinematic and calls <see cref="RigidbodyExtensions.Stop(Rigidbody2D)"/>
		/// </summary>
		/// <param name="rigidbody">the Rigidbody2D to freeze</param>
		public static void Freeze(this Rigidbody2D rigidbody)
		{
			rigidbody.isKinematic = true;
			rigidbody.Stop();
		}

		/// <summary>
		/// Sets a Rigidbody2D's IsKinematic to false.
		/// </summary>
		/// <param name="rigidbody">the Rigidbody2D to unfreeze</param>
		public static void Unfreeze(this Rigidbody2D rigidbody)
		{
			rigidbody.isKinematic = false;
		}

		/// <summary>
		/// Stops all velocity on a Rigidbody2D
		/// </summary>
		/// <param name="rigidbody">the Rigidbody2D to stop</param>
		public static void Stop(this Rigidbody2D rigidbody)
		{
			rigidbody.velocity = Vector2.zero;
			rigidbody.angularVelocity = 0f;
		}
	}
}