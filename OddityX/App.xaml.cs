using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Oddity;
using Serilog;
using Serilog.Core;
using System;
using UnhandledExceptionEventArgs = Microsoft.UI.Xaml.UnhandledExceptionEventArgs;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace OddityX
{
    public delegate void Notify();
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            UnhandledException += OnUnhandledException;
        }

        public static bool TryGoBack(Frame contentFrame)
        {
            if (contentFrame.CanGoBack)
            {
                contentFrame.GoBack();
                return true;
            }
            return false;
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // Occurs when an exception is not handled on the UI thread.
            Log.Error($"Error! {e.Message}");

            // if you want to suppress and handle it manually, 
            // otherwise app shuts down.
            e.Handled = true;
            if (ErrorOccured is not null)
            {
                LastException = e.Exception;
                ErrorOccured.Invoke();
            }
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Title = "OddityX";
            m_window.Activate();
            OddityCore = new OddityCore();
        }

        ~App()
        {
            Log.CloseAndFlush();
        }


        public static MainWindow m_window;
        public static OddityCore OddityCore;
        public static event Notify ErrorOccured;
        public static Exception LastException;
    }
}
