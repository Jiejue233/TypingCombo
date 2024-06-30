using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TypingCombo.src.Helpers;
using TypingCombo.src.Models;


namespace TypingCombo.src.ViewModels
{
    public class PresetViewModel : ViewModelBase
    {
        private const string PRESET_FOLDER_PATH = "Assets/Presets/";

        Presets presets;
        RankRange range;
        private string presetName;

        public float NONE 
        { 
            get { return range.NONE; }
            set
            {
                range.NONE = value; 
                OnPropertyChanged(nameof(NONE));
            }
        }
        public float C
        {
            get { return range.C; }
            set
            {
                range.NONE = value;
                OnPropertyChanged(nameof(C));
            }
        }
        public float B
        {
            get { return range.B; }
            set
            {
                range.NONE = value;
                OnPropertyChanged(nameof(B));
            }
        }
        public float A
        {
            get { return range.A; }
            set
            {
                range.NONE = value;
                OnPropertyChanged(nameof(A));
            }
        }
        public float S
        {
            get { return range.S; }
            set
            {
                range.NONE = value;
                OnPropertyChanged(nameof(S));
            }
        }
        public float SS
        {
            get { return range.SS; }
            set
            {
                range.NONE = value;
                OnPropertyChanged(nameof(SS));
            }
        }

        public int[] KeycodeEscaper
        {
            get { return presets.KeycodeEscaper; }
            set
            {
                presets.KeycodeEscaper = value;
                OnPropertyChanged();
            }
        }


        public PresetViewModel(string presetName)
        {
            Reload(presetName);
        }

        public void Save()
        {
            Presets content = new Presets()
            {
                RankRange = range,
                KeycodeEscaper = this.KeycodeEscaper
            };
            JsonHelper.WriteConfig<Presets>(content, PRESET_FOLDER_PATH + $"{presetName}.json");
        }

        public void Reload(string presetName)
        {
            this.presetName = presetName;
            presets = JsonHelper.ReadConfig<Presets>(PRESET_FOLDER_PATH + $"{presetName}.json");
            range = presets.RankRange;
        }

    }
}
