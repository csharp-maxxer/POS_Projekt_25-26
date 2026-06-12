using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace _5_Jahre_Hoelle.classes
{
    public static class Acheavments
    {
        private static int _acheavment1;
        private static int _acheavment2;
        private static int _acheavment3;
       

        public static int Acheavment1
        {
            get
            {
                return _acheavment1;
            }
            set
            {
                _acheavment1 = value;
                Save();
            }
        }

        public static int Acheavment2
        {
            get
            {
                return _acheavment2;
            }
            set
            {
                _acheavment2 = value;
                Save();
            }
        }

        public static int Acheavment3
        {
            get
            {
                return _acheavment3;
            }
            set
            {
                _acheavment3 = value;
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

            data_nix.Add("Acheavment1", 0);
            data_nix.Add("Acheavment2", 0);
            data_nix.Add("Acheavment3", 0);

            string json_nix = JsonSerializer.Serialize(data_nix);

            File.WriteAllText("achievments.json", json_nix);
        }

        private static void Save()
        {
            
         
            Dictionary<string, int> data = new Dictionary<string, int>();

            data.Add("Acheavment1", _acheavment1);
            data.Add("Acheavment2", _acheavment2);
            data.Add("Acheavment3", _acheavment3);

            string json = JsonSerializer.Serialize(data);

            File.WriteAllText("achievments.json", json);
        }

        private static void Load()
        {
           
            if (!File.Exists("achievments.json"))
            {
                //MessageBox.Show("File gibt es nicht");

                CreateDefaultSaveFile();

                _acheavment1 = 0;
                _acheavment2 = 0;
                _acheavment3 = 0;

                return;
            }

            string json = File.ReadAllText("achievments.json");

            if (json == "")
            {
                //MessageBox.Show("File ist leer");

                CreateDefaultSaveFile();

                _acheavment1 = 0;
                _acheavment2 = 0;
                _acheavment3 = 0;

                return;
            }

            Dictionary<string, int> data = JsonSerializer.Deserialize<Dictionary<string, int>>(json);

            if (data == null)
            {
                MessageBox.Show("File ist kaputt");

                CreateDefaultSaveFile();

                _acheavment1 = 0;
                _acheavment2 = 0;
                _acheavment3 = 0;

                return;
            }

            if (data.ContainsKey("Acheavment1"))
            {
                _acheavment1 = data["Acheavment1"];
            }
            else
            {
                _acheavment1 = 0;
            }

            if (data.ContainsKey("Acheavment2"))
            {
                _acheavment2 = data["Acheavment2"];
            }
            else
            {
                _acheavment2 = 0;
            }

            if (data.ContainsKey("Acheavment3"))
            {
                _acheavment3 = data["Acheavment3"];
            }
            else
            {
                _acheavment3 = 0;
            }
        }
    }
}
