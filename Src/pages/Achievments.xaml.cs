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
    /// Interaktionslogik für Achievments.xaml
    /// </summary>
    public partial class Achievments : Page
    {
        public Achievments()
        {
            InitializeComponent();
            Acheavments.init();
            if (Acheavments.Acheavment1 == 1)
            {
                Acheavment_1.Text = "☑";
            }
            else
            {
                Acheavment_1.Text = "☐";
            }

            if (Acheavments.Acheavment2 == 1)
            {
                Acheavment_2.Text = "☑";
            }
            else
            {
                Acheavment_2.Text = "☐";
            }

            if (Acheavments.Acheavment3 == 1)
            {
                _Acheavment_3.Text = "☑";
            }
            else
            {
                _Acheavment_3.Text = "☐";
            }
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
            Line_Zurück.Opacity = 1;
        }

        private void Button_Zurück_MouseLeave(object sender, MouseEventArgs e)
        {
            Line_Zurück.Opacity = 0;
        }
    }
}
