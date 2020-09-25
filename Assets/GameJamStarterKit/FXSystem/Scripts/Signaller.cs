using System.Collections;
using UnityEngine;

namespace GameJamStarterKit.FXSystem
{
	/// <summary>
	/// Allows for execution of a coroutine to be stopped while waiting for a signal to be raised.
	/// </summary>
	public class Signaller : MonoBehaviour
	{
		public bool ProcessSignalNextFrame;
		public string ActiveSignal;

		private bool _endSignalThisFrame;

		/// <summary>
		/// Raises a signal for a single frame
		/// </summary>
		/// <param name="signal">signal to raise</param>
		public void RaiseSignal(string signal)
		{
			if (string.IsNullOrEmpty(signal))
				return;

			ActiveSignal = signal.ToLowerInvariant();
			ProcessSignalNextFrame = true;
			_endSignalThisFrame = false;
		}

		/// <summary>
		/// Waits for the given signal to be raised. Signals will be raised for only one frame.
		/// </summary>
		/// <param name="signal">Signal to wait for</param>
		public IEnumerator WaitForSignal(string signal)
		{
			if (string.IsNullOrEmpty(signal))
				yield break;

			signal = signal.ToLowerInvariant();

			while (!ProcessSignalNextFrame || ActiveSignal != signal)
			{
				_endSignalThisFrame = true;
				yield return false;
			}

			_endSignalThisFrame = true;
			yield return true;
		}

		private void LateUpdate()
		{
			if (_endSignalThisFrame)
			{
				ProcessSignalNextFrame = false;
			}
		}
	}
}