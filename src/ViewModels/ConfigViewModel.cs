using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TypingCombo.src.Helpers;
using TypingCombo.src.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using TypingCombo.src.Helpers.Commands;
namespace TypingCombo.src.ViewModels
{
    public class ConfigViewModel : ViewModelBase
    {
        //public Config config;

        private ObservableCollection<string> presetList;
        private ObservableCollection<string> frontSetList;

        public ObservableCollection<string> PresetList => presetList;
        public ObservableCollection<string> FrontSetList => frontSetList;

        private ICommand refreshButton;

        private ICommand saveButton;

        public string SelectedPreset { 
            get 
            {
                return Properties.Settings.Default.SelectedPreset; 
            } 
            set
            {
                Properties.Settings.Default.SelectedPreset = value;
                OnPropertyChanged(nameof(SelectedPreset));
            } 
        }

        public string SelectedFrontSet
        {
            get
            {
                return Properties.Settings.Default.SelectedFrontSet;
            }
            set
            {
                Properties.Settings.Default.SelectedFrontSet = value;
                OnPropertyChanged(nameof(SelectedFrontSet));
            }
        }

        public bool ComboWindowOpen
        {
            get
            {
                return Properties.Settings.Default.ComboWindowOpen;
            }
            set
            {
                Properties.Settings.Default.ComboWindowOpen = value;
                OnPropertyChanged(nameof(ComboWindowOpen));
            }
        }


        public ICommand RefreshButton => refreshButton;
        public ICommand SaveButton => saveButton;


        public ConfigViewModel()
        {
            //Debug.WriteLine(Properties.Settings.Default.SelectedPreset);
            presetList = new ObservableCollection<string>();
            frontSetList = new ObservableCollection<string>();
            ReloadList();

            refreshButton = new RelayCommand(RefreshCommand);
            saveButton = new RelayCommand(SaveCommand);
        }

        private void ReloadList()
        {
            presetList.Clear();
            frontSetList.Clear();
            string trimed;
            List<string> temp = FolderExplorer.Explore("Assets/Presets/", ".json");
            foreach (string preset in temp)
            {
                trimed = preset.Split("/")[2];
                presetList.Add(trimed[0..^5]);
            }
            temp = FolderExplorer.Explore("Resources/Images/FrontSet");
            foreach (string frontSet in temp)
            {
                trimed = frontSet.Split("/")[2];
                frontSetList.Add(trimed[(trimed.IndexOf('\\') + 1)..]);
            }
        }
        public void Save()
        {
            Properties.Settings.Default.Save();
        }
        public void SaveCommand(object? parameter)
        {
            Properties.Settings.Default.Save();
        }
        public void RefreshCommand(object? parameter)
        {
            ReloadList();
        }
    }
}
