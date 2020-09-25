using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace GameJamStarterKit
{
    /// <summary>
    /// Utility class for using async/await with unity.
    /// </summary>
    public static class AsyncUnityUtility
    {
        static AsyncUnityUtility()
        {
            _cancellationToken = new CancellationTokenSource();
            Application.quitting += () => { _cancellationToken.Cancel(); };
        }
        
        private static readonly CancellationTokenSource _cancellationToken;
       
        
        /// <summary>
        /// Get a generic cancellation token to cancel tasks when Application.quitting is called.
        /// </summary>
        /// <returns>returns the cancellation token for Application.quitting</returns>
        public static CancellationToken GetQuittingCancelToken()
        {
            return _cancellationToken.Token;
        }
        /// <summary>
        /// Returns <see cref="GetQuittingCancelToken"/>.IsCancellationRequested 
        /// </summary>
        public static bool IsQuitTokenRequested => GetQuittingCancelToken().IsCancellationRequested;
        
        /// <summary>
        /// Waits until Time.timeScale is greater than 0. 
        /// </summary>
        /// <returns>Task completes when TIme.timeScale is > 0</returns>
        public static async Task HoldIfPaused()
        {
            while (!(Time.timeScale > 0) && !IsQuitTokenRequested)
            {
                await Task.Delay(1);
            }
        }

        /// <summary>
        /// A shortcut for Task.Delay but with seconds instead of milliseconds.
        /// </summary>
        /// <param name="seconds">seconds to wait</param>
        /// <returns>task completes after the seconds passed</returns>
        public static async Task WaitForSeconds(float seconds)
        {
            await Task.Delay(Mathf.RoundToInt(seconds) * 1000);
        }
    }
}