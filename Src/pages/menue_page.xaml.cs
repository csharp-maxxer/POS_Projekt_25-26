using _5_Jahre_Hoelle.classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Channels;
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
    /// Interaktionslogik für menue_page.xaml
    /// </summary>
    public partial class menue_page : Page
    {
        public menue_page()
        {
            
            InitializeComponent();
            Game.animation_fromblack(Menu_canva, 1);
        }

        private async void Button_Start_Click(object sender, RoutedEventArgs e)
        {
            File.WriteAllText("GameTracer.txt", "1");
            GameTracer.LevelNumber = 1;
            await Game.animation_Toblack(Menu_canva, 1);
           
            this.Content = new Frame
            {
                Content = new pages.choose_difficulty()
            };
            
        }

        private async void Button_Stats_Click(object sender, RoutedEventArgs e)
        {
            await Game.animation_Toblack(Menu_canva, 1);
            this.Content = new Frame
            {
                Content = new pages.Stats()
            };
        }

        private async void Button_Acheavments_Click(object sender, RoutedEventArgs e)
        {
            await Game.animation_Toblack(Menu_canva, 1);
            this.Content = new Frame
            {
                Content = new pages.Achievments()
            };
        }

        private async void Button_Load_Click(object sender, RoutedEventArgs e)
        {
            if (!File.Exists("save.txt"))
            {
                MessageBox.Show("Bitte speichern Sie zuerst einen Spielstand bevor sie probieren einen zu laden.");
                return;
            }
            else
            {
                string currentFloor = File.ReadAllText("GameTracer.txt");
                GameTracer.LevelNumber = Convert.ToInt32(currentFloor);
            }

            statistics.Attemps += 1;

            await Game.animation_Toblack(Menu_canva, 1);
            this.Content = new Frame
            {
                Content = new pages.Game()
            };
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            // KI: ChatGPT
            // Promt: Wie schließe ich das ganze programm in einer page this.close() geht nicht 
            // KI Anfang: 
            Application.Current.Shutdown();
            // KI Ende
        }





        private void Button_Start_MouseEnter(object sender, MouseEventArgs e)
        {
            polygon_Start.Opacity = 0.8;
        }

        private void Button_Start_MouseLeave(object sender, MouseEventArgs e)
        {
            polygon_Start.Opacity = 0;
        }

        private void Button_Load_MouseEnter(object sender, MouseEventArgs e)
        {
            polygon_Load.Opacity = 0.8;
        }

        private void Button_Load_MouseLeave(object sender, MouseEventArgs e)
        {
            polygon_Load.Opacity = 0;
        }

        private void Button_Exit_MouseEnter(object sender, MouseEventArgs e)
        {
            polygin_Exit.Opacity = 0.8;
        }

        private void Button_Acheavments_MouseLeave(object sender, MouseEventArgs e)
        {
            polygon_Achievments.Opacity = 0;
        }

        private void Button_Stats_MouseEnter(object sender, MouseEventArgs e)
        {
            polygom_Stats.Opacity = 0.8;
        }

        private void Button_Stats_MouseLeave(object sender, MouseEventArgs e)
        {
            polygom_Stats.Opacity = 0;
        }

        private void Button_Acheavments_MouseEnter(object sender, MouseEventArgs e)
        {
            polygon_Achievments.Opacity = 0.8;
        }

        private void Button_Exit_MouseLeave(object sender, MouseEventArgs e)
        {
            polygin_Exit.Opacity = 0;
        }
    }
}
