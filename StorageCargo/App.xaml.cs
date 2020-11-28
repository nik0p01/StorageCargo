
using NLog;
using StorageCargo.Exceptions;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace StorageCargo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {

            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "file.txt" };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            // Rules for mapping loggers to targets            
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

            // Apply config           
            NLog.LogManager.Configuration = config;

            ILogger logger = LogManager.GetCurrentClassLogger();

            logger.Error("UnhandledException caught : " + e.Exception.Message);
            if (e.Exception is FileNotFoundException)
            {
                MessageBox.Show(e.Exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
                e.Handled = true;
            }
            else if (e.Exception is ExcelProcessingException)
            {
                MessageBox.Show("An exception just occurred: " + e.Exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
                e.Handled = true;
            }
            else
            {
                MessageBox.Show("An unhandled exception just occurred: " + e.Exception.Message + ". The app will be closed", "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
                e.Handled = false;
            }
        }


    }
}