using _5_Jahre_Hoelle.pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace _5_Jahre_Hoelle.classes
{
    public class Room
    {
        [JsonIgnore]
        public Rect ExamBookRect { get; private set; }
        public bool ExamTriggered { get; set; } = false;
        public bool DoorTop { get; set; }
        public bool DoorRight { get; set; }
        public bool DoorBottom { get; set; }
        public bool DoorLeft { get; set; }
        [JsonIgnore]
        public List<Rect> obstacles { get; set; }
        public int Xpos { get; private set; }
        public int Ypos { get; private set; }
        // 14x7 mit je 120px
        public List<List<char>> room_matrix { get; set; }
        //public List<enemy> { get; private set; }
        public bool IsCleared { get; set; }
        public bool ExamRoom { get; set; }
        // Chatgpt: wie kann ich machen dass manche sachen nicht gespeichert werden?
        [JsonIgnore]
        // gpt ende.
        public Canvas RoomCanvas { get; private set; }
        public char Type { get; private set; }

        private static Random random = new Random();
        public List<Enemy> Enemies { get; set; } = new List<Enemy>();


        public Room(int ypos, int xpos, char type)
        {
            Logger.logger.Debug("room gets created");

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
            if (Type == 'O' || Type == 'S')
            {
                IsCleared = true;
                Logger.logger.Debug("room is cleared becuase its start or shop");
            }
            if (Type == 'X')
            {
                Exam_Enemy_Room();
            }
            ChooseRandomRoom();

            Logger.logger.Debug("room got created");
        }
        private void Exam_Enemy_Room()
        {
            // wählt mit einer 33% chance ob ein raum ein exam room oder ein enemy room ist.
            if (random.Next(3) == 2)
            {
                ExamRoom = true;
                Logger.logger.Debug("room got choosen as exam room");
            }
            else
            {
                Logger.logger.Debug("room got choosen as enemy room");
            }
        }

        private void ChooseRandomRoom()
        {
            Logger.logger.Debug("random room gets choosen");

            int roomNumber = random.Next(1, 21);

            string filepath = $"../../../rooms/room{roomNumber}.txt";

            string[] room_loaded = File.ReadAllLines(filepath);
            Logger.logger.Debug("room file got readed");

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

            Logger.logger.Debug("room matrix got loaded");
        }

        public void DrawRoom()
        {
            Logger.logger.Debug("room gets drawed");

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

            Logger.logger.Debug("room got drawed");
        }

        public void SpawnEnemies()
        {
            if (Type == 'X' && !ExamRoom)
            {
                Logger.logger.Debug("enemys are getting spawned");

                Random rand = new Random();
                int count = rand.Next(1, 4);

                for (int i = 0; i < count; i++)
                {
                    Enemy enemy = new Enemy(
                        damage: 4,
                        speed: 110,
                        range: 500,
                        firerate: 2.0,
                        health: 3
                    );

                    enemy.X_Pos = rand.Next(200, 1400);
                    enemy.Y_Pos = rand.Next(200, 600);

                    Enemies.Add(enemy);
                }

                Logger.logger.Debug("enemys got spawned");
            }
        }

        private void PlaceObjects()
        {
            Logger.logger.Debug("room objects are getting placed");

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
                            Rect obstacle = new Rect(
                                draw_at_x,
                                draw_at_y,
                                selected_grafic.Item2,
                                selected_grafic.Item3
                            );
                            //obstacle.Width = 120;
                            //obstacle.Height = 120;
                            //obstacle.X = mittelpunkt_x -(selected_grafic.Item2 / 2);
                            //obstacle.Y = mittelpunkt_y - (selected_grafic.Item3 / 2);

                            Canvas.SetLeft(image, draw_at_x);
                            Canvas.SetTop(image, draw_at_y);
                            obstacles.Add(obstacle);
                            RoomCanvas.Children.Add(image);


                            if (Game.ShowHitboxes)
                            {
                                Rectangle hitbox = new Rectangle();

                                hitbox.Width = obstacle.Width;
                                hitbox.Height = obstacle.Height;

                                hitbox.Stroke = Brushes.Red;
                                hitbox.StrokeThickness = 2;
                                hitbox.Fill = Brushes.Transparent;

                                Canvas.SetLeft(hitbox, obstacle.X);
                                Canvas.SetTop(hitbox, obstacle.Y);

                                RoomCanvas.Children.Add(hitbox);
                            }
                        }
                    }
                }

                if (ExamRoom)
                {
                    Image book_img = new Image();
                    book_img.Width = 102;
                    book_img.Height = 76;
                    book_img.Source = new BitmapImage(new Uri("../assets/book.png", UriKind.Relative));

                    double x = 790;
                    double y = 382;

                    Canvas.SetLeft(book_img, x);
                    Canvas.SetTop(book_img, y);

                    ExamBookRect = new Rect(x, y, book_img.Width, book_img.Height);
                    RoomCanvas.Children.Add(book_img);

                    Logger.logger.Debug("exam book got placed");
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

                Logger.logger.Debug("shop got placed");
            }

            Logger.logger.Debug("room objects got placed");
        }


        private void PlaceDoors()
        {
            Logger.logger.Debug("doors are getting placed");

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

            Logger.logger.Debug("doors got placed");
        }

        public string Serialize()
        {
            Logger.logger.Debug("room gets serialized");

            // chatgpt: wie kann ich machen dass es rekursiv in klassen serialisiert?
            return JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = true
            });
        }
    }
}