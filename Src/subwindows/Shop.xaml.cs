using System;
using System.Collections.Generic;
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

namespace _5_Jahre_Hoelle.subwindows
{
    /// <summary>
    /// Interaktionslogik für Shop.xaml
    /// </summary>
    public partial class Shop : Window
    {
        public Shop()
        {
            InitializeComponent();
        }

        private void Button_Monster_Click(object sender, RoutedEventArgs e)
        {
            // Noten abziehen buff adden oder was halt auch immer der macht
        }

        private void Button_Redbull_Click(object sender, RoutedEventArgs e)
        {
            // Noten abziehen buff adden oder was halt auch immer der macht
        }

        private void Button_Leberkase_Click(object sender, RoutedEventArgs e)
        {
            // Noten abziehen buff adden oder was halt auch immer der macht
        }

        private void Button_Monster_MouseEnter(object sender, MouseEventArgs e)
        {
            TxtBlock_MonsterDescription.Foreground = Brushes.Black;
        }

        private void Button_Monster_MouseLeave(object sender, MouseEventArgs e)
        {
            TxtBlock_MonsterDescription.Foreground = Brushes.Transparent;
        }

        private void Button_Redbull_MouseEnter(object sender, MouseEventArgs e)
        {
            TxtBlock_RedbullDescription.Foreground = Brushes.Black;
        }

        private void Button_Redbull_MouseLeave(object sender, MouseEventArgs e)
        {
            TxtBlock_RedbullDescription.Foreground= Brushes.Transparent;
        }

        private void Button_Leberkase_MouseEnter(object sender, MouseEventArgs e)
        {
            TxtBlock_LeberkaseDescription.Foreground = Brushes.Black;
        }

        private void Button_Leberkase_MouseLeave(object sender, MouseEventArgs e)
        {
            TxtBlock_LeberkaseDescription.Foreground= Brushes.Transparent;
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
