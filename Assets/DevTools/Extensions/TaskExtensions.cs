using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Extensions
{
    public static class TaskExtensions
    {
        public static async void Forget(this Task task)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }
        }

        public static Task WaitForSeconds(double seconds) =>
            Task.Delay(TimeSpan.FromSeconds(seconds));
    }
}