using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TypingCombo.src.Helpers;
using TypingCombo.src.Helpers.Commands;

namespace TypingCombo.src.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public ConfigViewModel configViewModel { get; }
        public PresetViewModel presetViewModel { get; }

        private ICommand navigateSettingCommand;
        public ICommand NavigateSettingCommand => navigateSettingCommand;


        public int[] KeycodeEscaper
        {
            get { return presetViewModel.KeycodeEscaper; }
        }

        public float NONE
        {
            get { return presetViewModel.NONE; }
        }
        public float C
        {
            get { return presetViewModel.C; }
        }
        public float B
        {
            get { return presetViewModel.B; }
        }
        public float A
        {
            get { return presetViewModel.A; }
        }
        public float S
        {
            get { return presetViewModel.S; }
        }
        public float SS
        {
            get { return presetViewModel.SS; }
        }

        public string SelectedFrontSet
        {
            get { return configViewModel.SelectedFrontSet; }
        }

        public bool ComboWindowOpen
        {
            get { return configViewModel.ComboWindowOpen; }
        }

        public HomeViewModel(NavigationStore navigationStore)
        {
            navigateSettingCommand = new NavigateCommand<SettingViewModel>(navigationStore, ()=> new SettingViewModel(navigationStore));
        }

        public HomeViewModel(NavigationStore navigationStore, ConfigViewModel cvm, PresetViewModel pvm)
        {
            navigateSettingCommand = new NavigateCommand<SettingViewModel>(navigationStore, () => new SettingViewModel(navigationStore, cvm, pvm));
            configViewModel = cvm;
            presetViewModel = pvm;
        }

    }
}
