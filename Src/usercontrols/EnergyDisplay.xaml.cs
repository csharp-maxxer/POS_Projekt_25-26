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

namespace _5_Jahre_Hoelle.usercontrols
{
    /// <summary>
    /// Interaktionslogik für EnergyDisplay.xaml
    /// </summary>
    public partial class EnergyDisplay : UserControl
    {
        public EnergyDisplay()
        {
            InitializeComponent();
            Logger.logger.Debug("EnergyDisplay initialized");
        }

        public void SetEnergy(double energy)
        {
            Logger.logger.Debug("Energy was updated");

            if (energy > 100)
            {
                energy = 100;
            }

            if (energy < 0)
            {
                energy = 0;
            }

            EnergyNumber.Content = $"{energy}";
            // chatgpt anfang: gib mir die rechnung mit der ich die progressbar richtig anzeige
            EnergyBar.Width = 216 * energy / 100;
            // chatgpt ende
            if (energy > 60)
            {
                EnergyBar.Fill = Brushes.Green;
                EnergyNumber.Foreground = Brushes.White;
            }
            else if (energy > 30)
            {
                EnergyBar.Fill = Brushes.Yellow;
                EnergyNumber.Foreground = Brushes.Yellow;
            }
            else
            {
                EnergyBar.Fill = Brushes.Red;
                EnergyNumber.Foreground = Brushes.Red;
            }
        }
    }
}