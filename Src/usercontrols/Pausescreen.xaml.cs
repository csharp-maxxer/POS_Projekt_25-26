using _5_Jahre_Hoelle.classes;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;

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

namespace _5_Jahre_Hoelle.usercontrols
{
    /// <summary>
    /// Interaktionslogik für Pausescreen.xaml
    /// </summary>
    public partial class Pausescreen : UserControl
    {
        // cbatgpt anfang: Mach mir den ContinueButton das er funktioniert(code)
        public event Action ContinueClicked;
        // chatgpt ende

        public Pausescreen()
        {
            InitializeComponent();
        }
        private string _savedata;

        public Pausescreen(string savedata)
        { 
            InitializeComponent();
            _savedata = savedata;
        }

        private void Button_continue_Click(object sender, RoutedEventArgs e)
        {
            // cbatgpt anfang: Mach mir den ContinueButton das er funktioniert(code)
            ContinueClicked?.Invoke();

            Panel parent = this.Parent as Panel;

            if (parent != null)
            {
                parent.Children.Remove(this);
            }
            // chatgpt ende
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            // chatgpt anfang: wie mach ich das die menue page nicht im usercontrol aufgeht sondern in der main page
            Window.GetWindow(this).Content = new Frame
            {
                NavigationUIVisibility = NavigationUIVisibility.Hidden,
                Content = new pages.menue_page()
            };
        }
            // chatgpt ende
        

        private void Button_savegame_Click(object sender, RoutedEventArgs e)
        {
            // chatgpt anfang: wie mach ich das die menue page nicht im usercontrol aufgeht sondern in der main page
            Window.GetWindow(this).Content = new Frame
            {
                NavigationUIVisibility = NavigationUIVisibility.Hidden,
                Content = new pages.menue_page()
            };
            // chatgpt ende
            File.WriteAllText("save.json", _savedata);
            File.WriteAllText("GameTracer.txt", $"{GameTracer.LevelNumber}");
        }

        private void Button_Exit_MouseEnter(object sender, MouseEventArgs e)
        {
            polygon_exit.Opacity = 1;
        }

        private void Button_Exit_MouseLeave(object sender, MouseEventArgs e)
        {
            polygon_exit.Opacity = 0
                ;
        }

        private void Button_savegame_MouseEnter(object sender, MouseEventArgs e)
        {
            polygon_Save.Opacity = 1
                ;
        }

        private void Button_savegame_MouseLeave(object sender, MouseEventArgs e)
        {
            polygon_Save.Opacity = 0
                ;
        }

        private void Button_continue_MouseEnter(object sender, MouseEventArgs e)
        {
            polygon_Continue.Opacity = 1;
        }

        private void Button_continue_MouseLeave(object sender, MouseEventArgs e)
        {
            polygon_Continue.Opacity = 0;
        }
    }
}
