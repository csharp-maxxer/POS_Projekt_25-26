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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _5_Jahre_Hoelle.pages
{
    /// <summary>
    /// Interaktionslogik für choose_difficulty.xaml
    /// </summary>
    public partial class choose_difficulty : Page
    {
        public choose_difficulty()
        {
            InitializeComponent();
            Logger.logger.Debug("Choose Difficulty Page was loaded");
            Game.animation_fromblack(Diff_can, 2);
        }

        private async void Button_Normal_Click(object sender, RoutedEventArgs e)
        {
            Logger.logger.Debug("Normal Difficulty Button was pressed");
            statistics.Attemps += 1;
            
            Acheavments.Acheavment1 = 1;
           
            await Game.animation_Toblack(Diff_can, 1);
            this.Content = new Frame
            {
                Content = new pages.Game(1)
            };

        }

        private void Button_Normal_MouseEnter(object sender, MouseEventArgs e)
        {
            Logger.logger.Debug("Mouse Entered Normal Difficulty Button");
            polygon_Normal.Opacity = 0.8;
        }

        private void Button_Normal_MouseLeave(object sender, MouseEventArgs e)
        {
            Logger.logger.Debug("Mouse left Normal Difficulty Button");
            polygon_Normal.Opacity = 0;
        }

        private async void  Button_Hard_Click(object sender, RoutedEventArgs e)
        {
            Logger.logger.Debug("Hard Difficulty Button was pressed");
            statistics.Attemps += 1; 
            Acheavments.Acheavment1 = 1;
            await Game.animation_Toblack(Diff_can, 1);
            this.Content = new Frame
            {
                Content = new pages.Game(0.75)
            };
        }

        private void Button_Hard_MouseEnter(object sender, MouseEventArgs e)
        {
            Logger.logger.Debug("Mouse Entered Normal Difficulty Button");
            polygon_Hard.Opacity = 0.8;
        }

        private void Button_Hard_MouseLeave(object sender, MouseEventArgs e)
        {
            Logger.logger.Debug("Mouse left Normal Difficulty Button");
            polygon_Hard.Opacity = 0;
        }

        private async void Button_Zurück_Click(object sender, RoutedEventArgs e)
        {
            Logger.logger.Debug("Back to Menu Button was pressed");
            await Game.animation_Toblack(Diff_can, 1);
            this.Content = new Frame
            {
                Content = new pages.menue_page()
            };
        }
        

        private void Button_Zurück_MouseEnter(object sender, MouseEventArgs e)
        {
            Logger.logger.Debug("Mouse Entered Normal Difficulty Button");
            Line_Zurück.Opacity = 1;
        }

        private void Button_Zurück_MouseLeave(object sender, MouseEventArgs e)
        {
            Logger.logger.Debug("Mouse left Normal Difficulty Button");
            Line_Zurück.Opacity = 0;
        }
    }
}
