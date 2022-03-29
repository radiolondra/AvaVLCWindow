using Avalonia.Controls;
using LibVLCSharp.Shared;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace AvaVLCWindow.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        LibVLC? _libVLC;
        public MediaPlayer? MediaPlayer;
        
        public MainWindowViewModel()
        {
            if (!Avalonia.Controls.Design.IsDesignMode)
            {
                var libVlcDirectoryPath = Path.Combine(Environment.CurrentDirectory, "libvlc", IsWin64() ? "win-x64" : "win-x86");
                Core.Initialize(libVlcDirectoryPath);

                _libVLC = new LibVLC(
                    enableDebugLogs: true
                    );
                _libVLC.Log += VlcLogger_Event;                

                MediaPlayer = new MediaPlayer(_libVLC) {};
                
            }
        }        

        private void VlcLogger_Event(object? sender, LogEventArgs l)
        {
            Debug.WriteLine(l.Message);
        }

        public void Play()
        {
            if (_libVLC != null && MediaPlayer != null)
            {
                using var media = new Media(
                    _libVLC, 
                    new Uri("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4")
                    );
                MediaPlayer.Play(media);
            }
        }

        public void Stop()
        {
            if (MediaPlayer != null)
            {
                MediaPlayer.Stop();
            }
        }

        public void Dispose()
        {
            MediaPlayer?.Dispose();
            _libVLC?.Dispose();
        }

        public static bool IsWin64()
        {            
            if (IntPtr.Size == 4)
            {
                return false;
            }
            return true;
        }
    }
}
