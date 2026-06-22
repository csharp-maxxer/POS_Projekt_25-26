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
            Logger.logger.Debug("Stats initialized");
            Logger.logger.Debug("Stats Page was opened");

            statistics.init();
            Logger.logger.Debug("Statistics initialized");

            Game.animation_fromblack(Canva_Stats, 1);

            Label_attemps.Content = $"Attemps: {statistics.Attemps}";
            Logger.logger.Debug("Attemps label was set");

            Label_Wins.Content = $"Wins: {statistics.Wins}";
            Logger.logger.Debug("Wins label was set");

            int nr_achievments = 0;
            if (!File.Exists("achievments.json"))
            {
                Logger.logger.Debug("achievments.json does not exist");

                statistics.Achievments = 0;
                Logger.logger.Debug("achievments was set to 0");
            }
            else
            {
                Logger.logger.Debug("achievments.json exists");

                string json = File.ReadAllText("achievments.json");
                Logger.logger.Debug("achievments.json was read");

                Dictionary<string, int> data = JsonSerializer.Deserialize<Dictionary<string, int>>(json);
                Logger.logger.Debug("achievments.json was deserialized");

                foreach (KeyValuePair<string, int> item in data)
                {
                    if (item.Value == 1)
                        nr_achievments++;
                }

                

                statistics.Achievments = nr_achievments;
                Logger.logger.Debug("Achievments amount was saved in statistics");
            }

            Label_acheavments.Content = $"Acheavments: {statistics.Achievments}";
            Logger.logger.Debug("acheavments label was set");
        }

        private async void Button_Zurück_Click(object sender, RoutedEventArgs e)
        {
            Logger.logger.Debug("Button back to Menu was pressed");

            await Game.animation_Toblack(Canva_Stats, 1);
            this.Content = new Frame
            {
                Content = new pages.menue_page()
            };
        }

        private void Button_Zurück_MouseEnter(object sender, MouseEventArgs e)
        {
            Line_Zurück.Opacity = 1;
            Logger.logger.Debug("Mouse wntered Button Back to Menu ");
        }

        private void Button_Zurück_MouseLeave(object sender, MouseEventArgs e)
        {
            Line_Zurück.Opacity = 0;
            Logger.logger.Debug("Mouse left button Back to Menu was pressed");
        }
    }
}