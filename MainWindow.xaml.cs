using System.Text;
using System.Text.Json;
using System.IO;
using System.Diagnostics;
using System.Drawing;
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
using TypingCombo.Views;
using TypingCombo.src.Models;
using TypingCombo.src.ViewModels;
using TypingCombo.src.Helpers;
using static System.Net.Mime.MediaTypeNames;


namespace TypingCombo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ComboRankWindow? comboRankWindow;
        public MainWindow()
        {
            InitializeComponent();
            this.Closing += MainWindow_Closing;

        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            if (comboRankWindow != null)
            {
                comboRankWindow.Close();
                comboRankWindow = null;
            }
        }
    }
}