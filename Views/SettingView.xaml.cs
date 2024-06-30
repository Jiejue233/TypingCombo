using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using TypingCombo.src.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace TypingCombo.Views
{
    /// <summary>
    /// SettingView.xaml 的交互逻辑
    /// </summary>
    public partial class SettingView : System.Windows.Controls.UserControl
    {
        private Dictionary<string, System.Windows.Controls.Button> _buttons = new Dictionary<string, System.Windows.Controls.Button>();
        private HashSet<int> _selectedKeys = new HashSet<int>();
        SettingViewModel vm;
        private bool _isEscaperDirty;
        private bool _isSettingDirty;
        public SettingView()
        {
            InitializeComponent();
            Loaded += SettingView_Loaded;
            
        }

        private void KeyInitializer()
        {
            _buttons.Add("Escape", btnEscape);
            _buttons.Add("F1", btnF1);
            _buttons.Add("F2", btnF2);
            _buttons.Add("F3", btnF3);
            _buttons.Add("F4", btnF4);
            _buttons.Add("F5", btnF5);
            _buttons.Add("F6", btnF6);
            _buttons.Add("F7", btnF7);
            _buttons.Add("F8", btnF8);
            _buttons.Add("F9", btnF9);
            _buttons.Add("F10", btnF10);
            _buttons.Add("F11", btnF11);
            _buttons.Add("F12", btnF12);
            _buttons.Add("Oemtilde", btnOemtilde); // The "`" key
            _buttons.Add("Oem3", btnOemtilde); // The "`" key
            _buttons.Add("D1", btnD1);
            _buttons.Add("D2", btnD2);
            _buttons.Add("D3", btnD3);
            _buttons.Add("D4", btnD4);
            _buttons.Add("D5", btnD5);
            _buttons.Add("D6", btnD6);
            _buttons.Add("D7", btnD7);
            _buttons.Add("D8", btnD8);
            _buttons.Add("D9", btnD9);
            _buttons.Add("D0", btnD0);
            _buttons.Add("OemMinus", btnOemMinus); // The "-" or "_" key
            _buttons.Add("OemPlus", btnOemPlus); // The "+" or "=" key
            _buttons.Add("Back", btnBack);
            _buttons.Add("Tab", btnTab);
            _buttons.Add("Q", btnQ);
            _buttons.Add("W", btnW);
            _buttons.Add("E", btnE);
            _buttons.Add("R", btnR);
            _buttons.Add("T", btnT);
            _buttons.Add("Y", btnY);
            _buttons.Add("U", btnU);
            _buttons.Add("I", btnI);
            _buttons.Add("O", btnO);
            _buttons.Add("P", btnP);
            _buttons.Add("OemOpenBrackets", btnOemOpenBrackets); // Changed from "["
            _buttons.Add("OemCloseBrackets", btnOemCloseBrackets); // Changed from "]"
            _buttons.Add("OemBackslash", btnOemPipe); // Changed from "\\"
            _buttons.Add("OemPipe", btnOemPipe);
            _buttons.Add("CapsLock", btnCapsLock);
            _buttons.Add("Capital", btnCapsLock);
            _buttons.Add("A", btnA);
            _buttons.Add("S", btnS);
            _buttons.Add("D", btnD);
            _buttons.Add("F", btnF);
            _buttons.Add("G", btnG);
            _buttons.Add("H", btnH);
            _buttons.Add("J", btnJ);
            _buttons.Add("K", btnK);
            _buttons.Add("L", btnL);
            _buttons.Add("OemSemicolon", btnOemSemicolon); // Changed from ";"
            _buttons.Add("OemQuotes", btnOemQuotes); // Changed from "'"
            _buttons.Add("Return", btnEnter); // Changed from "Enter"
            _buttons.Add("LeftShift", btnLShiftKey);
            _buttons.Add("Z", btnZ);
            _buttons.Add("X", btnX);
            _buttons.Add("C", btnC);
            _buttons.Add("V", btnV);
            _buttons.Add("B", btnB);
            _buttons.Add("N", btnN);
            _buttons.Add("M", btnM);
            _buttons.Add("OemComma", btnOemComma); // Changed from ","
            _buttons.Add("OemPeriod", btnOemPeriod); // Changed from "."
            _buttons.Add("OemQuestion", btnOemQuestion); // Changed from "/"
            _buttons.Add("RightShift", btnRShiftKey);
            _buttons.Add("LeftCtrl", btnLControlKey);
            _buttons.Add("LWin", btnLWin);
            _buttons.Add("LeftAlt", btnLAltKey); 
            _buttons.Add("System", btnLAltKey); // 
            _buttons.Add("Space", btnSpace);
            _buttons.Add("RightAlt", btnRAltKey);
            _buttons.Add("RightCtrl", btnRControlKey);
           /*_buttons.Add("Left", btnLeftArrow);
            _buttons.Add("Down", btnDownArrow);
            _buttons.Add("Right", btnRightArrow);
            _buttons.Add("Up", btnUpArrow);
            _buttons.Add("NumLock", btnNumLock);
            _buttons.Add("Divide", btnPadDivide);
            _buttons.Add("Multiply", btnPadMultiply);
            _buttons.Add("Subtract", btnPadMinus);
            _buttons.Add("Add", btnPadPlus);
            _buttons.Add("Enter", btnPadEnter);
            _buttons.Add("NumPad1", btnPad1);
            _buttons.Add("NumPad2", btnPad2);
            _buttons.Add("NumPad3", btnPad3);
            _buttons.Add("NumPad4", btnPad4);
            _buttons.Add("NumPad5", btnPad5);
            _buttons.Add("NumPad6", btnPad6);
            _buttons.Add("NumPad7", btnPad7);
            _buttons.Add("NumPad8", btnPad8);
            _buttons.Add("NumPad9", btnPad9);
            _buttons.Add("NumPad0", btnPad0);
            _buttons.Add("Decimal", btnPadPeriod);*/

        }


        private void KeyboardClear()
        {
            foreach (System.Windows.Controls.Button button in _buttons.Values)
            {
                if (button.Background == System.Windows.Media.Brushes.Yellow)
                {
                    button.Background = System.Windows.Media.Brushes.LightGray;
                }
            }
            _selectedKeys.Clear();
        }

        private void SettingView_Loaded(object sender, RoutedEventArgs e)
        {
            vm = DataContext as SettingViewModel;
            KeyInitializer();
            LoadKeyboard();
            Focus();
            _isEscaperDirty = false;
        }

        private void LoadKeyboard(bool Reload = false)
        {
            KeyboardClear();
            if (vm is not null)
            {
                if (Reload) vm.Load();
                foreach (int vk in vm.KeycodeEscaper)
                {
                    string? keyName = Enum.GetName((Key)vk);
                    if (keyName != null)
                    {
                        ToggleButtonSelection(_buttons[keyName], false);
                    }
                }
            }
            else
            {
                Debug.WriteLine("vm is null");
            }
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string keyName = e.Key.ToString();
            Debug.WriteLine(keyName);
            if (_buttons.TryGetValue(keyName, out System.Windows.Controls.Button button))
            {
                ToggleButtonSelection(button, true);
            }
        }

        private void Window_GotFocus(object sender, RoutedEventArgs e)
        {
            InputMethod.SetIsInputMethodEnabled(sender as UIElement, false);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.Button button)
            {
                ToggleButtonSelection(button, true);
            }
        }

        private void presetSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadKeyboard(Reload: true);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderToText(sender as Slider);
        }

        private void SliderToText(Slider slider)
        {
            if (vm is not null)
            {
                float threshold = (float)(slider.Value/100) * vm.presetViewModel.SS;
                switch (slider.Name.Substring(6))
                {
                    case "SS":
                        textS.Text = threshold.ToString();
                        vm.presetViewModel.S = threshold;
                        break;
                    case "S":
                        textA.Text = threshold.ToString();
                        vm.presetViewModel.A = threshold;
                        break;
                    case "A":
                        textB.Text = threshold.ToString();
                        vm.presetViewModel.B = threshold;
                        break;
                    case "B":
                        textC.Text = threshold.ToString();
                        vm.presetViewModel.C = threshold;
                        break;
                    case "C":
                        textNONE.Text = threshold.ToString();
                        vm.presetViewModel.NONE = threshold;
                        break;
                }
            }
        }

        private void ToggleButtonSelection(System.Windows.Controls.Button button, bool setDirty)
        {
            int virtualKey = int.Parse(button.Tag.ToString());
            //int virtualKey = (int) button.Tag;
            if (_selectedKeys.Contains(virtualKey))
            {
                _selectedKeys.Remove(virtualKey);
                button.Background = System.Windows.Media.Brushes.LightGray;
            }
            else
            {
                _selectedKeys.Add(virtualKey);
                button.Background = System.Windows.Media.Brushes.Yellow;
            }
            if (setDirty && !_isEscaperDirty)
            {
                _isEscaperDirty = true;
                keyboardSaveButton.Background = System.Windows.Media.Brushes.IndianRed;
            }
        }
        private void KeyboardSave_Click(object sender, RoutedEventArgs e)
        {
            vm.KeycodeEscaper = _selectedKeys.ToArray();
            if (_isEscaperDirty)
            {
                keyboardSaveButton.Background = System.Windows.Media.Brushes.LightGray;
                _isEscaperDirty = false;
                _isSettingDirty = true;
            }
        }

        private void SettingSaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isSettingDirty)
            {
                settingSaveButton.Background = System.Windows.Media.Brushes.LightGray;
                vm.Save();
                _isSettingDirty = true;
            }

        }
    }
}
