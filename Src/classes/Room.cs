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
using System.Windows.Media.Imaging;


namespace _5_Jahre_Hoelle.classes
{
    public class Room
    {
        public bool DoorTop { get; set; }
        public bool DoorRight { get; set; }
        public bool DoorBottom { get; set; }
        public bool DoorLeft { get; set; }
        public List<Rect> obstacles {  get; set; }
        public int Xpos { get; private set; }
        public int Ypos { get; private set; }
        // 14x7 mit je 120px
        private List<List<char>> room_matrix { get; set; }
        //public List<enemy> { get; private set; }
        public bool IsCleared { get; private set; }
        public bool ExamRoom { get; private set; }
        public Canvas RoomCanvas { get; private set; }
        public char Type { get; private set; }

        private static Random random = new Random();

        public Room(int ypos, int xpos, char type)
        {
            obstacles = new List<Rect>();
            DoorTop = false;
            DoorRight = false;
            DoorBottom = false;
            DoorLeft = false;

            Xpos = xpos;
            Ypos = ypos;
            Type = type;
            IsCleared = false;
            ExamRoom = false;

            RoomCanvas = new Canvas();

            RoomCanvas.Width = 1680;
            RoomCanvas.Height = 840;

            room_matrix = new List<List<char>>();

            if (Type == 'X')
            {
                Exam_Enemy_Room();
            }
            ChooseRandomRoom();
        }
        private void Exam_Enemy_Room()
        {
            // wählt mit einer 33% chance ob ein raum ein exam room oder ein enemy room ist.
            if (random.Next(3) == 2)
            {
                ExamRoom = true;
            }
        }

        private void ChooseRandomRoom()
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
                    row_splitted.Add(char_);
                }

                room_matrix.Add(row_splitted);
            }
        }

        public void DrawRoom()
        {
            obstacles.Clear();
            RoomCanvas.Children.Clear();

            Image background = new Image();

            background.Width = 1920;
            background.Height = 1000;

            // Source: Maximilian Schreiter
            // Prompt: Wie kann ich ein bild im code adden?
            // Max Anfang   
            background.Source = new BitmapImage(new Uri("../assets/background_vorlaeufig.png", UriKind.Relative));
            // Max Ende

            Canvas.SetLeft(background, -120);
            Canvas.SetTop(background, -80);

            RoomCanvas.Children.Add(background);
            
            PlaceObjects();
            PlaceDoors();
        }

        public void SpawnEnemies()
        {

        }

        private void PlaceObjects()
        {
            if (this.Type == 'O' || this.Type == 'X')
            {
                Dictionary<int, (string, int, int)> graphics = new Dictionary<int, (string, int, int)>();

                graphics.Add(0, ("chair_downwards.png", 45, 84));
                graphics.Add(1, ("chair_left.png", 56, 62));
                graphics.Add(2, ("chair_right.png", 57, 62));
                graphics.Add(3, ("chair_upwards.png", 49, 61));
                graphics.Add(4, ("desk_dave1.png", 120, 95));
                graphics.Add(5, ("desk_dave2.png", 120, 120));
                graphics.Add(6, ("desk_erik1.png", 120, 95));
                graphics.Add(7, ("desk_erik2.png", 120, 120));
                graphics.Add(8, ("desk_vale1.png", 120, 95));
                graphics.Add(9, ("desk_vale2.png", 120, 120));

                for (int y = 0; y < room_matrix.Count; y++)
                {
                    for (int x = 0; x < room_matrix[y].Count; x++)
                    {
                        if (room_matrix[y][x] == 'D')
                        {
                            int random_grafic = random.Next(graphics.Count);
                            (string, int, int) selected_grafic = graphics[random_grafic];

                            Image image = new Image();
                            image.Width = selected_grafic.Item2;
                            image.Height = selected_grafic.Item3;
                            image.Source = new BitmapImage(new Uri($"../assets/{selected_grafic.Item1}", UriKind.Relative));

                            double mittelpunkt_x = 120 * x + 60;
                            double mittelpunkt_y = 120 * y + 60;
                            double draw_at_x = mittelpunkt_x - (selected_grafic.Item2 / 2);
                            double draw_at_y = mittelpunkt_y - (selected_grafic.Item3 / 2);
                            Rect obstacle = new Rect();
                            obstacle.Width = 120;
                            obstacle.Height = 120;
                            obstacle.X = mittelpunkt_x -(selected_grafic.Item2 / 2);
                            obstacle.Y = mittelpunkt_y - (selected_grafic.Item3 / 2);
                           
                            

                            Canvas.SetLeft(image, draw_at_x);
                            Canvas.SetTop(image, draw_at_y);
                            obstacles.Add(obstacle);
                          
                            RoomCanvas.Children.Add(image);
                        }
                    }
                }

                if (ExamRoom)
                {
                    Image book_img = new Image();
                    book_img.Width = 102;
                    book_img.Height = 76;
                    book_img.Source = new BitmapImage(new Uri("../assets/book.png", UriKind.Relative));
                    Canvas.SetLeft(book_img, 790);
                    Canvas.SetTop(book_img, 382);
                    RoomCanvas.Children.Add(book_img);
                }
            }
            if (this.Type == 'S')
            {
                Image booth = new Image();
                booth.Width = 138;
                booth.Height = 202;
                booth.Source = new BitmapImage(new Uri($"../assets/shop_booth.png", UriKind.Relative));

                Canvas.SetLeft(booth, 771);
                Canvas.SetTop(booth, 319);

                RoomCanvas.Children.Add(booth);
            }
        }
            

        private void PlaceDoors()
        {
            if (DoorTop)
            {
                Image door = new Image();

                door.Width = 205;
                door.Height = 80;


                door.Source = new BitmapImage(new Uri("../assets/door_up.png", UriKind.Relative));

                Canvas.SetLeft(door, 738);
                Canvas.SetTop(door, -80);

                RoomCanvas.Children.Add(door);
            }
            if (DoorBottom)
            {
                Image door = new Image();

                door.Width = 206;
                door.Height = 80;


                door.Source = new BitmapImage(new Uri("../assets/door_down.png", UriKind.Relative));

                Canvas.SetLeft(door, 738);
                Canvas.SetTop(door, 840);

                RoomCanvas.Children.Add(door);
            }
            if (DoorLeft)
            {
                Image door = new Image();

                door.Width = 120;
                door.Height = 209;


                door.Source = new BitmapImage(new Uri("../assets/door_left.png", UriKind.Relative));

                Canvas.SetLeft(door, -120);
                Canvas.SetTop(door, 316);

                RoomCanvas.Children.Add(door);
            }
            if (DoorRight)
            {
                Image door = new Image();

                door.Width = 120;
                door.Height = 209;


                door.Source = new BitmapImage(new Uri("../assets/door_right.png", UriKind.Relative));

                Canvas.SetLeft(door, 1680);
                Canvas.SetTop(door, 316);

                RoomCanvas.Children.Add(door); 
            }
        }
    }
}
