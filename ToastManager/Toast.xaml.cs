using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

            if (e.OldValue != null)
            {
                toast.UnregisterToast(e.OldValue.ToString());
            }
            if (e.NewValue != null)
            {
                toast.RegisterToast(e.NewValue.ToString());              
            }
        }  

        private object _view; public object View
        {
            get { return _view; }
            set { _view = value; OnPropertyChanged(); }
        }

        private bool _initRegistered;


        public Toast()
        {
            InitializeComponent();
            DataContext = this;

            this.Loaded += Toast_Loaded;
            this.Unloaded += Toast_Unloaded;
        }

        

        private void Toast_Loaded(object sender, RoutedEventArgs e)
        {
            //If the toast is not registered, register it
            if (!_initRegistered)
            {
                this.RegisterToast(this.ToastName);
            }            
        }
        private void Toast_Unloaded(object sender, RoutedEventArgs e)
        {
            //If the toast is registered, unregister it
            if (_initRegistered)
            {
                this.UnregisterToast(this.ToastName);
            }
        }

        private void RegisterToast(string toastName)
        {
            if (string.IsNullOrEmpty(toastName))
            {
                return;
            }

            // Retrieve the specific Window this toast is in
            var window = Window.GetWindow(this); 
            if (window != null)
            {
                ToastDictionary.RegisterToast(window, toastName, this);
                _initRegistered = true;
            }
        }
        private void UnregisterToast(string toastName)
        {
            if (string.IsNullOrEmpty(toastName))
            {
                return;
            }

            // Retrieve the specific Window this toast is in
            var window = Window.GetWindow(this); 
            if (window != null)
            {
                ToastDictionary.UnregisterToast(window, toastName);
                _initRegistered = false;
            }
        }

        public static Toast GetToast(Window window, string toastName)
        {
            return ToastDictionary.GetToast(window, toastName);
        }
        public static Toast GetToast(string toastName)
        {
            //Test if the toast is registered in a single instance
            IEnumerable<Toast> toasts = ToastDictionary.GetToasts(toastName);

            //If toats not null and contains only one element, return it
            if (toasts != null && toasts.Count() == 1)
            {
                return toasts.First();
            }

            //Else retrieve the correct toast from the active window
            var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (window != null)
            {
                return ToastDictionary.GetToast(window, toastName);
            }
            return null;
        }



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
