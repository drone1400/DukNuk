using System.Windows;
using System.Windows.Controls;
using DukNuk.Wpf.Controls;
using DukNuk.Wpf.Test.ViewModels;
namespace DukNuk.Wpf.Test.Views {
    public partial class MainWindow : NukWindow {
        public MainWindow() {
            var vm = new MainWindowViewModel();
            vm.InitializeThemes();
            
            this.InitializeComponent();
            this.DataContext = vm;
        }
        private void ButtonTabPanelTop_OnClick(object sender, RoutedEventArgs e) {
            this.theBigTabControl.TabStripPlacement = Dock.Top;
        }
        private void ButtonTabPanelBottom_OnClick(object sender, RoutedEventArgs e) {
            this.theBigTabControl.TabStripPlacement = Dock.Bottom;
        }
        private void ButtonTabPanelLeft_OnClick(object sender, RoutedEventArgs e) {
            this.theBigTabControl.TabStripPlacement = Dock.Left;
        }
        private void ButtonTabPanelRight_OnClick(object sender, RoutedEventArgs e) {
            this.theBigTabControl.TabStripPlacement = Dock.Right;
        }

    }
}

