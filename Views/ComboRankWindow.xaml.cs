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

namespace TypingCombo.Views
{
    /// <summary>
    /// ComboRankWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ComboRankWindow : Window
    {
        public ComboRankWindow()
        {
            InitializeComponent();
            this.MouseLeftButtonDown += (sender, e) => this.DragMove();
        }

        public void UpdateImage(BitmapImage? image)
        {
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
