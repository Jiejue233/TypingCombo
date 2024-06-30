using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Shapes;
using TypingCombo.src.Helpers;

namespace TypingCombo.Views
{
    /// <summary>
    /// ComboRankWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ComboRankWindow : Window
    {
        private BitmapImage C;
        private BitmapImage B;
        private BitmapImage A;
        private BitmapImage S;
        private BitmapImage SS;
        private BitmapImage SSS;


        public ComboRankWindow(string imageFolder)
        {
            InitializeComponent();

            C = new(new Uri(imageFolder + "C.png", UriKind.RelativeOrAbsolute));
            B = new(new Uri(imageFolder + "B.png", UriKind.RelativeOrAbsolute));
            A = new(new Uri(imageFolder + "A.png", UriKind.RelativeOrAbsolute));
            S = new(new Uri(imageFolder + "S.png", UriKind.RelativeOrAbsolute));
            SS = new(new Uri(imageFolder + "SS.png", UriKind.RelativeOrAbsolute));
            SSS = new(new Uri(imageFolder + "SSS.png", UriKind.RelativeOrAbsolute));

            this.Loaded += ComboRankWindow_Loaded;
            this.Closing += ComboRankWindow_Closing;
            this.MouseLeftButtonDown += (sender, e) => this.DragMove();

        }

        private void ComboRankWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // 加载窗口位置
            this.Left = Properties.Settings.Default.WindowLeft;
            this.Top = Properties.Settings.Default.WindowTop;
        }

        private void ComboRankWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.WindowLeft = this.Left;
            Properties.Settings.Default.WindowTop = this.Top;
            Properties.Settings.Default.Save();
        }


        public void UpdateRank(RankState state)
        {
            BitmapImage? image = null;
            switch (state)
            {
                case RankState.SSS:
                    image = SSS;
                    break;
                case RankState.SS:
                    image = SS;
                    break;
                case RankState.S:
                    image = S;
                    break;
                case RankState.A:
                    image = A;
                    break;
                case RankState.B:
                    image = B;
                    break;
                case RankState.C:
                    image = C;
                    break;
                default:
                    break;
            }


            if (image != null)
            {
                image.Freeze();
                ComboRank.Source = image;
                this.Width = image.Width;
                this.Height = image.Height;
                this.Background = System.Windows.Media.Brushes.Transparent;
            }
            else
            {
                ComboRank.Source = null;
                this.Width = 100;
                this.Height = 100;
                this.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(51, 255, 255, 255));  // #33FFFFFF
            }
        }

    }
}
