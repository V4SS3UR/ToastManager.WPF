using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using TestApplication.Core;
using ToastManager;

namespace TestApplication.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour View2.xaml
    /// </summary>
    public partial class View2 : UserControl, INotifyPropertyChanged
    {
        public RelayCommand WarningButtonCommand { get; set; }
        public RelayCommand ErrorButtonCommand { get; set; }
        public RelayCommand SuccessButtonCommand { get; set; }
        public RelayCommand InfoButtonCommand { get; set; }

        private string _toastNameWithGuid; public string ToastNameWithGuid
        {
            get { return _toastNameWithGuid; }
            set { _toastNameWithGuid = value; OnPropertyChanged(); }
        }


        private Toast toast 
        { 
            get => Toast.GetToast(this.ToastNameWithGuid); 
        }

        private bool _darkOverlay; public bool DarkOverlay 
        { 
            get { return _darkOverlay; }
            set { _darkOverlay = value; OnPropertyChanged(); } 
        }

        public View2()
        {
            InitializeComponent();
            this.DataContext = this;

            var shortGuid = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            ToastNameWithGuid = "View2Toast" + shortGuid;

            DarkOverlay = true;

            WarningButtonCommand = new RelayCommand((parameter) =>
            {
                toast.ShowWarningToast("This is a warning toast", darkOverlay: DarkOverlay);
            });
            ErrorButtonCommand = new RelayCommand((parameter) =>
            {
                toast.ShowErrorToast("This is an error toast", darkOverlay: DarkOverlay);
            });
            SuccessButtonCommand = new RelayCommand((parameter) =>
            {
                toast.ShowSuccessToast("This is a success toast", darkOverlay: DarkOverlay);
            });
            InfoButtonCommand = new RelayCommand((parameter) =>
            {
                toast.ShowInfoToast("This is an info toast", darkOverlay: DarkOverlay);
            });
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
