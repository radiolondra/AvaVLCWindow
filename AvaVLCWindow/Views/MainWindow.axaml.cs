using Avalonia.Controls;
using Avalonia.Media;
using AvaVLCWindow.ViewModels;
using LibVLCSharp.Avalonia;

namespace AvaVLCWindow.Views
{
    public partial class MainWindow : Window
    {
        private static MainWindow _this;
        public VideoView _videoViewer;        
        public MainWindowViewModel viewModel;
        
        
        public MainWindow()
        {
            InitializeComponent();

            if (!Design.IsDesignMode)
            {
                _this = this;

                viewModel = new MainWindowViewModel();
                DataContext = viewModel;

                _videoViewer = this.Get<VideoView>("VideoViewer");

                Opened += MainWindow_Opened;
            }

        }

        public static MainWindow GetInstance()
        {
            return _this;
        }

        private void MainWindow_Opened(object? sender, System.EventArgs e)
        {            
            if (_videoViewer != null && viewModel.MediaPlayer != null)
            {
                _videoViewer.MediaPlayer = viewModel.MediaPlayer;                
                _videoViewer.MediaPlayer.SetHandle(_videoViewer.hndl);
                // or
                //_videoViewer.MediaPlayer.Hwnd = _videoViewer.hndl.Handle;               
                
            }
        }
    }
}
