using _5_Jahre_Hoelle.classes;
using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace _5_Jahre_Hoelle.pages
{
    /// <summary>
    /// Interaktionslogik für Game.xaml
    /// </summary>
    public partial class Game : Page
    {
        private bool moveLeft;
        private bool moveRight;
        private bool moveUp;
        private bool moveDown;
        private DispatcherTimer gameTimer;
        private Player player;
        public Game()
        {
            InitializeComponent();

            player = new Player(1.0,1.0,1.0,1.0,1,1.0);
            gameTimer = new DispatcherTimer();
            gameTimer.Interval = TimeSpan.FromMilliseconds(16);
            gameTimer.Tick += GameTimer_Tick;

        }

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            DrawGame();
        }


        private void DrawGame()
        {
            main.Children.Add(Rect_Player);
            Canvas.SetLeft(Rect_Player, player.X_Pos);
            Canvas.SetTop(Rect_Player, player.Y_Pos);
            
        }

        private static List<List<string>> CreateMapMatrix(int size_matrix, int anzahl_rooms)
        {
            if (anzahl_rooms > size_matrix * size_matrix)
                throw new Exception("zuviele Räume, es kann keine Sackgassen geben weil die ganze map voll ist..");

            int size = size_matrix;

            Dictionary<int, string> directions = new Dictionary<int, string>();
            directions.Add(0, "North");
            directions.Add(1, "East");
            directions.Add(2, "South");
            directions.Add(3, "West");

            List<List<string>> feld = new List<List<string>>(); // mitte 5,5

            for (int i = 0; i < size; i++)
            {
                List<string> zwsch_speicher = new List<string>();
                for (int j = 0; j < size; j++)
                {
                    zwsch_speicher.Add("-");
                }
                feld.Add(zwsch_speicher);
            }

            // mitte adden
            int startX = size / 2;
            int startY = size / 2;

            feld[startY][startX] = "O";

            Random rand = new Random();

            
            // KI: ChatGPT
            // Promt: kannst du es vllt so machen dass die räume nicht so in clustern spawnen sondern eher so wie eben in isaac in einer schöneren struktur weißt du?
            // KI-Start

            static int CountNeighbors(List<List<string>> feld, int x, int y)
            {
                int count = 0;

                int[,] dirs = { { 0, -1 }, { 1, 0 }, { 0, 1 }, { -1, 0 } };

                for (int i = 0; i < 4; i++)
                {
                    int nx = x + dirs[i, 0];
                    int ny = y + dirs[i, 1];

                    if (nx >= 0 && nx < feld.Count && ny >= 0 && ny < feld.Count)
                    {
                        if (feld[ny][nx] != "-")
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


                foreach (var room in rooms)
                {
                    int n = CountNeighbors(feld, room.x, room.y);

                    if (n == 1) // Sackgasse
                        deadEnds.Add(room);
                }

                // 👉 Bevorzuge neue Räume (letzte = natürlicher Pfad)
                var current = frontier[rand.Next(Math.Min(3, frontier.Count))];

                List<(int dx, int dy)> dirs = new List<(int, int)>()
                {
                    (0,-1),(1,0),(0,1),(-1,0)
                };

                // mischen
                dirs.Sort((a, b) => rand.Next(-1, 2));

                foreach (var d in dirs)
                {
                    int newX = current.x + d.dx;
                    int newY = current.y + d.dy;

                    if (newX < 0 || newX >= size || newY < 0 || newY >= size)
                        continue;

                    if (feld[newY][newX] != "-")
                        continue;

                    // 👉 Nachbarn zählen (ANTI-CLUSTER!)
                    int neighbors = CountNeighbors(feld, newX, newY);

                    if (neighbors > 1)
                        continue;

                    feld[newY][newX] = "X";
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
                    if (feld[i][j] != "-")
                    {

                        if (CountNeighbors(feld, j, i) == 1)
                        {
                            possible_rooms.Add((j, i));
                        }


                    }
                }
            }

            int random_int = rand.Next(possible_rooms.Count);
            (int tresure_x, int tresure_y) tresure_room = possible_rooms[random_int];
            possible_rooms.Remove(tresure_room);
            feld[tresure_room.tresure_y][tresure_room.tresure_x] = "T";

            random_int = rand.Next(possible_rooms.Count);
            (int boss_room_x, int boss_room_y) boss_room = possible_rooms[random_int];
            possible_rooms.Remove(boss_room);
            feld[boss_room.boss_room_y][boss_room.boss_room_x] = "B";

            // print
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(feld[i][j] + " ");
                }
                Console.WriteLine();
            }

            return feld;
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Page_KeyUp(object sender, KeyEventArgs e)
        {

        }
    }
}
