using System;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Navigation;
using TypingCombo.src.Helpers;
using TypingCombo.src.ViewModels;

namespace TypingCombo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        
        protected override void OnStartup(StartupEventArgs e)
        {
            NavigationStore navigationStore = new NavigationStore();

            ConfigViewModel cvm = new ConfigViewModel();
            PresetViewModel pvm = new PresetViewModel(cvm.SelectedPreset);

            navigationStore.CurrentViewModel = new HomeViewModel(navigationStore, cvm, pvm);

            MainWindow mainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore)
            };

            MainWindow.Show();

            base.OnStartup(e);
        }
    }

}
