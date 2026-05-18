using _5_Jahre_Hoelle.classes;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace _5_Jahre_Hoelle.pages
{
    /// <summary>
    /// Interaktionslogik für Game.xaml
    /// </summary>
    public partial class Game : Page
    {
        private Room currentRoom;
        private bool isTransitioning = false;
        private bool moveLeft;
        private bool moveRight;
        private bool moveUp;
        private bool moveDown;
        private DispatcherTimer gameTimer;
        private Player player;
        private int upframcount;
        private int downframecount;
        private int rightframcount;
        private int leftframcount;
        public Game()
        {
            InitializeComponent();
            Focus();

            player = new Player(1.0, 5.0, 1.0, 1.0, 1, 1.0);

            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(16);
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
            // Testing
            List<List<char>> matrix_rooms = new List<List<char>>();
            matrix_rooms = CreateMapMatrix(10, 15);
            Dictionary<(int y, int x), Room> cllted_rooms = Collect_Rooms(matrix_rooms);
            assing_doors_to_rooms(cllted_rooms);


            // code
            currentRoom = cllted_rooms[(5, 5)];
            currentRoom.DrawRoom();
            CanvasGame.Children.Add(currentRoom.RoomCanvas);


            // TESTING DELETE ME
            //this.KeyDown += Game_KeyDown_DELETE_ME;
            this.Focusable = true;
            this.Focus();

            upframcount = 0;
            downframecount = 0;
            rightframcount = 0;
            leftframcount = 0;
        }

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            move();
            DrawGame();
        }

        private async void move()
        {
            Vector direction = new Vector(0, 0);

            if (moveLeft)
            {
                //if (!isTransitioning)
                //{
                //    var nextRoom = new Room(
                //            currentRoom.Ypos + 1,
                //            currentRoom.Xpos,
                //            'X');
                //    nextRoom.DrawRoom();
                //    await SwitchRoom(currentRoom, nextRoom);
                //}

           
            }
                

                direction.X -= 1;
                leftframcount++;
                //chatgpt Anfang: wie kann ich im xmal.cs in C# in wpf ein Rectangle mit einem bild fillen
                Rect_Player.Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(
                    new Uri($"pack://application:,,,/assets/links_{leftframcount}.png")
    ),
                    Stretch = Stretch.Uniform
                };
                //chatgpt Ende
                if (leftframcount == 2)
                    leftframcount = 0;
            }
            if (moveRight)
            {
                direction.X += 1;
                rightframcount++;
                //chatgpt Anfang: wie kann ich im xmal.cs in C# in wpf ein Rectangle mit einem bild fillen
                Rect_Player.Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(
                    new Uri($"pack://application:,,,/assets/rechts_{rightframcount}.png")
    ),
                    Stretch = Stretch.Uniform
                };
                //chatgpt Ende
                if (rightframcount == 2)
                    rightframcount = 0;
            }


            if (moveUp)
            {
                direction.Y -= 1;
                upframcount++;
                //chatgpt Anfang: wie kann ich im xmal.cs in C# in wpf ein Rectangle mit einem bild fillen
                Rect_Player.Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(
                    new Uri($"pack://application:,,,/assets/Player_up_{upframcount}.png")
    ),
                    Stretch = Stretch.Uniform
                };
                //chatgpt Ende
                if (upframcount == 2) 
                    upframcount = 0;
            }


            if (moveDown)
            {
                direction.Y += 1;
                downframecount++;
                //chatgpt Anfang: wie kann ich im xmal.cs in C# in wpf ein Rectangle mit einem bild fillen
                Rect_Player.Fill = new ImageBrush
                {
                    ImageSource = new BitmapImage(
                    new Uri($"pack://application:,,,/assets/front_{downframecount}.png")
    ),
                    Stretch = Stretch.Uniform
                };
                //chatgpt Ende
                if (downframecount == 2)
                    downframecount = 0;
            }

            if (direction.Length > 0)
            {
                // chatgpt anfang: wie mach ich das die diagonales laufen gleich schnell ist
                direction.Normalize();
                // chatgpt ende: wie mach ich das die diagonales laufen gleich schnell ist

                player.X_Pos += direction.X * player.Speed;
                player.Y_Pos += direction.Y * player.Speed;
            }
        }

        private void DrawGame()
        {
            Canvas.SetLeft(Rect_Player, player.X_Pos);
            Canvas.SetTop(Rect_Player, player.Y_Pos);

            // Testing
          
            
        }

        //private async void Game_KeyDown_DELETE_ME(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.D)
        //    {

        //        var nextRoom = new Room(
        //        currentRoom.Ypos + 1,
        //        currentRoom.Xpos,
        //        'X');
        //        nextRoom.DrawRoom();
        //        await SwitchRoom(currentRoom, nextRoom);
        //    }
        //}

        public void assing_doors_to_rooms(Dictionary<(int, int), Room> clltd_rooms)
        {
            // welcher raum hat welche türen?
            foreach (Room room in clltd_rooms.Values)
            {
                int x = room.Xpos;
                int y = room.Ypos;

                if (clltd_rooms.ContainsKey((y - 1, x)))
                {
                    room.DoorTop = true;
                }

                if (clltd_rooms.ContainsKey((y, x + 1)))
                {
                    room.DoorRight = true;
                }

                if (clltd_rooms.ContainsKey((y + 1, x)))
                {
                    room.DoorBottom = true;
                }

                if (clltd_rooms.ContainsKey((y, x - 1)))
                {
                    room.DoorLeft = true;
                }
            }
        }

        public Dictionary<(int y, int x), Room> Collect_Rooms (List<List<char>> rooms)
        {
            // adds all rooms into a dict<(int x, int y), Room>
            Dictionary<(int y, int x), Room> dict_rooms = new Dictionary<(int, int), Room>(); 

            for (int i = 0; i < rooms.Count; i++) 
            {
                for (int j = 0; j < rooms[i].Count; j++)
                {
                    if (rooms[i][j] == '-')
                    {
                        continue;
                    }
                    else if (rooms[i][j] ==  'X')
                    {
                        dict_rooms.Add((i, j), new Room(i, j, 'X'));
                    }
                    else if (rooms[i][j] == 'O')
                    {
                        dict_rooms.Add((i, j), new Room(i, j, 'O'));
                    }
                    else if (rooms[i][j] == 'B')
                    {
                        dict_rooms.Add((i, j), new Room(i, j, 'B'));
                    }
                    else if (rooms[i][j] == 'S')
                    {
                        dict_rooms.Add((i, j), new Room(i, j, 'S'));
                    }
                }
            }
            return dict_rooms;
        }


        private static List<List<char>> CreateMapMatrix(int size_matrix, int anzahl_rooms)
        {
            if (anzahl_rooms > size_matrix * size_matrix)
                throw new Exception("zuviele Räume, es kann keine Sackgassen geben weil die ganze map voll ist..");

            int size = size_matrix;

            Dictionary<int, string> directions = new Dictionary<int, string>();
            directions.Add(0, "North");
            directions.Add(1, "East");
            directions.Add(2, "South");
            directions.Add(3, "West");

            List<List<char>> feld = new List<List<char>>(); // mitte 5,5

            for (int i = 0; i < size; i++)
            {
                List<char> zwsch_speicher = new List<char>();
                for (int j = 0; j < size; j++)
                {
                    zwsch_speicher.Add('-');
                }
                feld.Add(zwsch_speicher);
            }

            // mitte adden
            int startX = size / 2;
            int startY = size / 2;

            feld[startY][startX] = 'O';

            Random rand = new Random();

            // KI: ChatGPT
            // Promt: kannst du es vllt so machen dass die räume nicht so in clustern spawnen sondern eher so wie eben in isaac in einer schöneren struktur weißt du?
            // KI-Start

            static int CountNeighbors(List<List<char>> feld, int x, int y)
            {
                int count = 0;

                int[,] dirs = { { 0, -1 }, { 1, 0 }, { 0, 1 }, { -1, 0 } };

                for (int i = 0; i < 4; i++)
                {
                    int nx = x + dirs[i, 0];
                    int ny = y + dirs[i, 1];

                    if (nx >= 0 && nx < feld.Count && ny >= 0 && ny < feld.Count)
                    {
                        if (feld[ny][nx] != '-')
                            count++;
                    }
                }

                return count;
            }

            List<(int x, int y)> rooms = new List<(int, int)>();
            List<(int x, int y)> frontier = new List<(int, int)>(); // WICHTIG!
            List<(int x, int y)> deadEnds = new List<(int, int)>();

            rooms.Add((startX, startY));
            frontier.Add((startX, startY));

            int maxRooms = anzahl_rooms;

            while (rooms.Count < maxRooms && frontier.Count > 0)
            {


                foreach ((int x, int y) room in rooms)
                {
                    int n = CountNeighbors(feld, room.x, room.y);

                    if (n == 1) // Sackgasse
                        deadEnds.Add(room);
                }

                // 👉 Bevorzuge neue Räume (letzte = natürlicher Pfad)
                (int x, int y) current = frontier[rand.Next(Math.Min(3, frontier.Count))];

                List<(int dx, int dy)> dirs = new List<(int, int)>()
                {
                    (0,-1),(1,0),(0,1),(-1,0)
                };

                // mischen
                dirs.Sort((a, b) => rand.Next(-1, 2));

                foreach ((int dx, int dy) d in dirs)
                {
                    int newX = current.x + d.dx;
                    int newY = current.y + d.dy;

                    if (newX < 0 || newX >= size || newY < 0 || newY >= size)
                        continue;

                    if (feld[newY][newX] != '-')
                        continue;

                    // 👉 Nachbarn zählen (ANTI-CLUSTER!)
                    int neighbors = CountNeighbors(feld, newX, newY);

                    if (neighbors > 1)
                        continue;

                    feld[newY][newX] = 'X';
                    rooms.Add((newX, newY));
                    frontier.Add((newX, newY));

                    break; // nur EIN Raum pro Schritt
                }

                // 👉 manchmal entfernen → erzeugt Sackgassen
                if (rand.NextDouble() < 0.3)
                    frontier.Remove(current);
            }

            //KI-Ende

            List<(int, int)> possible_rooms = new List<(int, int)>();

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (feld[i][j] == 'X')
                    {
                        if (CountNeighbors(feld, j, i) == 1)
                        {
                            possible_rooms.Add((j, i));
                        }
                    }
                }
            }

            int random_int = rand.Next(possible_rooms.Count);
            (int shop_x, int shop_y) shop = possible_rooms[random_int];
            possible_rooms.Remove(shop);
            feld[shop.shop_y][shop.shop_x] = 'S';

            random_int = rand.Next(possible_rooms.Count);
            (int boss_room_x, int boss_room_y) boss_room = possible_rooms[random_int];
            possible_rooms.Remove(boss_room);
            feld[boss_room.boss_room_y][boss_room.boss_room_x] = 'B';
                        
            return feld;
        }

        // KI: Chatgpt
        // Prompt: Kannst du mir bitte eine Animation zwischen den Räumen machen, halt wenn man wechselt dass es so eine ist wie in the biinding of isaac
        // KI Start
        public async Task SwitchRoom(Room room_from, Room room_to)
        {
            if (isTransitioning) return;
            isTransitioning = true;

            room_to.DrawRoom();

            CanvasGame.Children.Add(room_to.RoomCanvas);
            Panel.SetZIndex(room_to.RoomCanvas, 1);
            Panel.SetZIndex(room_from.RoomCanvas, 0);

            double width = 1680;
            double height = 840;

            TranslateTransform oldTransform = new TranslateTransform();
            TranslateTransform newTransform = new TranslateTransform();

            room_from.RoomCanvas.RenderTransform = oldTransform;
            room_to.RoomCanvas.RenderTransform = newTransform;

            Duration duration = new Duration(TimeSpan.FromMilliseconds(300));

            double oldToX = 0;
            double oldToY = 0;

            double newFromX = 0;
            double newFromY = 0;

            // RECHTS
            if (room_to.Xpos > room_from.Xpos)
            {
                newFromX = width;
                oldToX = -width;
            }

            // LINKS
            else if (room_to.Xpos < room_from.Xpos)
            {
                newFromX = -width;
                oldToX = width;
            }

            // UNTEN
            else if (room_to.Ypos > room_from.Ypos)
            {
                newFromY = height;
                oldToY = -height;
            }

            // OBEN
            else if (room_to.Ypos < room_from.Ypos)
            {
                newFromY = -height;
                oldToY = height;
            }

            DoubleAnimation oldAnimX = new DoubleAnimation(0, oldToX, duration);

            DoubleAnimation oldAnimY = new DoubleAnimation(0, oldToY, duration);

            DoubleAnimation newAnimX = new DoubleAnimation(newFromX, 0, duration);

            DoubleAnimation newAnimY = new DoubleAnimation(newFromY, 0, duration);

            oldTransform.BeginAnimation(
                TranslateTransform.XProperty,
                oldAnimX);

            oldTransform.BeginAnimation(
                TranslateTransform.YProperty,
                oldAnimY);

            newTransform.BeginAnimation(
                TranslateTransform.XProperty,
                newAnimX);

            newTransform.BeginAnimation(
                TranslateTransform.YProperty,
                newAnimY);

            await Task.Delay(300);

            CanvasGame.Children.Remove(room_from.RoomCanvas);

            currentRoom = room_to;

            isTransitioning = false;
        }
        // KI ENDE
        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A)
            {
                moveLeft = true;
            }

            if (e.Key == Key.W)
            {
                moveUp = true;
            }

            if (e.Key == Key.D)
            {
                moveRight = true;
            }

            if (e.Key == Key.S)
            {
                moveDown = true;
            }
        }

        private void Page_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A)
            {
                moveLeft = false;
            }

            if (e.Key == Key.W)
            {
                moveUp = false;
            }

            if (e.Key == Key.D)
            {
                moveRight = false;
            }

            if (e.Key == Key.S)
            {
                moveDown = false;
            }
        }
    }
}