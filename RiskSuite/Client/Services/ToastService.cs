using LogSuite.Client.Helpers;
using System;
using System.Timers;

namespace LogSuite.Client.Services
{
    public class ToastService : IDisposable
    {
        public event Action<string, ToastLevel> OnShow;
        public event Action OnHide;
        private System.Timers.Timer Countdown;

        public void ShowToast(string message, ToastLevel level)
        {
            OnShow?.Invoke(message, level);
            StartCountdown();
        }

        public void ToastSuccess(string message)
        {
            OnShow?.Invoke(message, ToastLevel.Success);
            StartCountdown();
        }

        public void ToastError(string message)
        {
            OnShow?.Invoke(message, ToastLevel.Error);
            StartCountdown();
        }

        private void StartCountdown()
        {
            SetCountdown();
            if (Countdown.Enabled)
            {
                Countdown.Stop();
                Countdown.Start();
            }
            else
            {
                Countdown.Start();
            }
        }

        private void SetCountdown()
        {
            if (Countdown == null)
            {
                Countdown = new System.Timers.Timer(10000);
                Countdown.Elapsed += HideToast;
                Countdown.AutoReset = false;
            }
        }

        private void HideToast(object source, ElapsedEventArgs args)
        {
            OnHide?.Invoke();
        }

        public void Dispose()
        {
            Countdown?.Dispose();
        }
    }
}
