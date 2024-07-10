using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using ToastManager.Core;

namespace ToastManager
{
    /// <summary>
    /// Logique d'interaction pour Toast_View.xaml
    /// </summary>
    public partial class ToastDialog : UserControl, INotifyPropertyChanged
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



        public ToastDialog()
        {
            InitializeComponent();

            DataContext = this;

            OkButtonCommand = new RelayCommand(command => OnOkButtonClick?.Invoke());
            YesButtonCommand = new RelayCommand(command => OnYesButtonClick?.Invoke());
            NoButtonCommand = new RelayCommand(command => OnNoButtonClick?.Invoke());
            CancelButtonCommand = new RelayCommand(command => OnCancelButtonClick?.Invoke());
            ExitButtonCommand = new RelayCommand(command => OnExitButtonClick?.Invoke());
        }

        public ToastDialog(string message, string title = "Info",
            ToastType toastType = ToastType.Info,
            ToastButton toastButton = ToastButton.Yes | ToastButton.No,
            bool isDarkBackground = false) : this()
        {
            Message = message;
            Title = title;
            ToastType = toastType;
            ToastButton = toastButton;
            IsDarkBackground = isDarkBackground;
        }




        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
