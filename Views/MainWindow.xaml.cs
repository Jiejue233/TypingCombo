using System.Text;
using System.Text.Json;
using System.IO;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using TypingCombo.src.Helpers;
using TypingCombo.Views;


namespace TypingCombo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string configFilePath = "Assets/Configs/config.json";
        string jsonContent;
        Config config;
        ComboRankWindow? comboRankWindow;

        private const int WH_KEYBOARD_LL = 13;
        private const int WH_MOUSE_LL = 14;

        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;

        private const int WM_MOUSEMOVE = 0x0200;
        private const int WM_MOUSEWHEEL = 0x020A;

        private Dictionary<int, bool> VK_KEYBOARD_ESCAPE = new Dictionary<int, bool>();

        public int ActionCount { get; set; }
        private DateTime _startTime;

        // 键盘钩子的回调函数的委托声明
        private delegate IntPtr KeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private KeyboardProc _keyboardProc;

        // 鼠标钩子的回调函数的委托声明
        private delegate IntPtr MouseProc(int nCode, IntPtr wParam, IntPtr lParam);
        private MouseProc _mouseProc;

        // 钩子句柄
        private IntPtr _keyboardHookID = IntPtr.Zero;
        private IntPtr _mouseHookID = IntPtr.Zero;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, KeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, MouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);


        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string moduleName);


        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public IntPtr dwExtraInfo;
        }


        public MainWindow()
        {
            InitializeComponent();
            _startTime = DateTime.Now;
            ReloadConfig();

            ReloadEscapeList();


            SetupHook();

        }

        public void ReloadConfig()
        {
            jsonContent = File.ReadAllText(configFilePath);
            config = JsonSerializer.Deserialize<Config>(json: jsonContent);
        }


        public void ReloadEscapeList()
        {
            foreach (int keyValue in Enum.GetValues(typeof(Keys)))
            {
                if (!VK_KEYBOARD_ESCAPE.ContainsKey(keyValue))
                {
                    VK_KEYBOARD_ESCAPE.Add(keyValue, false); // 初始化时将所有键的状态设为false
                }
            }


            foreach (int escapeKey in config.KeycodeEscaper)
            {
                VK_KEYBOARD_ESCAPE[escapeKey] = true;
            }
        }


        private void SetupHook()
        {
            // 实例化委托
            _keyboardProc = KeyboardHookCallback;
            _mouseProc = MouseHookCallback;
            // 设置钩子
            _keyboardHookID = SetHook(WH_KEYBOARD_LL, _keyboardProc);
            _mouseHookID = SetHook(WH_MOUSE_LL, _mouseProc);
        }

        private IntPtr SetHook(int idHook, KeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            {
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    return SetWindowsHookEx(idHook, proc, GetModuleHandle(curModule.ModuleName), 0);
                }
            }
        }

        private IntPtr SetHook(int idHook, MouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            {
                using (ProcessModule curModule = curProcess.MainModule)
                {
                    return SetWindowsHookEx(idHook, proc, GetModuleHandle(curModule.ModuleName), 0);
                }
            }
        }

        private IntPtr KeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN))
            {
                var keyboardHookStruct = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                int vkCode = keyboardHookStruct.vkCode;

                // some keyboard escape value
                if (VK_KEYBOARD_ESCAPE[vkCode])
                {
                    return CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
                }
                ActionCount++;
            }
            UpdateAPM();
            return CallNextHookEx(_keyboardHookID, nCode, wParam, lParam);
        }

        private IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                // some mouse escape value
                if (wParam == WM_MOUSEMOVE || wParam == WM_MOUSEWHEEL)
                {
                    return CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
                }
                ActionCount++;
            }
            UpdateAPM();
            return CallNextHookEx(_mouseHookID, nCode, wParam, lParam);
        }

        private void Windows_Closed(object sender, EventArgs e)
        {
            UnhookWindowsHookEx(_keyboardHookID);
            UnhookWindowsHookEx(_mouseHookID);
            if (comboRankWindow != null)
            {
                comboRankWindow.Close();
                comboRankWindow = null;
            }
        }


        private void ToggleRankWindow_Checked(object sender, RoutedEventArgs e)
        {
            if (comboRankWindow == null)
            {
                comboRankWindow = new ComboRankWindow();
                comboRankWindow.Show();
            }
        }

        private void ToggleRankWindow_Unchecked(object sender, RoutedEventArgs e)
        {
            if (comboRankWindow != null)
            {
                comboRankWindow.Close();
                comboRankWindow = null;
            }
        }


        private void UpdateAPM()
        {
            TimeSpan elapsedTime = DateTime.Now - _startTime;
            double minutes = elapsedTime.TotalMinutes;
            if (minutes > 0)
            {
                double apm = ActionCount / (minutes+1);
                Dispatcher.Invoke(() => { ApmTextBlock.Text = $"APM: {apm:F2}"; });
                if (comboRankWindow != null)
                {
                    string imageFolder = @$"Resources/Images/FrontSet/{config.SelectedFrontSet}/";
                    if (apm >= config.RankRange.SS)
                    {
                        BitmapImage image = new BitmapImage(new Uri(imageFolder + "SSS.png", UriKind.RelativeOrAbsolute));
                        comboRankWindow.UpdateImage(image);
                    }
                    else if (apm >= config.RankRange.S)
                    {
                        BitmapImage image = new BitmapImage(new Uri(imageFolder + "SS.png", UriKind.RelativeOrAbsolute));
                        comboRankWindow.UpdateImage(image);
                    }
                    else if (apm >= config.RankRange.A)
                    {
                        BitmapImage image = new BitmapImage(new Uri(imageFolder + "S.png", UriKind.RelativeOrAbsolute));
                        comboRankWindow.UpdateImage(image);
                    }
                    else if (apm >= config.RankRange.B)
                    {
                        BitmapImage image = new BitmapImage(new Uri(imageFolder + "A.png", UriKind.RelativeOrAbsolute));
                        comboRankWindow.UpdateImage(image);
                    }
                    else if (apm >= config.RankRange.C)
                    {
                        BitmapImage image = new BitmapImage(new Uri(imageFolder + "B.png", UriKind.RelativeOrAbsolute));
                        comboRankWindow.UpdateImage(image);
                    }
                    else if (apm >= config.RankRange.NONE)
                    {
                        BitmapImage image = new BitmapImage(new Uri(imageFolder + "C.png", UriKind.RelativeOrAbsolute));
                        comboRankWindow.UpdateImage(image);
                    }
                    else
                    {
                        comboRankWindow.UpdateImage(null);
                    }
                }
            }
        }
    }
}