using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;


namespace _5_Jahre_Hoelle.classes
{
    public class Room
    {
        public int Xpos { get; private set; }
        public int Ypos { get; private set; }
        // 14x7 mit je 120px
        private List<List<char>> room_matrix {  get; set; }
        //public List<enemy> { get; private set; }
        public bool IsCleared { get; private set; }

        public Canvas RoomCanvas { get; private set; }
        public char Type { get; private set; }

        private static Random random = new Random();

        public Room(int ypos, int xpos, char type)
        {

            Xpos = xpos;
            Ypos = ypos;
            Type = type;
            IsCleared = false;

            RoomCanvas = new Canvas();

            RoomCanvas.Width = 1680;
            RoomCanvas.Height = 840;

            room_matrix = new List<List<char>>();
            
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
            RoomCanvas.Children.Clear();

            for (int y = 0; y < room_matrix.Count; y++)
            {
                for (int x = 0; x < room_matrix[y].Count; x++)
                {
                    char currentChar = room_matrix[y][x];

                    if (currentChar == 'D')
                    {
                        Rectangle rect = new Rectangle();

                        rect.Width = 120;
                        rect.Height = 120;

                        rect.Fill = Brushes.Red;

                        Canvas.SetLeft(rect, x * 120);
                        Canvas.SetTop(rect, y * 120);

                        RoomCanvas.Children.Add(rect);
                    }
                }
            }
        }

        public void SpawnEnemies()
        {

        }
    }
}
