using _5_Jahre_Hoelle.classes;
using _5_Jahre_Hoelle.subwindows;
using _5_Jahre_Hoelle.usercontrols;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
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
        private Room currentRoom;
        private bool isTransitioning = false;   
        private bool moveLeft;
        private bool moveRight;
        private bool moveUp;
        private bool moveDown;
        private double energyTimer = 4;
        private Player player;
        private bool playerNearShop = false;
        private bool shopOpen = false;
        private double animationTimer = 0;
        private bool examOpen = false;
        private bool gamePaused = false;
        private List<Rectangle> bulletRects = new List<Rectangle>();
        bool shootup;
        bool shootright;
        bool shootleft;
        bool shootdown;
        double difficulty;
        double bulletRotationTimer = 0;
        private double shootTimer = 0;
        private usercontrols.Pausescreen pauseScreen;
        private List<Rectangle> enemyRects = new List<Rectangle>();
        private List<Rectangle> bossRects = new List<Rectangle>();
        private Boss currentBoss = null;


        //chatgpt anfang: wie mache ich hier einen Deltatimer
        private DateTime lastFrameTime;
        private double deltaTime;
        //chatgpt ende
        private int animationTick = 0;
        private int animationFrame = 0;
        //chatgpt Anfang: wie kann ich im xmal.cs in C# in wpf ein Rectangle mit einem bild fillen



        private ImageBrush links1 = new ImageBrush
        {
            ImageSource = new BitmapImage(
                    new Uri($"pack://application:,,,/assets/links_1.png")
    ),
            Stretch = Stretch.Uniform
        };
        //chatgpt Ende

        private ImageBrush links2 = new ImageBrush
        {
            ImageSource = new BitmapImage(
                new Uri($"pack://application:,,,/assets/links_2.png")
),
            Stretch = Stretch.Uniform
        };



        private ImageBrush rechts1 = new ImageBrush
        {
            ImageSource = new BitmapImage(
                    new Uri($"pack://application:,,,/assets/rechts_1.png")
    ),
            Stretch = Stretch.Uniform
        };

        private ImageBrush rechts2 = new ImageBrush
        {
            ImageSource = new BitmapImage(
                new Uri($"pack://application:,,,/assets/rechts_2.png")
),
            Stretch = Stretch.Uniform
        };

        private ImageBrush front1 = new ImageBrush
        {
            ImageSource = new BitmapImage(
                    new Uri($"pack://application:,,,/assets/front_1.png")
    ),
            Stretch = Stretch.Uniform
        };

        private ImageBrush front2 = new ImageBrush
        {
            ImageSource = new BitmapImage(
                new Uri($"pack://application:,,,/assets/front_2.png")
),
            Stretch = Stretch.Uniform
        };



        private ImageBrush up1 = new ImageBrush
        {
            ImageSource = new BitmapImage(
                    new Uri($"pack://application:,,,/assets/Player_up_1.png")
    ),
            Stretch = Stretch.Uniform
        };

        private ImageBrush up2 = new ImageBrush
        {
            ImageSource = new BitmapImage(
                new Uri($"pack://application:,,,/assets/Player_up_2.png")
),
            Stretch = Stretch.Uniform
        };

        private ImageBrush bullet1 = new ImageBrush
        {
            ImageSource = new BitmapImage(
        new Uri("pack://application:,,,/assets/ammo_pencil.png")
    ),
            Stretch = Stretch.Uniform
        };

        private ImageBrush bullet2 = new ImageBrush
        {
            ImageSource = new BitmapImage(
                new Uri("pack://application:,,,/assets/ammo_rubber.png") // new Uri ("/assets", uri kind relative)
            ),
            Stretch = Stretch.Uniform
        };

        private ImageBrush enemy_front1 = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("pack://application:,,,/assets/enemy_front_1.png")),
            Stretch = Stretch.Uniform
        };

        private ImageBrush enemy_front2 = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("pack://application:,,,/assets/enemy_front_2.png")),
            Stretch = Stretch.Uniform
        };

        private ImageBrush enemy_back1 = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("pack://application:,,,/assets/enemy_back_1.png")),
            Stretch = Stretch.Uniform
        };

        private ImageBrush enemy_back2 = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("pack://application:,,,/assets/enemy_back_2.png")),
            Stretch = Stretch.Uniform
        };

        private ImageBrush enemy_left1 = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("pack://application:,,,/assets/enemy_left_1.png")),
            Stretch = Stretch.Uniform
        };

        private ImageBrush enemy_left2 = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("pack://application:,,,/assets/enemy_left_2.png")),
            Stretch = Stretch.Uniform
        };

        private ImageBrush enemy_right1 = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("pack://application:,,,/assets/enemy_right_1.png")),
            Stretch = Stretch.Uniform
        };

        private ImageBrush enemy_right2 = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("pack://application:,,,/assets/enemy_right_2.png")),
            Stretch = Stretch.Uniform
        };

        private ImageBrush ammo_coin = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("pack://application:,,,/assets/ammo_coin.png")),
            Stretch = Stretch.Uniform
        };

        private ImageBrush boss_sprite = new ImageBrush
        {
            ImageSource = new BitmapImage(new Uri("pack://application:,,,/assets/boss.png")),
            Stretch = Stretch.Uniform
        };

        Dictionary<(int y, int x), Room> cllted_rooms;

        public Game(double difficulty)
        {
            statistics.init();
            Acheavments.init();
            this.difficulty = difficulty;
            InitializeComponent();
            
            player = new Player(1.0 * difficulty, 250.0 * difficulty, 4.5 * difficulty, 0.5 * difficulty, 70, 100);
            EnergyDisplay.SetEnergy(player.Energy);
            Rect_Player.Fill = front1;
            lastFrameTime = DateTime.Now;
            CompositionTarget.Rendering += GameLoop;

            List<List<char>> matrix_rooms = new List<List<char>>();
            matrix_rooms = CreateMapMatrix(10, 15);
            cllted_rooms = Collect_Rooms(matrix_rooms);
            assing_doors_to_rooms(cllted_rooms);

            currentRoom = cllted_rooms[(5, 5)];
            currentRoom.DrawRoom();
            CanvasGame.Children.Add(currentRoom.RoomCanvas);

            this.Focusable = true;
            // chatgpt anfang: warum kann ich wenn ich schieße nicht pause drücken (code)
            this.Loaded += Game_Loaded;
            this.Unloaded += Game_Unloaded;
            //chatgpt ende
        }
        

        public Game()
        {
            statistics.init();
            this.difficulty = difficulty;
            InitializeComponent();

            player = new Player(1.0 * difficulty, 250.0 * difficulty, 4.5 * difficulty, 0.5 * difficulty, 70, 100);
            EnergyDisplay.SetEnergy(player.Energy);
            Rect_Player.Fill = front1;
            lastFrameTime = DateTime.Now;
            CompositionTarget.Rendering += GameLoop;

            List<List<char>> matrix_rooms = new List<List<char>>();
            matrix_rooms = CreateMapMatrix(10, 15);
            cllted_rooms = Collect_Rooms(matrix_rooms);
            assing_doors_to_rooms(cllted_rooms);

            currentRoom = cllted_rooms[(5, 5)];
            currentRoom.DrawRoom();
            CanvasGame.Children.Add(currentRoom.RoomCanvas);

            this.Focusable = true;
            // chatgpt anfang: warum kann ich wenn ich schieße nicht pause drücken (code)
            this.Loaded += Game_Loaded;
            this.Unloaded += Game_Unloaded;
            //chatgpt ende
            LoadGame();
        }

        // chatgpt anfang: warum kann ich wenn ich schieße nicht pause drücken (code)
        private void Game_Loaded(object sender, RoutedEventArgs e)
        {
            this.Focus();

            Window window = Window.GetWindow(this);

            if (window != null)
            {
                window.PreviewKeyDown += Window_PreviewKeyDown;
            }
        }

        private void Game_Unloaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);

            if (window != null)
            {
                window.PreviewKeyDown -= Window_PreviewKeyDown;
            }
        }
       // chatgpt ende

        private void GameLoop(object? sender, EventArgs e)
        {
            // chatgpt anfang: wie mache ich bei diesem code das die bullets nur 4 mal in der sekunde geschossen werden (ganzer code)
            DateTime now = DateTime.Now;
            deltaTime = (now - lastFrameTime).TotalSeconds;
            lastFrameTime = now;
            //chatgpt ende
            if (deltaTime > 0.05) deltaTime = 0.05;

            if (gamePaused || isTransitioning)
                return;

            energyTimer -= deltaTime;

            if (energyTimer <= 0)
            {
                if (player.Energy > 0)
                {
                    player.Energy -= 1;
                }

                EnergyDisplay.SetEnergy(player.Energy);

                energyTimer = 1;
            }

            if (player.Energy == 0)
            {
                PauseGame();

                this.Content = new Frame
                {
                    Content = new pages.menue_page()
                };

                return;
            }



            //chatgpt andang: fixe den bug das ich wenn ich schießen anfange nicht mehr aufhören kann und ich mich nicht bewegen kann
            moveLeft = Keyboard.IsKeyDown(Key.A);
            moveRight = Keyboard.IsKeyDown(Key.D);
            moveUp = Keyboard.IsKeyDown(Key.W);
            moveDown = Keyboard.IsKeyDown(Key.S);

            shootup = Keyboard.IsKeyDown(Key.Up);
            shootleft = Keyboard.IsKeyDown(Key.Left);
            shootright = Keyboard.IsKeyDown(Key.Right);
            shootdown = Keyboard.IsKeyDown(Key.Down);
            //chatgpt ende
            // chatgpt anfang: wie mache ich bei diesem code das die bullets nur 4 mal in der sekunde geschossen werden (ganzer code)
            shootTimer -= deltaTime;

           
            if (shootTimer <= 0)
            {
                shootrangecheck();

            }

            if (shootup && shootTimer <= 0)
            {
                Bullets bullet = new Bullets(
                    player.Damage,
                    player.Firerate,
                    player.Range,
                    new Vector(0, -10),
                    player.X_Pos + 35,
                    player.Y_Pos + 40
                    
                );

                bullet.X_pos = player.X_Pos + 35;
                bullet.Y_pos = player.Y_Pos + 40;

                player.Attack(bullet);

                shootTimer = player.Firerate;
            }
            // chatgpt ende
            if (shootleft && shootTimer <= 0)
            {
                Bullets bullet = new Bullets(
                    player.Damage,
                    player.Firerate,
                    player.Range,
                    new Vector(-10, 0),
                    player.X_Pos + 35,
                    player.Y_Pos + 40
                );

                bullet.X_pos = player.X_Pos + 35;
                bullet.Y_pos = player.Y_Pos + 40;

                player.Attack(bullet);

                shootTimer = player.Firerate;
            }

            if (shootright && shootTimer <= 0)
            {
                Bullets bullet = new Bullets(
                    player.Damage,
                    player.Firerate,
                    player.Range,
                    new Vector(10, 0),
                    player.X_Pos + 35,
                    player.Y_Pos + 40
                );

                bullet.X_pos = player.X_Pos + 35;
                bullet.Y_pos = player.Y_Pos + 40;

                player.Attack(bullet);

                shootTimer = player.Firerate;
            }

            if (shootdown && shootTimer <= 0)
            {
                Bullets bullet = new Bullets(
                    player.Damage,
                    player.Firerate,
                    player.Range,
                    new Vector(0, 10),
                    player.X_Pos + 35,
                    player.Y_Pos + 40
                );

                bullet.X_pos = player.X_Pos + 35;
                bullet.Y_pos = player.Y_Pos + 40;

                player.Attack(bullet);

                shootTimer = player.Firerate;
            }



            bulletRotationTimer -= deltaTime;

            if (bulletRotationTimer <= 0)
            {
                foreach (Bullets bullet in player.Bullets)
                {
                    bullet.rotation += 45;

                    if (bullet.rotation >= 360)
                    {
                        bullet.rotation = 0;
                    }
                }

                foreach (Enemy enemy in currentRoom.Enemies)
                {
                    foreach (Bullets bullet in enemy.Bullets)
                    {
                        bullet.rotation += 45;

                        if (bullet.rotation >= 360)
                        {
                            bullet.rotation = 0;
                        }
                    }
                }


                bulletRotationTimer = 0.04;
            }

            foreach (Rect obstacle in currentRoom.obstacles)
            {
                player_bullet_collision_obstacle(obstacle);
            }
            move();
            UpdateEnemies();
            UpdateBoss();
            player_bullet_move();
            DrawGame();
        }

        private bool CollidesWithObstacle(double newX, double newY)
        {
            Rect playerHitbox = new Rect(
                newX + 30,
                newY + 50,
                43,
                50
            );

            foreach (Rect obstacle in currentRoom.obstacles)
            {
                //caht gpt Anfang: Wie kann ich checken ob zwei Rect miteinander kollidieren
                if (playerHitbox.IntersectsWith(obstacle))
                // chatgpt ende
                {
                    return true;
                }
            }

            return false;
        }

        
        

        private async void move()
        {
            if (isTransitioning || gamePaused) return;


            Rect palyer_rect = new Rect();
            palyer_rect.X = player.X_Pos;
            palyer_rect.Y = player.Y_Pos;
            palyer_rect.Width = 73;
            palyer_rect.Height = 100;


            Vector direction = new Vector(0, 0);

            if (moveLeft && player.X_Pos  >= 0)
            {
                if (currentRoom.DoorLeft && player.Y_Pos >= 300 && player.Y_Pos <= 450 && player.X_Pos <= 2 && currentRoom.IsCleared)
                {
                    if (!isTransitioning)
                    {
                        Room next_room = cllted_rooms[(currentRoom.Ypos, currentRoom.Xpos - 1)];
                        player.X_Pos = -200;
                        _ = SwitchRoom(currentRoom, next_room); // _ ist wie await nur dass es nicht wartet
                        player.X_Pos = 1620;
                    }
                }
                

                direction.X -= 1;
                // KI: Claude AI
                // Prompt: yo big c wie kann ich die animation frameabhängig machen?
                // KI anfang
                animationTimer += deltaTime;
                if (animationTimer < 0.15)
                {
                    Rect_Player.Fill = links1;
                }
                else if (animationTimer < 0.3)
                {
                    Rect_Player.Fill = links2;
                }
                else
                {
                    animationTimer = 0;
                }
                // KI Ende (block wird weitere 3x verwendet.)
            }


            if (moveRight && player.X_Pos + 73 <= 1680)
            {
                if (currentRoom.DoorRight && player.Y_Pos >= 300 && player.Y_Pos <= 450 && player.X_Pos >= 1580 && currentRoom.IsCleared)
                {
                    if (!isTransitioning)   
                    {
                        Room next_room = cllted_rooms[(currentRoom.Ypos, currentRoom.Xpos + 1)];
                        player.X_Pos = -200;
                        _ = SwitchRoom(currentRoom, next_room);
                        player.X_Pos = 40;
                    }
                }

                direction.X += 1;
                animationTimer += deltaTime;
                if (animationTimer < 0.15)
                {
                    Rect_Player.Fill = rechts1;
                }
                else if (animationTimer < 0.3)
                {
                    Rect_Player.Fill = rechts2;
                }
                else
                {
                    animationTimer = 0;
                }
            }


            if (moveUp && player.Y_Pos >= -10)
            {
                
                if (currentRoom.DoorTop && player.Y_Pos <= 15 && player.X_Pos >= 760 && player.X_Pos <= 900 && currentRoom.IsCleared)
                {
                    if (!isTransitioning)
                    {
                        Room next_room = cllted_rooms[(currentRoom.Ypos - 1, currentRoom.Xpos)];
                        player.Y_Pos = -200;
                        _ = SwitchRoom(currentRoom, next_room);
                        player.Y_Pos = 720;
                    }
                }
                
                direction.Y -= 1;
                animationTimer += deltaTime;
                if (animationTimer < 0.15)
                {
                    Rect_Player.Fill = up1;
                }
                else if (animationTimer < 0.3)
                {
                    Rect_Player.Fill = up2;
                }
                else
                {
                    animationTimer = 0;
                }
            }


            if (moveDown && player.Y_Pos + 100
                <= 840)
            {
                if (currentRoom.DoorBottom && player.Y_Pos >= 730 && player.X_Pos >= 760 && player.X_Pos <= 900 && currentRoom.IsCleared) 
                {
                    if (!isTransitioning)
                    {
                        Room next_room = cllted_rooms[(currentRoom.Ypos + 1, currentRoom.Xpos)];
                        player.Y_Pos = -200;
                        _ = SwitchRoom(currentRoom, next_room);
                        player.Y_Pos = 20;
                    }
                }

                direction.Y += 1;
                animationTimer += deltaTime;
                if (animationTimer < 0.15)
                {
                    Rect_Player.Fill = front1;
                }
                else if (animationTimer < 0.3)
                {
                    Rect_Player.Fill = front2;
                }
                else
                {
                    animationTimer = 0;
                }
            }

            if (direction.Length > 0)
            {
                // chatgpt anfang: wie mach ich das die diagonales laufen gleich schnell ist
                direction.Normalize();
                // chatgpt ende: wie mach ich das die diagonales laufen gleich schnell ist

                double Xnew = player.X_Pos + direction.X * player.Speed * deltaTime;
                double Ynew = player.Y_Pos + direction.Y * player.Speed * deltaTime;

                if (!CollidesWithObstacle(Xnew, player.Y_Pos))
                {
                    player.X_Pos = Xnew;
                }

                if (!CollidesWithObstacle(player.X_Pos, Ynew))
                {
                    player.Y_Pos = Ynew;
                }
            }

            CheckShopCollision();
            CheckExamCollision();
        }

        private void DrawGame()
        {
            
            foreach (Rectangle rect in bulletRects)
            {
                CanvasGame.Children.Remove(rect);
            }

            bulletRects.Clear();

            foreach (Bullets bullet in player.Bullets)
            {
                Rectangle bulletRect = new Rectangle();

                bulletRect.Width = 25;
                bulletRect.Height = 25;

                if (bullet.random == 1)
                {
                    bulletRect.Fill = bullet1;
                }
                else
                {
                    bulletRect.Fill = bullet2;
                }

                
                bulletRect.RenderTransform = new RotateTransform(bullet.rotation, 12.5, 12.5);


                Canvas.SetLeft(bulletRect, bullet.X_pos);
                Canvas.SetTop(bulletRect, bullet.Y_pos);
                Panel.SetZIndex(bulletRect, 10);

                CanvasGame.Children.Add(bulletRect);
                bulletRects.Add(bulletRect);
            }


            Canvas.SetLeft(Rect_Player, player.X_Pos);
            Canvas.SetTop(Rect_Player, player.Y_Pos);
            Panel.SetZIndex(TxtShopHint, 20);



            if (playerNearShop)
            {
                TxtShopHint.Visibility = Visibility.Visible;
            }
            else
            {
                TxtShopHint.Visibility = Visibility.Collapsed;
            }


            // OPS
            foreach (Rectangle rect in enemyRects)
            {
                CanvasGame.Children.Remove(rect);
            }
            enemyRects.Clear();

            foreach (Enemy enemy in currentRoom.Enemies)
            {
                enemy.animationTimer += deltaTime;

                ImageBrush currentFrame;

                if (enemy.lastDirection == "front")
                {
                    currentFrame = enemy.animationTimer < 0.15 ? enemy_front1 : enemy_front2;
                }
                else if (enemy.lastDirection == "up")
                {
                    currentFrame = enemy.animationTimer < 0.15 ? enemy_back1 : enemy_back2;
                }
                else if (enemy.lastDirection == "left")
                {
                    currentFrame = enemy.animationTimer < 0.15 ? enemy_left1 : enemy_left2;
                }
                else
                {
                    currentFrame = enemy.animationTimer < 0.15 ? enemy_right1 : enemy_right2;
                }

                if (enemy.animationTimer >= 0.3)
                {
                    enemy.animationTimer = 0;
                }

                Rectangle enemyRect = new Rectangle();
                enemyRect.Width = 120;
                enemyRect.Height = 90;
                enemyRect.Fill = currentFrame; // TODO here sprite
                Canvas.SetLeft(enemyRect, enemy.X_Pos);
                Canvas.SetTop(enemyRect, enemy.Y_Pos);
                Panel.SetZIndex(enemyRect, 5);
                CanvasGame.Children.Add(enemyRect);
                enemyRects.Add(enemyRect);

                foreach (Bullets bullet in enemy.Bullets)
                {
                    Rectangle bulletRect = new Rectangle();
                    bulletRect.Width = 25;
                    bulletRect.Height = 25;
                    bulletRect.Fill = ammo_coin;
                    bulletRect.RenderTransform = new RotateTransform(bullet.rotation, 12.5, 12.5);
                    Canvas.SetLeft(bulletRect, bullet.X_pos);
                    Canvas.SetTop(bulletRect, bullet.Y_pos);
                    Panel.SetZIndex(bulletRect, 10);
                    CanvasGame.Children.Add(bulletRect);
                    enemyRects.Add(bulletRect); 
                }
            }
            // Boss grafics
            foreach (Rectangle rect in bossRects)
            {
                CanvasGame.Children.Remove(rect);
            }
            bossRects.Clear();

            if (currentBoss != null && currentRoom.Type == 'B')
            {
                Rectangle bossRect = new Rectangle();
                bossRect.Width = 120;
                bossRect.Height = 120;
                bossRect.Fill = boss_sprite;
                Canvas.SetLeft(bossRect, currentBoss.X_Pos);
                Canvas.SetTop(bossRect, currentBoss.Y_Pos);
                Panel.SetZIndex(bossRect, 5);
                CanvasGame.Children.Add(bossRect);
                bossRects.Add(bossRect);

                foreach (Bullets bullet in currentBoss.Bullets)
                {
                    Rectangle bulletRect = new Rectangle();
                    bulletRect.Width = 25;
                    bulletRect.Height = 25;
                    bulletRect.Fill = ammo_coin;
                    bulletRect.RenderTransform = new RotateTransform(bullet.rotation, 12.5, 12.5);
                    Canvas.SetLeft(bulletRect, bullet.X_pos);
                    Canvas.SetTop(bulletRect, bullet.Y_pos);
                    Panel.SetZIndex(bulletRect, 10);
                    CanvasGame.Children.Add(bulletRect);
                    bossRects.Add(bulletRect);
                }
            }
        }


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
            player.Bullets.Clear();
            isTransitioning = true;
            Rect_Player.Visibility = Visibility.Hidden;
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
            if (currentRoom.IsCleared == false)
            {
                if (currentRoom.Type == 'B')
                {
                    currentBoss = new Boss(
                        damage: 2,
                        speed: 100,
                        range: 500,
                        firerate: 1.5,
                        this
                    );

                    currentBoss.X_Pos = 750;
                    currentBoss.Y_Pos = 200;
                }
                else
                {
                    currentBoss = null;
                    currentRoom.SpawnEnemies();
                }
            }
            else
            {
                currentBoss = null;
            }
            Rect_Player.Visibility = Visibility.Visible;
            isTransitioning = false;
        }
        // KI ENDE

        //chatgpt anfang: warum kann ich wenn ich schieße nicht mehr pause drücken(code)
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                e.Handled = true;

                if (!gamePaused)
                {
                    ShowPauseScreen();
                }

                return;
            }

            if (e.Key == Key.E && playerNearShop && !shopOpen && !gamePaused)
            {
                e.Handled = true;

                shopOpen = true;

                Shop shop = new Shop(player);
                shop.ShowDialog();

                shopOpen = false;
                this.Focus();
            }
        }

        private void ShowPauseScreen()
        {
            PauseGame();

            if (pauseScreen == null)
            {
                pauseScreen = new usercontrols.Pausescreen(SaveGame());

                pauseScreen.Width = 600;
                pauseScreen.Height = 700;

                Canvas.SetLeft(pauseScreen, 540);
                Canvas.SetTop(pauseScreen, 88);
                Panel.SetZIndex(pauseScreen, 99);
                // chatgpt mach das der continue button funktioniert(code)
                pauseScreen.ContinueClicked += () =>
                {
                    CanvasGame.Children.Remove(pauseScreen);
                    pauseScreen = null;

                    ResumeGame();
                    this.Focus();
                };
                // ende
                CanvasGame.Children.Add(pauseScreen);
            }
        }

        private void player_bullet_collision_obstacle(Rect obstacle)
        {

            for (int i = player.Bullets.Count - 1; i >= 0; i--)
            {
                Bullets bullet = player.Bullets[i];

                Rect rect_bullet = new Rect(bullet.X_pos, bullet.Y_pos, 10, 10);

                if (rect_bullet.IntersectsWith(obstacle))
                {

                    player.Bullets.RemoveAt(i);
                }
            }
        }


        private void player_bullet_move()
        {
            for (int i = player.Bullets.Count - 1; i >= 0; i--)
            {
                Bullets bullet = player.Bullets[i];

                bullet.X_pos += bullet.Direction.X;
                bullet.Y_pos += bullet.Direction.Y;

                if (bullet.X_pos >= 1680 ||
                    bullet.Y_pos <= 0 ||
                    bullet.X_pos <= 0 ||
                    bullet.Y_pos >= 840)
                {
                    player.Bullets.RemoveAt(i);
                }
            }
        }

        private void shootrangecheck()
        {
            for (int i = player.Bullets.Count - 1; i >= 0; i--)
            {
                Bullets bullet = player.Bullets[i];

               

                if (bullet.Direction.X == 10)
                {
                    if (bullet.X_pos_created + bullet.Range * 100 <= bullet.X_pos)
                    {
                        player.Bullets.RemoveAt(i);
                    }
                }

                if (bullet.Direction.X == -10)
                {
                    if (bullet.X_pos_created - bullet.Range * 100 >= bullet.X_pos)
                    {
                        player.Bullets.RemoveAt(i);
                    }
                }

                if (bullet.Direction.Y == 10)
                {
                    if (bullet.Y_pos_created + bullet.Range * 100 <= bullet.Y_pos)
                    {
                        player.Bullets.RemoveAt(i);
                    }
                }

                if (bullet.Direction.Y == -10)
                {
                    if (bullet.Y_pos_created - bullet.Range * 100 >= bullet.Y_pos)
                    {
                        player.Bullets.RemoveAt(i);
                    }
                }
            }
        }

     
     

        private void CheckShopCollision()
        {
            playerNearShop = false;

            if (currentRoom.Type != 'S')
                return;

            double shopX = 771;
            double shopY = 319;
            double shopWidth = 138;
            double shopHeight = 202;

            double playerX = player.X_Pos;
            double playerY = player.Y_Pos;
            double playerWidth = 73;
            double playerHeight = 100;
            // KI: Chatgpt
            // Prompt: Hey chat kannst du bitte einen bool machen wo true macht wenn man in diese Borders reinkommt weil lwk ich bin faul
            // KI Anfang
            bool intersects = playerX < shopX + shopWidth && playerX + playerWidth > shopX && playerY < shopY + shopHeight && playerY + playerHeight > shopY;
            // KI Ende

            playerNearShop = intersects;
        }

        private void CheckExamCollision()
        {
            if (currentRoom.Type != 'X') return;
            if (!currentRoom.ExamRoom) return;
            if (currentRoom.ExamTriggered) return;

            Rect playerRect = new Rect(
                player.X_Pos,
                player.Y_Pos,
                73,
                100
            );

            if (playerRect.IntersectsWith(currentRoom.ExamBookRect))
            {
                currentRoom.ExamTriggered = true;
                OpenExamRoom();
            }
        }

        private void OpenExamRoom()
        {
            PauseGame();

            question_ask exam = new question_ask(player,difficulty);
            exam.ShowDialog();

            currentRoom.IsCleared = true;
            GradeDisplay.SetNote(player.Grade);
            ResumeGame();
        }

        public void PauseGame()
        {
            gamePaused = true;

            CompositionTarget.Rendering -= GameLoop;

            moveLeft = false;
            moveRight = false;
            moveUp = false;
            moveDown = false;

            shootup = false;
            shootdown = false;
            shootleft = false;
            shootright = false;
        }

        public void ResumeGame()
        {
            if (!gamePaused) return;

            gamePaused = false;
            lastFrameTime = DateTime.Now;
            CompositionTarget.Rendering += GameLoop;
        }

        private void UpdateEnemies()
        {
            for (int i = currentRoom.Enemies.Count - 1; i >= 0; i--)
            {
                Enemy enemy = currentRoom.Enemies[i];

                enemy.Move(player, deltaTime, currentRoom.Enemies);
                enemy.Shoot(player, deltaTime);

                // move bullets
                for (int j = enemy.Bullets.Count - 1; j >= 0; j--)
                {
                    Bullets bullet = enemy.Bullets[j];

                    bullet.X_pos += bullet.Direction.X;
                    bullet.Y_pos += bullet.Direction.Y;

                    // hitbox room
                    if (bullet.X_pos >= 1680 || bullet.Y_pos <= 0 || bullet.X_pos <= 0 || bullet.Y_pos >= 840)
                    {
                        enemy.Bullets.RemoveAt(j);
                        continue;
                    }

                    // Bullet hit player
                    Rect bulletRect = new Rect(bullet.X_pos, bullet.Y_pos, 25, 25);
                    Rect playerRect = new Rect(player.X_Pos + 30, player.Y_Pos + 50, 43, 50);

                    if (bulletRect.IntersectsWith(playerRect))
                    {
                        if (player.Grade - bullet.Damage / difficulty  <= 0)
                        {
                            player.Grade = 0;
                        }
                        else
                        {
                            player.Grade -= bullet.Damage / difficulty;
                        }
                        GradeDisplay.SetNote(player.Grade);
                        enemy.Bullets.RemoveAt(j);
                        break;
                    }
                }

                //player hit enemy
                for (int j = player.Bullets.Count - 1; j >= 0; j--)
                {
                    Bullets bullet = player.Bullets[j];
                    Rect bulletRect = new Rect(bullet.X_pos, bullet.Y_pos, 25, 25);
                    Rect enemyRect = new Rect(enemy.X_Pos, enemy.Y_Pos, 50, 50);

                    if (bulletRect.IntersectsWith(enemyRect))
                    {
                        enemy.Health -= bullet.Damage;
                        player.Bullets.RemoveAt(j);
                        if (player.Grade + 2 * difficulty >= 100)
                        {
                            player.Grade = 100;
                        }
                        else
                        {
                            player.Grade += 2 * difficulty;
                        }
                        GradeDisplay.SetNote(player.Grade);
                        if (enemy.Health <= 0)
                        {
                            currentRoom.Enemies.RemoveAt(i);
                            break;
                        }
                    }
                }
            }

            if (currentRoom.Enemies.Count == 0 && !currentRoom.ExamRoom)
            {
                currentRoom.IsCleared = true;
            }

        }

        private void UpdateBoss()
        {
            if (currentRoom.Type != 'B') 
                return;
            if (currentBoss == null)
            {
                return;
            }
            currentRoom.IsCleared = false;

            currentBoss.Move(player, deltaTime);
            currentBoss.Attack(player, deltaTime);
            if (currentBoss.QuestionCount >= 5)
            {
                currentBoss.Bullets.Clear();
                currentBoss = null;
                currentRoom.IsCleared = true;
                return;
            }

            // Boss bullet move
            for (int j = currentBoss.Bullets.Count - 1; j >= 0; j--)
            {
                Bullets bullet = currentBoss.Bullets[j];

                bullet.X_pos += bullet.Direction.X;
                bullet.Y_pos += bullet.Direction.Y;

                if (bullet.X_pos >= 1680 || bullet.Y_pos <= 0 ||
                    bullet.X_pos <= 0 || bullet.Y_pos >= 840)
                {
                    currentBoss.Bullets.RemoveAt(j);
                    continue;
                }

                // Boss hit player
                Rect bulletRect = new Rect(bullet.X_pos, bullet.Y_pos, 25, 25);
                Rect playerRect = new Rect(player.X_Pos + 30, player.Y_Pos + 50, 43, 50);

                if (bulletRect.IntersectsWith(playerRect))
                {
                    if (player.Grade - bullet.Damage / difficulty <= 0)
                    {
                        player.Grade = 0;
                    }
                    else
                    {
                        player.Grade -= bullet.Damage / difficulty;
                    }

                    GradeDisplay.SetNote(player.Grade);
                    currentBoss.Bullets.RemoveAt(j);
                    continue;
                }
            }
        }
        // KI: Chatgpt
        // Promt: Bitte mach mir eine Funktion wo das hier speichert
        // KI Anfang
        private string SaveGame()
        {
            SaveGame save = new SaveGame();

            // Player
            save.PlayerDamage = player.Damage;
            save.PlayerSpeed = player.Speed;
            save.PlayerRange = player.Range;
            save.PlayerFirerate = player.Firerate;
            save.PlayerGrade = player.Grade;
            save.PlayerEnergy = player.Energy;
            save.PlayerX = player.X_Pos;
            save.PlayerY = player.Y_Pos;

            // aktueller Raum
            save.CurrentRoomX = currentRoom.Xpos;
            save.CurrentRoomY = currentRoom.Ypos;

            // Räume - nur die wichtigen Daten
            save.Rooms = cllted_rooms.Values.Select(r => new RoomSaveData
            {
                Xpos = r.Xpos,
                Ypos = r.Ypos,
                Type = r.Type,
                IsCleared = r.IsCleared,
                DoorTop = r.DoorTop,
                DoorRight = r.DoorRight,
                DoorBottom = r.DoorBottom,
                DoorLeft = r.DoorLeft,
                ExamRoom = r.ExamRoom,
                ExamTriggered = r.ExamTriggered,
                room_matrix = r.room_matrix,
                Enemies = r.Enemies.Select(enemy => new EnemySaveData
                {
                    X_Pos = enemy.X_Pos,
                    Y_Pos = enemy.Y_Pos,
                    Health = enemy.Health,
                    Damage = enemy.Damage,
                    Speed = enemy.Speed,
                    Range = enemy.Range,
                    Firerate = enemy.Firerate
                }).ToList()
            }).ToList();

            // Boss
            if (currentBoss != null)
            {
                save.HasBoss = true;
                save.BossX = currentBoss.X_Pos;
                save.BossY = currentBoss.Y_Pos;
                save.QuestionCount = currentBoss.QuestionCount;
            }
            else
            {
                save.HasBoss = false;
            }

            return JsonSerializer.Serialize(save, new JsonSerializerOptions { WriteIndented = true });
        }

        private void LoadGame()
        {
            try
            {
                if (!File.Exists("save.txt"))
                    return;

                string json = File.ReadAllText("save.txt");
                SaveGame save = JsonSerializer.Deserialize<SaveGame>(json);

                

                // Räume wiederherstellen
                cllted_rooms.Clear();

                foreach (RoomSaveData rd in save.Rooms)
                {
                    Room room = new Room(rd.Ypos, rd.Xpos, rd.Type);

                    room.IsCleared = rd.IsCleared;
                    room.DoorTop = rd.DoorTop;
                    room.DoorRight = rd.DoorRight;
                    room.DoorBottom = rd.DoorBottom;
                    room.DoorLeft = rd.DoorLeft;
                    room.ExamRoom = rd.ExamRoom;
                    room.ExamTriggered = rd.ExamTriggered;
                    room.room_matrix = rd.room_matrix;

                    foreach (EnemySaveData ed in rd.Enemies)
                    {
                        Enemy enemy = new Enemy(
                            damage: ed.Damage,
                            speed: ed.Speed,
                            range: ed.Range,
                            firerate: ed.Firerate,
                            health: ed.Health
                        );

                        enemy.X_Pos = ed.X_Pos;
                        enemy.Y_Pos = ed.Y_Pos;

                        room.Enemies.Add(enemy);
                    }

                    cllted_rooms.Add((rd.Ypos, rd.Xpos), room);
                }

                // aktuellen Raum setzen
                currentRoom = cllted_rooms[(save.CurrentRoomY, save.CurrentRoomX)];
                currentRoom.DrawRoom();


                // Canvas updaten
                List<UIElement> elements = new List<UIElement>();
                foreach (UIElement obj in CanvasGame.Children)
                {
                    elements.Add(obj);
                }
                foreach (UIElement obj in elements)
                {
                    if (obj != Rect_Player)
                    {
                        CanvasGame.Children.Remove(obj);
                    }
                }
                CanvasGame.Children.Add(currentRoom.RoomCanvas);

                // Player wiederherstellen
                player.Damage = save.PlayerDamage;
                player.Speed = save.PlayerSpeed;
                player.Range = save.PlayerRange;
                player.Firerate = save.PlayerFirerate;
                player.Grade = save.PlayerGrade;
                player.Energy = save.PlayerEnergy;
                player.X_Pos = save.PlayerX;
                player.Y_Pos = save.PlayerY;

                // Boss wiederherstellen
                if (save.HasBoss)
                {
                    currentBoss = new Boss(2, 100, 500, 1.5, this);
                    currentBoss.X_Pos = save.BossX;
                    currentBoss.Y_Pos = save.BossY;
                    currentBoss.QuestionCount = save.QuestionCount;
                }

                GradeDisplay.SetNote(player.Grade);
                EnergyDisplay.SetEnergy(player.Energy);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler: " + ex.Message + "\n\n" + ex.StackTrace);
            }
        }
        // KI ENDE
    }
}