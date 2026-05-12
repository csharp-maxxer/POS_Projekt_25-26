using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _5_Jahre_Hoelle.classes
{
    public class Room
    {
        // 14x7 mit je 120px
        private List<List<char>> room_matrix {  get; set; }
        //public List<enemy> { get; private set; }
        public bool IsCleared { get; private set; }
        private static Random random;

        public Room()
        {
            IsCleared = false;
            room_matrix = new List<List<char>>();
            random = new Random();
            ChooseRandomRoom();   
        }

        public void ChooseRandomRoom()
        {
            
            int roomNumber = random.Next(1, 21);

            string filepath = $"../../../rooms/room{roomNumber}.txt";

            string[] room_loaded = File.ReadAllLines(filepath);

            foreach (string room in room_loaded)
            {
                string[] row = room.Split(",");
                List<char> row_splitted = new List<char>();

                foreach (string c in row)
                {
                    char char_ = Convert.ToChar(c);
                    //MessageBox.Show(c);
                    row_splitted.Add(char_);
                }

                room_matrix.Add(row_splitted);
            }
        }

        public void DrawRoom()
        {

        }

        public void SpawnEnemies()
        {

        }
    }
}
