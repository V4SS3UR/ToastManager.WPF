using System;
using ToastManager.Core;
using ToastManager.WPF.View;

namespace ToastManager.Core
{
    public static class ToastManager
    {
        public static Action<object> ShowToastCommand;
        public static Action CloseToastCommand;

        public static void ShowWarningToast(string message, string title = "Warning")
        {
            ShowToast(message, title, ToastType.Warning, ToastButton.Ok);
        }
        public static void ShowErrorToast(string message, string title = "Error")
        {
            ShowToast(message, title, ToastType.Error, ToastButton.Ok);
        }
        public static void ShowSuccessToast(string message, string title = "Success")
        {
            ShowToast(message, title, ToastType.Success, ToastButton.Ok);
        }
        public static void ShowInfoToast(string message, string title = "Info")
        {
            ShowToast(message, title, ToastType.Info, ToastButton.Ok);
        }

        public static void ShowToast(string message, string title, ToastType toastType, ToastButton toastButton)
        {
            var toastView = new Toast_View(message, title, toastType, toastButton);
            toastView.OnOkButtonClick += () => CloseToast();
            toastView.OnExitButtonClick += () => CloseToast();

            ShowToast(toastView);
        }

        public static void ShowToast(object view)
        {
            ShowToastCommand?.Invoke(view);
        }

        public static void CloseToast()
        {
            CloseToastCommand?.Invoke();
        }

    }
}
