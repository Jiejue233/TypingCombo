using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TypingCombo.src.Helpers;
using TypingCombo.src.Helpers.Commands;

namespace TypingCombo.src.ViewModels
{
    public class SettingViewModel: ViewModelBase
    {
        public ConfigViewModel configViewModel { get; }
        public PresetViewModel presetViewModel { get; }

        private ICommand navigateHomeCommand;
        public ICommand NavigateHomeCommand => navigateHomeCommand;

        public int[] KeycodeEscaper
        {
            get { return presetViewModel.KeycodeEscaper; }
            set
            {
                presetViewModel.KeycodeEscaper = value;
                OnPropertyChanged();
            }
        }

        public string SelectedPreset
        {
            get
            {
                return configViewModel.SelectedPreset;
            }
            set
            {
                configViewModel.SelectedPreset = value;
                OnPropertyChanged(nameof(SelectedPreset));
            }
        }

        public string SelectedFrontSet
        {
            get
            {
                return configViewModel.SelectedFrontSet;
            }
            set
            {
                configViewModel.SelectedFrontSet = value;
                OnPropertyChanged(nameof(SelectedFrontSet));
            }
        }

        public ObservableCollection<string> PresetList => configViewModel.PresetList;
        public ObservableCollection<string> FrontSetList => configViewModel.FrontSetList;

        public SettingViewModel(NavigationStore navigationStore)
        {
            navigateHomeCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore));
        }
        public SettingViewModel(NavigationStore navigationStore, ConfigViewModel cvm, PresetViewModel pvm)
        {
            navigateHomeCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore, cvm, pvm));
            configViewModel = cvm;
            presetViewModel = pvm;
        }

        public void Save()
        {
            configViewModel.Save();
            presetViewModel.Save();
        }

        public void Load()
        {
            presetViewModel.Reload(SelectedPreset);
        }
    }
}
