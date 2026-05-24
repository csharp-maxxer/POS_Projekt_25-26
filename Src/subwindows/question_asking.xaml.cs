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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _5_Jahre_Hoelle.subwindows
{
    /// <summary>
    /// Interaktionslogik für question_asking.xaml
    /// </summary>
    public partial class question_asking : UserControl
    {
        public question_asking(Player player)
        {
            InitializeComponent();

            Tuple<string, Dictionary<string, int>> test = ParseQuestion(choose_question());

            MessageBox.Show(test.Item1);

            foreach (KeyValuePair<string, int> entry in test.Item2)
            {
                string key = entry.Key;
                int val = entry.Value;
                MessageBox.Show($"{key}:{val}");
            }
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
    }
}
