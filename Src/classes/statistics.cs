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

        private static string filePath = "saves/statistics.json";

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

        private static void Save()
        {
            Dictionary<string, int> data = new Dictionary<string, int>();

            data.Add("Attemps", _attemps);
            data.Add("Wins", _wins);
            data.Add("Achievments", _achievment_numer);

            string json = JsonSerializer.Serialize(data);

            File.WriteAllText(filePath, json);
        }

        private static void Load()
        {
            if (!File.Exists(filePath))
            {
                MessageBox.Show("File Gibt es nicht");
                File.Create(filePath);
                
                Dictionary<string, int> data_NULL = new Dictionary<string, int>();

                data_NULL.Add("Attemps", 0);
                data_NULL.Add("Wins", 0);
                data_NULL.Add("Achievments", 0);

                string json_NULL = JsonSerializer.Serialize(data_NULL);
                File.WriteAllText(filePath, json_NULL);
                return;
            }

            string json;

            json = File.ReadAllText(filePath);

            Dictionary<string, int> data = JsonSerializer.Deserialize<Dictionary<string, int>>(json);

            if (data == null)
            {
                MessageBox.Show("File ist leer");
                File.Create(filePath);

                Dictionary<string, int> data_NULL = new Dictionary<string, int>();

                data_NULL.Add("Attemps", 0);
                data_NULL.Add("Wins", 0);
                data_NULL.Add("Achievments", 0);

                string json_NULL = JsonSerializer.Serialize(data_NULL);
                File.WriteAllText(filePath, json_NULL);
                return;
            }

            if (data.ContainsKey("Attemps"))
            {
                _attemps = data["Attemps"];
            }

            if (data.ContainsKey("Wins"))
            {
                _wins = data["Wins"];
            }

            if (data.ContainsKey("Achievments"))
            {
                _achievment_numer = data["Achievments"];
            }
        }
    }
}