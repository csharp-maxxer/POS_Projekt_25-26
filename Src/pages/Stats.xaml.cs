using _5_Jahre_Hoelle.classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Printing;
using System.Text;
using System.Text.Json;
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
    /// Interaktionslogik für Stats.xaml
    /// </summary>
    public partial class Stats : Page
    {
        public Stats()
        {
            
            InitializeComponent();

            statistics.init();
            Game.animation_fromblack(Canva_Stats, 1);
            Label_attemps.Content = $"Attemps: {statistics.Attemps}";
            Label_Wins.Content = $"Wins: {statistics.Wins}";
            int nr_achievments = 0;
            if (!File.Exists("achievments.json"))
            {
                statistics.Achievments = 0;
            }
            else
            {
                string json = File.ReadAllText("achievments.json");
                Dictionary<string, int> data = JsonSerializer.Deserialize<Dictionary<string, int>>(json);
                foreach (KeyValuePair<string, int> item in data)
                {
                    if (item.Value == 1)
                        nr_achievments++;
                }
                statistics.Achievments = nr_achievments;
            }
            Label_acheavments.Content = $"Acheavments: {statistics.Achievments}";
        }

        private async void Button_Zurück_Click(object sender, RoutedEventArgs e)
        {
            await Game.animation_Toblack(Canva_Stats, 1);
            this.Content = new Frame
            {
                Content = new pages.menue_page()
            };
        }

        private void Button_Zurück_MouseEnter(object sender, MouseEventArgs e)
        {
            Line_Zurück.Opacity = 1;
        }

        private void Button_Zurück_MouseLeave(object sender, MouseEventArgs e)
        {
            Line_Zurück.Opacity = 0;
        }
    }
}
