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
                Logger.logger.Debug("attemps got changed");
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
                Logger.logger.Debug("wins got changed");
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
                Logger.logger.Debug("achievments got changed");
                Save();
            }
        }

        public static void init()
        {
            Logger.logger.Debug("statistics class is getting initialized");
            Load();
        }

        private static void CreateDefaultSaveFile()
        {
            Logger.logger.Debug("default statistics file gets created");

            Dictionary<string, int> data_nix = new Dictionary<string, int>();

            data_nix.Add("Attemps", 0);
            data_nix.Add("Wins", 0);
            data_nix.Add("Achievments", 0);

            string json_nix = JsonSerializer.Serialize(data_nix);

            File.WriteAllText("statistics.json", json_nix);
            Logger.logger.Debug("default statistics file got created");
        }

        private static void Save()
        {
            Logger.logger.Debug("statistics are getting saved");

            Dictionary<string, int> data = new Dictionary<string, int>();

            data.Add("Attemps", _attemps);
            data.Add("Wins", _wins);
            data.Add("Achievments", _achievment_numer);

            string json = JsonSerializer.Serialize(data);

            File.WriteAllText("statistics.json", json);
            Logger.logger.Debug("statistics got saved");
        }

        private static void Load()
        {
            Logger.logger.Debug("statistics are getting loaded");

            if (!File.Exists("statistics.json"))
            {
                Logger.logger.Debug("statistics file does not exist");
                //MessageBox.Show("File gibt es nicht");

                CreateDefaultSaveFile();

                _attemps = 0;
                _wins = 0;
                _achievment_numer = 0;

                Logger.logger.Debug("statistics are set to 0");

                return;
            }

            string json = File.ReadAllText("statistics.json");
            Logger.logger.Debug("statistics file got readed");

            if (json == "")
            {
                Logger.logger.Warning("statistics file is empty");
                MessageBox.Show("File ist leer");

                CreateDefaultSaveFile();

                _attemps = 0;
                _wins = 0;
                _achievment_numer = 0;

                Logger.logger.Debug("statistics are set to 0");

                return;
            }

            Dictionary<string, int> data = JsonSerializer.Deserialize<Dictionary<string, int>>(json);
            Logger.logger.Debug("statistics file got deserialized");

            if (data == null)
            {
                Logger.logger.Warning("statistics file is broken");
                MessageBox.Show("File ist kaputt");

                CreateDefaultSaveFile();

                _attemps = 0;
                _wins = 0;
                _achievment_numer = 0;

                Logger.logger.Debug("statistics are set to 0");

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

            Logger.logger.Debug("statistics got loaded");
        }
    }
}