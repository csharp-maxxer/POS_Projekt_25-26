using _5_Jahre_Hoelle.classes;
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
        Player player;

        private void UpdateLabels()
        {
            lbl_energy_current.Content = $"current Energy: {player.Energy}";
            Lbl_grading_current.Content = $"current Grading: {player.Grade}%";
        }

        public Shop(Player player)
        {
            InitializeComponent();
            this.player = player;
            UpdateLabels();
        }

        private void Button_Monster_Click(object sender, RoutedEventArgs e)
        {
            player.Energy += 15;
            player.Grade -= 20;
            UpdateLabels();
            Acheavments.Acheavment2 = 1;
        }

        private void Button_Redbull_Click(object sender, RoutedEventArgs e)
        {
            player.Grade += 15;
            player.Energy -= 20;
            UpdateLabels();
            Acheavments.Acheavment2 = 1;
        }

        private void Button_Leberkase_Click(object sender, RoutedEventArgs e)
        {
            player.Energy += 25;
            player.Grade -= 30;
            UpdateLabels();
            Acheavments.Acheavment2 = 1;
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
            TxtBlock_LeberkaseDescription.Foreground = Brushes.Transparent;
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
