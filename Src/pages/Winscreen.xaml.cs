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


        }
        private void Button_Zurück_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new Frame
            {

                Content = new pages.menue_page()
            };
        }

        private void Button_Zurück_MouseEnter(object sender, MouseEventArgs e)
        {
            polygon_zurück.Opacity = 1;
        }

        private void Button_Zurück_MouseLeave(object sender, MouseEventArgs e)
        {
            polygon_zurück.Opacity = 0;
        }
    }
}
