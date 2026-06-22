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
    /// Interaktionslogik für Winscreen.xaml
    /// </summary>
    public partial class Winscreen : Page
    {
        public Winscreen()
        {
            InitializeComponent();

            Logger.logger.Debug("Winscreen initialized");
            Logger.logger.Debug("Winscreen Page was opened");


        }
        private void Button_Zurück_Click(object sender, RoutedEventArgs e)
        {
            Logger.logger.Debug("button Back to Menu was pressed");

            this.Content = new Frame
            {

                Content = new pages.menue_page()
            };
        }

        private void Button_Zurück_MouseEnter(object sender, MouseEventArgs e)
        {
            polygon_zurück.Opacity = 1;
            Logger.logger.Debug("mouse Entered Button Back to Menu ");
        }

        private void Button_Zurück_MouseLeave(object sender, MouseEventArgs e)
        {
            polygon_zurück.Opacity = 0;
            Logger.logger.Debug("Mouse left Button Back Menu was pressed");
        }
    }
}