using TestApplication.Core;
using System.Windows;
using ToastManager;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Controls;
using ToastManager.Core;
using RelayCommand = TestApplication.Core.RelayCommand;

namespace TestApplication
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public RelayCommand WarningButtonCommand { get; set; }
        public RelayCommand ErrorButtonCommand { get; set; }
        public RelayCommand SuccessButtonCommand { get; set; }
        public RelayCommand InfoButtonCommand { get; set; }
        public RelayCommand CustomToastButtonCommand { get; set; }
        public RelayCommand CustomViewButtonCommand { get; set; }

        private Toast toast 
        { 
            get => Toast.GetToast("MainWindowToast"); 
        }

        private bool _darkOverlay; public bool DarkOverlay
        {
            get { return _darkOverlay; }
            set { _darkOverlay = value; OnPropertyChanged(); }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            var toast = Toast.GetToast("MainWindowToast");

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
            CustomToastButtonCommand = new RelayCommand((parameter) =>
            {
                ShowCustomToast();
            });
            CustomViewButtonCommand = new RelayCommand((parameter) =>
            {
                ShowCustomView();
            });




            this.Loaded += (sender, e) =>
            {
                //Bring a welcome message when first loaded
                var message = "Hi, welcome to ToastManager!";
                var title = "Information";
                toast.ShowInfoToast(message, title, darkOverlay: true);
            };
        }

        private void ShowCustomToast()
        {
            var toastDialog = new ToastDialog("This is a custom toast", "title", ToastType.Info, ToastButton.Ok | ToastButton.Yes | ToastButton.No | ToastButton.Cancel, DarkOverlay);
            toastDialog.OnOkButtonClick += () => toast.CloseToast();
            toastDialog.OnYesButtonClick += () => toast.CloseToast();
            toastDialog.OnNoButtonClick += () => toast.CloseToast();
            toastDialog.OnCancelButtonClick += () => toast.CloseToast();
            toast.ShowToast(toastDialog);
        }
        private void ShowCustomView()
        {
            //Create a simple custom control
            var userControl = new UserControl
            {
                Width = 300,
                Height = 300,
                Background = Brushes.DarkGray
            };

            //Define content with a textblock and a button
            var content = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Center,
            };
            content.Children.Add(new TextBlock()
            {
                Text = "This is a custom toast.\nWith a custom view.",
                TextAlignment = TextAlignment.Center,
                Foreground = Brushes.White,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            });
            content.Children.Add(new Button()
            {
                Content = "Close toast",
                Background = Brushes.White,
                Foreground = Brushes.Black,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Command = new RelayCommand((parameter) =>
                {
                    toast.CloseToast();
                })
            });

            userControl.Content = content;

            toast.ShowToast(userControl);
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
