using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using ToastManager.Core;

namespace ToastManager
{
    /// <summary>
    /// Logique d'interaction pour Toast.xaml
    /// </summary>
    public partial class Toast : UserControl, INotifyPropertyChanged
    {
        private static DependencyProperty ToastNameProperty = DependencyProperty.Register(nameof(ToastName), typeof(string), typeof(Toast), new PropertyMetadata(null, OnToastNameChanged));
        public string ToastName
        {
            get { return (string)GetValue(ToastNameProperty); }
            set { SetValue(ToastNameProperty, value); }
        }
        private static void OnToastNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Toast toast = d as Toast;

            if(e.OldValue != null)
            {
                ToastDictionary.UnregisterToast(e.OldValue.ToString());
            }
            if (e.NewValue != null)
            {
                ToastDictionary.RegisterToast(e.NewValue.ToString(), toast);
            }
        }


        private object _view; public object View
        {
            get { return _view; }
            set { _view = value; OnPropertyChanged(); }
        }


        public Toast()
        {
            InitializeComponent();
            DataContext = this;
        }


        public static Toast GetToast(string toastName) => ToastDictionary.GetToast(toastName);


        public void ShowWarningToast(string message, string title = "Warning", bool darkOverlay = false)
        {
            ShowToast(message, title, ToastType.Warning, ToastButton.Ok, darkOverlay);
        }
        public void ShowErrorToast(string message, string title = "Error", bool darkOverlay = false)
        {
            ShowToast(message, title, ToastType.Error, ToastButton.Ok, darkOverlay);
        }
        public void ShowSuccessToast(string message, string title = "Success", bool darkOverlay = false)
        {
            ShowToast(message, title, ToastType.Success, ToastButton.Ok, darkOverlay);
        }
        public void ShowInfoToast(string message, string title = "Info", bool darkOverlay = false)
        {
            ShowToast(message, title, ToastType.Info, ToastButton.Ok, darkOverlay);
        }

        public void ShowToast(string message, string title, ToastType toastType, ToastButton toastButton, bool darkOverlay = false)
        {
            var toastDialog = new ToastDialog(message, title, toastType, toastButton, darkOverlay);
            toastDialog.OnOkButtonClick += () => CloseToast();
            toastDialog.OnExitButtonClick += () => CloseToast();

            View = toastDialog;
        }
        public void ShowToast(object view)
        {
            View = view;
        }
        public void CloseToast()
        {
            View = null;
        }




        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
