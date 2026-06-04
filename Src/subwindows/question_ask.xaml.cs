using _5_Jahre_Hoelle.classes;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace _5_Jahre_Hoelle.subwindows
{
    /// <summary>
    /// Interaktionslogik für question_ask.xaml
    /// </summary>
    public partial class question_ask : Window
    {
        public bool AwnserCorrect;
        private int antwort1;
        private int antwort2;
        private int antwort3;
        private int antwort4;
        private Player player;
        public double difficulty;
        public question_ask(Player player, double difficulty)
        {
            InitializeComponent();
            this.difficulty = difficulty;

            this.player = player;

            Tuple<string, Dictionary<string, int>> test = ParseQuestion(choose_question());

            Txt_Frage.Text = test.Item1;

            List<KeyValuePair<string, int>> answers =
                test.Item2.ToList();

            // Antworttexte setzen
            Txt_Antwort1.Text = answers[0].Key;
            Txt_Antwort2.Text = answers[1].Key;
            Txt_Antwort3.Text = answers[2].Key;
            Txt_Antwort4.Text = answers[3].Key;

            // Werte speichern
            antwort1 = answers[0].Value;
            antwort2 = answers[1].Value;
            antwort3 = answers[2].Value;
            antwort4 = answers[3].Value;
        }

        // KI Chatgpt
        // Prompt: yo big c kannst du mir einen desearialisieren machen der so deserialisiert wie das angegegebene format
        // KI Anfang
        private static Tuple<string, Dictionary<string, int>> ParseQuestion(string input)
        {
            int splitIndex = input.IndexOf(":{");

            if (splitIndex == -1)
                throw new ArgumentException("Ungültiges Format");

            string frage = input.Substring(0, splitIndex);

            string antwortBlock = input.Substring(splitIndex + 2);
            antwortBlock = antwortBlock.TrimEnd('}');

            Dictionary<string, int> antworten = new Dictionary<string, int>();

            string[] parts = antwortBlock.Split(',');

            for (int i = 0; i < parts.Length; i++)
            {
                string[] kv = parts[i].Split(':');

                if (kv.Length != 2)
                    continue;

                string antwort = kv[0].Trim();
                int wert = int.Parse(kv[1].Trim());

                antworten.Add(antwort, wert);
            }

            return Tuple.Create(frage, antworten);
        }
        // KI Ende

        private string choose_question()
        {
            string[] unformated = File.ReadAllLines("../../../questions/questions.txt");
            Random random = new Random();
            return unformated[random.Next(unformated.Length)];
        }

        private async void Rect_Antwort1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            await CheckAnswer(antwort1, Rect_Antwort1);
        }

        private async void Rect_Antwort2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            await CheckAnswer(antwort2, Rect_Antwort2);
        }

        private async void Rect_Antwort3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            await CheckAnswer(antwort3, Rect_Antwort3);
        }

        private async void Rect_Antwort4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            await CheckAnswer(antwort4, Rect_Antwort4);
        }

        private async Task CheckAnswer(int value, Border clickedBorder)
        {
            IsEnabled = false;

            if (value == 1)
            {
                clickedBorder.Background = Brushes.Green;
                if (player.Grade + 5 * difficulty >= 100)
                {
                    player.Grade = 100;
                }
                else
                {
                    player.Grade += 5 * difficulty;
                }
                AwnserCorrect = true;
            }   
            else
            {
                clickedBorder.Background = Brushes.Red;
                if (player.Grade - 20 /difficulty <= 0)
                {
                    player.Grade = 0;
                }
                else
                {
                    player.Grade -= 20 / difficulty ;
                }
                AwnserCorrect = false;
            }

            await Task.Delay(300);

            Close();
        }
    }
}
