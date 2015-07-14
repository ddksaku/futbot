using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FutbotWeb;
using System.Threading;
using Futbot.Log;

namespace FutbotSandbox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected void TestLogin(object sender, RoutedEventArgs e)
        {
            FutbotWebManager.AddScript(new LoginScript(), TestService.TestAccounts[0], TestService.TestFifa[0]);
            while (FutbotWebManager.ActiveThreads > 0)
            {
                Thread.Sleep(500);
                LogText.Text += SandboxLogger.Log.GetText();
            }
        }        
    }
}
