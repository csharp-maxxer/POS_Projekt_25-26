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
    /// Interaktionslogik für Grade_uc.xaml
    /// </summary>
    public partial class Grade_uc : UserControl
    {
        private double note;

        public Grade_uc()
        {
            InitializeComponent();
            Logger.logger.Debug("Grade_uc initialized");

            SetNote(70);
        }

        public void SetNote(double newNote)
        {
            note = newNote;
            Logger.logger.Debug("Grade was updated");

            Grade1.Visibility = Visibility.Collapsed;
            Grade2.Visibility = Visibility.Collapsed;
            Grade3.Visibility = Visibility.Collapsed;
            Grade4.Visibility = Visibility.Collapsed;
            Grade5.Visibility = Visibility.Collapsed;

            if (note >= 90)
            {
                Grade1.Visibility = Visibility.Visible;
            }
            else if (note >= 80)
            {
                Grade2.Visibility = Visibility.Visible;
            }
            else if (note >= 70)
            {
                Grade3.Visibility = Visibility.Visible;
            }
            else if (note >= 60)
            {
                Grade4.Visibility = Visibility.Visible;
            }
            else if (note < 60)
            {
                Grade5.Visibility = Visibility.Visible;
            }
        }
    }
}