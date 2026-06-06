using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace _5_Jahre_Hoelle.classes
{
    public static class statistics
    {
        private static int _attemps;
        private static int _wins;
        private static int _achievment_numer;
        

        public static int Attemps
        {
            get
            {
                return _attemps;
            }
            set
            {
                _attemps = value;
                Save();
            }
        }

        public static int Wins
        {
            get
            {
                return _wins;
            }
            set
            {
                _wins = value;
                Save();
            }
        }

        public static int Achievments
        {
            get
            {
                return _achievment_numer;
            }
            set
            {
                _achievment_numer = value;
                Save();
            }
        }

        public static void init()
        {
            Load();
        }

        private static void CreateDefaultSaveFile()
        {
           
            Dictionary<string, int> data_nix = new Dictionary<string, int>();

            data_nix.Add("Attemps", 0);
            data_nix.Add("Wins", 0);
            data_nix.Add("Achievments", 0);

            string json_nix = JsonSerializer.Serialize(data_nix);

            File.WriteAllText("statistics.json", json_nix);
        }

        private static void Save()
        {
            
            Dictionary<string, int> data = new Dictionary<string, int>();

            data.Add("Attemps", _attemps);
            data.Add("Wins", _wins);
            data.Add("Achievments", _achievment_numer);

            string json = JsonSerializer.Serialize(data);

            File.WriteAllText("statistics.json", json);
        }

        private static void Load()
        {
            
            if (!File.Exists("statistics.json"))
            {
                MessageBox.Show("File gibt es nicht");

                CreateDefaultSaveFile();

                _attemps = 0;
                _wins = 0;
                _achievment_numer = 0;

                return;
            }

            string json = File.ReadAllText("statistics.json");

            if (json == "")
            {
                MessageBox.Show("File ist leer");

                CreateDefaultSaveFile();

                _attemps = 0;
                _wins = 0;
                _achievment_numer = 0;

                return;
            }

            Dictionary<string, int> data = JsonSerializer.Deserialize<Dictionary<string, int>>(json);

            if (data == null)
            {
                MessageBox.Show("File ist kaputt");

                CreateDefaultSaveFile();

                _attemps = 0;
                _wins = 0;
                _achievment_numer = 0;

                return;
            }

            if (data.ContainsKey("Attemps"))
            {
                _attemps = data["Attemps"];
            }
            else
            {
                _attemps = 0;
            }

            if (data.ContainsKey("Wins"))
            {
                _wins = data["Wins"];
            }
            else
            {
                _wins = 0;
            }

            if (data.ContainsKey("Achievments"))
            {
                _achievment_numer = data["Achievments"];
            }
            else
            {
                _achievment_numer = 0;
            }
        }
    }
}