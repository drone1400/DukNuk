using System.Windows;
using DukNuk.Wpf.Helpers;

namespace DukNuk.Wpf.Test {
    public partial class App : Application {

        protected override void OnStartup(StartupEventArgs e) {
            // NOTE: this needs to be called after the App's InitializeComponent!
            // otherwise the ThemeManager's resource dictionaries will get replaced during initialization...
            ThemeManager.Default.InitializeAppResources("Default Blue");
            
            //TODO, other startup code here...

        }
    }
}

