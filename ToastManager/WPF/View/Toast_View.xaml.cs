using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using ToastManager.Core;

namespace ToastManager.WPF.View
{
    /// <summary>
    /// Logique d'interaction pour Toast_View.xaml
    /// </summary>
    public partial class Toast_View : UserControl, INotifyPropertyChanged
    {
        public event Action OnOkButtonClick;
        public event Action OnYesButtonClick;
        public event Action OnNoButtonClick;
        public event Action OnCancelButtonClick;
        public event Action OnExitButtonClick;


        public RelayCommand OkButtonCommand { get; set; }
        public RelayCommand YesButtonCommand { get; set; }
        public RelayCommand NoButtonCommand { get; set; }
        public RelayCommand CancelButtonCommand { get; set; }
        public RelayCommand ExitButtonCommand { get; set; }


        public string _title; public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }

        public string _message; public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged(); }
        }

        private ToastType _toastType; public ToastType ToastType
        {
            get { return _toastType; }
            set { _toastType = value; OnPropertyChanged(); }
        }

        private ToastButton _toastButton; public ToastButton ToastButton
        {
            get { return _toastButton; }
            set { _toastButton = value; OnPropertyChanged(); }
        }

        private bool _isDarkBackground; public bool IsDarkBackground
        {
            get { return _isDarkBackground; }
            set { _isDarkBackground = value; OnPropertyChanged(); }
        }



        public Toast_View()
        {
            InitializeComponent();

            this.DataContext = this;

            this.OkButtonCommand = new RelayCommand(command => OnOkButtonClick?.Invoke());
            this.YesButtonCommand = new RelayCommand(command => OnYesButtonClick?.Invoke());
            this.NoButtonCommand = new RelayCommand(command => OnNoButtonClick?.Invoke());
            this.CancelButtonCommand = new RelayCommand(command => OnCancelButtonClick?.Invoke());
            this.ExitButtonCommand = new RelayCommand(command => OnExitButtonClick?.Invoke());
        }

        public Toast_View(string message, string title = "Info", 
            ToastType toastType = ToastType.Info, 
            ToastButton toastButton = ToastButton.Yes | ToastButton.No,
            bool isDarkBackground = false) : this()
        {
            this.Message = message;
            this.Title = title;
            this.ToastType = toastType;
            this.ToastButton = toastButton;
            this.IsDarkBackground = isDarkBackground;
        }


        




        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
