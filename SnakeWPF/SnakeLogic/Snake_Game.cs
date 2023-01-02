using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;

namespace SnakeWPF.SnakeLogic
{
    public enum Directions { Stop = 0, Left, Right, Up, Down };
    public class Snake_Part
    {
        public int x { set; get; }
        public int y { set; get; }
        public Directions Direction { set; get; }
        public Ellipse Visualisation { set; get; }
    }
    public class GameInstance
    {
        MainWindow Main_Window = System.Windows.Application.Current.MainWindow as MainWindow;
        static string Resources_Directory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/SnakeResources";
        public string Fruit_Path = Resources_Directory + "/Fruit.png";
        public string Head_Path = Resources_Directory + "/SnakeHead.png";
        public string Body_Path = Resources_Directory + "/SnakeBody.png";
        public string Tail_Path = Resources_Directory + "/SnakeTail.png";
        public string Ground_Path = Resources_Directory + "/Ground.jpg";

        Canvas Game_Canvas;
        TextBlock Score_TextBlock;
        TextBlock Level_TextBlock;

        public List<Snake_Part> Snake_Parts = new List<Snake_Part>();
        public List<Directions> List_Directions = new List<Directions>();

        int Size;
        int Score_Modificator;
        int Speed_Modificator;
        int Initial_Speed;
        public int Score = 0;
        public int Level = 1;
        int Fruit_Position_X;
        int Fruit_Position_Y;
        Ellipse Fruit_Visualisation;

        bool Game_Stance = false;
        Random Random_Generator = new Random();
        DispatcherTimer Game_Timer { get; set; }
        public GameInstance(Canvas GameCanvas, int GameSize, TextBlock ScoreTextBlock, TextBlock LevelTextBlock)
        {
            Game_Canvas = GameCanvas;
            Size = GameSize;
            Score_TextBlock = ScoreTextBlock;
            Level_TextBlock = LevelTextBlock;
            Init();
        }

        void Init()
        {
            Game_Canvas.Background = new ImageBrush(new BitmapImage(new Uri(Ground_Path)));
            Game_Canvas.Children.Clear();
            for (int i = 0; i < Size; i++)
            {
                for (int k = 0; k < Size; k++)
                {
                    Rectangle Cell = new Rectangle();
                    Cell.Height = Game_Canvas.Height / Size;
                    Cell.Width = Game_Canvas.Width / Size;
                    Cell.Stroke = Brushes.Black;
                    Game_Canvas.Children.Add(Cell);
                    Canvas.SetTop(Cell, i * Game_Canvas.Height / Size);
                    Canvas.SetLeft(Cell, k * Game_Canvas.Width / Size);
                }
            }

            Ellipse Head_Visualisation = new Ellipse
            {   Height = Game_Canvas.Height / Size,
                Width = Game_Canvas.Width / Size,
                Fill = new ImageBrush(new BitmapImage(new Uri(Head_Path)))
            };
            Snake_Part Head = new Snake_Part { x = Size / 2, y = Size / 2, Visualisation = Head_Visualisation, Direction = 0 };
            Snake_Parts.Add(Head);
            Game_Canvas.Children.Add(Head_Visualisation);
            Canvas.SetTop(Snake_Parts[0].Visualisation, Snake_Parts[0].y * Game_Canvas.Height / Size);
            Canvas.SetLeft(Snake_Parts[0].Visualisation, Snake_Parts[0].x * Game_Canvas.Width / Size);

            Main_Window.Music_Player.Open(new Uri(Main_Window.Music_Paths[1]));
            Main_Window.Music_Player.Play();
            Score_TextBlock.Text = Score.ToString();
            Level_TextBlock.Text = Level.ToString();

            Score_Modificator = 100;
            Speed_Modificator = (Size >= 10 ? 170 / Size : 100 / Size);
            Initial_Speed = 280 - Speed_Modificator;
            Fruit_Spawn();
            List_Directions.Add(0);
            Game_Stance = true;
            Game_Timer = new DispatcherTimer(DispatcherPriority.Render);
            Game_Timer.Interval = TimeSpan.FromMilliseconds(Initial_Speed);
            Start();
        }

        void Fruit_Spawn()
        {
            Fruit_Position_X = Random_Generator.Next(0, Size);
            Fruit_Position_Y = Random_Generator.Next(0, Size);

            int i = 0;
            while (i < Snake_Parts.Count)
            {
                if ((Snake_Parts[i].x == Fruit_Position_X) && (Snake_Parts[i].y == Fruit_Position_Y))
                {
                    Fruit_Position_X = Random_Generator.Next(0, Size);
                    Fruit_Position_Y = Random_Generator.Next(0, Size);
                    i = -1;
                }
                i++;
            }

            if (Fruit_Visualisation == null)
            {
                Fruit_Visualisation = new Ellipse { Height = Game_Canvas.Height / Size, Width = Game_Canvas.Width / Size,
                    Fill = new ImageBrush(new BitmapImage(new Uri(Fruit_Path)))};
                Game_Canvas.Children.Add(Fruit_Visualisation);
            }
            Canvas.SetTop(Fruit_Visualisation, Fruit_Position_Y * (Game_Canvas.Height / Size));
            Canvas.SetLeft(Fruit_Visualisation, Fruit_Position_X * (Game_Canvas.Width / Size));
        }

        void Visualise()
        {
            int i = Snake_Parts.Count - 1;
            while (i >= 1)
            {
                Canvas.SetTop(Snake_Parts[i].Visualisation, Canvas.GetTop(Snake_Parts[i - 1].Visualisation));
                Canvas.SetLeft(Snake_Parts[i].Visualisation, Canvas.GetLeft(Snake_Parts[i - 1].Visualisation));
                switch (Snake_Parts[i].Direction)
                {
                    case (Directions.Down):
                    {
                        Snake_Parts[i].Visualisation.RenderTransform = new RotateTransform(0, Game_Canvas.Width / (2 * Size), Game_Canvas.Height / (2 * Size));
                        break;
                    }
                    case (Directions.Up):
                    {
                        Snake_Parts[i].Visualisation.RenderTransform = new RotateTransform(180, Game_Canvas.Width / (2 * Size), Game_Canvas.Height / (2 * Size));
                        break;
                    }
                    case (Directions.Left):
                    {
                        Snake_Parts[i].Visualisation.RenderTransform = new RotateTransform(90, Game_Canvas.Width / (2 * Size), Game_Canvas.Height / (2 * Size));
                        break;
                    }
                    case (Directions.Right):
                    {
                        Snake_Parts[i].Visualisation.RenderTransform = new RotateTransform(270, Game_Canvas.Width / (2 * Size), Game_Canvas.Height / (2 * Size));
                        break;
                    }
                }
                i--;
            }
            Canvas.SetTop(Snake_Parts[0].Visualisation, Snake_Parts[0].y * Game_Canvas.Height / Size);
            Canvas.SetLeft(Snake_Parts[0].Visualisation, Snake_Parts[0].x * Game_Canvas.Width / Size);
            switch (Snake_Parts[0].Direction)
            {
                case (Directions.Down):
                    {
                        Snake_Parts[0].Visualisation.RenderTransform = new RotateTransform(180, Game_Canvas.Width / (2 * Size), Game_Canvas.Height / (2 * Size));
                        break;
                    }
                case (Directions.Up):
                    {
                        Snake_Parts[0].Visualisation.RenderTransform = new RotateTransform(0, Game_Canvas.Width / (2 * Size), Game_Canvas.Height / (2 * Size));
                        break;
                    }
                case (Directions.Left):
                    {
                        Snake_Parts[0].Visualisation.RenderTransform = new RotateTransform(270, Game_Canvas.Width / (2 * Size), Game_Canvas.Height / (2 * Size));
                        break;
                    }
                case (Directions.Right):
                    {
                        Snake_Parts[0].Visualisation.RenderTransform = new RotateTransform(90, Game_Canvas.Width / (2 * Size), Game_Canvas.Height / (2 * Size));
                        break;
                    }
            }
        }

        void Logic()
        {
            int End_of_the_tail_x = Snake_Parts[Snake_Parts.Count - 1].x;
            int End_of_the_tail_y = Snake_Parts[Snake_Parts.Count - 1].y;

            if (Snake_Parts[0].Direction != 0)
                for (int i = Snake_Parts.Count - 1; i >= 1; i--)
                {
                    Snake_Parts[i].Direction = Snake_Parts[i - 1].Direction;
                    Snake_Parts[i].x = Snake_Parts[i - 1].x;
                    Snake_Parts[i].y = Snake_Parts[i - 1].y;
                }

            int d = List_Directions.Count - 1;
            while (((Snake_opposite(List_Directions[d], List_Directions[0]) && (Snake_Parts.Count > 1)) || (List_Directions[0] == List_Directions[d])) && (d > 0))
                d--;

            Directions Current_Direction = List_Directions[d];
            switch (Current_Direction)
            {
                case (Directions.Left):
                    {
                        Snake_Parts[0].x -= 1;
                        break;
                    }
                case (Directions.Up):
                    {
                        Snake_Parts[0].y -= 1;
                        break;
                    }
                case (Directions.Right):
                    {
                        Snake_Parts[0].x += 1;
                        break;
                    }
                case (Directions.Down):
                    {
                        Snake_Parts[0].y += 1;
                        break;
                    }
            }

            List_Directions.Clear();
            List_Directions.Add(Current_Direction);
            Snake_Parts[0].Direction = Current_Direction;

            if (Snake_Parts[0].x == -1)
                Snake_Parts[0].x = Size - 1;
            else if (Snake_Parts[0].x >= Size)
                Snake_Parts[0].x = 0;
            if (Snake_Parts[0].y == -1)
                Snake_Parts[0].y = Size - 1;
            else if (Snake_Parts[0].y >= Size)
                Snake_Parts[0].y = 0;

            if ((Snake_Parts[0].x == Fruit_Position_X) && (Snake_Parts[0].y == Fruit_Position_Y))
            {
                Main_Window.Sound_Player.SoundLocation = Main_Window.Eatsounds_Paths[Random_Generator.Next(0,7)];
                Main_Window.Sound_Player.Load();
                Main_Window.Sound_Player.Play();

                Score += Score_Modificator;
                Score_TextBlock.Text = Score.ToString();

                Create_New_Snake_Part(End_of_the_tail_x, End_of_the_tail_y);

                if (Snake_Parts.Count == Size * Level)
                {
                    Level += 1;
                    Level_TextBlock.Text = Level.ToString();
                    Game_Timer.Interval = TimeSpan.FromMilliseconds(Initial_Speed - Speed_Modificator * Level);
                }
                if (Snake_Parts.Count == Size * Size)
                    End("You won!");
                else
                    Fruit_Spawn();
            }
            else if (Snake_Parts[0].Direction != 0)
            {
                Main_Window.Sound_Player.SoundLocation = Main_Window.Walksounds_Paths[Random_Generator.Next(0, 4)];
                Main_Window.Sound_Player.Load();
                Main_Window.Sound_Player.Play();
            }

            int k = 1;
            while (k < Snake_Parts.Count)
            {
                if ((Snake_Parts[k].x == Snake_Parts[0].x) && (Snake_Parts[k].y == Snake_Parts[0].y))
                {
                    End("Game Over!");
                }
                k++;
            }

        }

        bool Snake_opposite(Directions Last_Direction, Directions Direction)
        {
            return (((Last_Direction == Directions.Left) && (Direction == Directions.Right))
                || ((Last_Direction == Directions.Right) && (Direction == Directions.Left))
                || ((Last_Direction == Directions.Up) && (Direction == Directions.Down)
                || ((Last_Direction == Directions.Down) && (Direction == Directions.Up))));
        }

        void Create_New_Snake_Part(int X, int Y)
        {
            Ellipse Tail_Visualisation = new Ellipse
            {
                Height = Game_Canvas.Height / Size, 
                Width = Game_Canvas.Width / Size,
                Fill = new ImageBrush(new BitmapImage(new Uri(Tail_Path)))
            };
            if (Snake_Parts.Count >= 2)
            {
                Ellipse New_Snake_Part_Visualisation = new Ellipse
                {
                    Height = Game_Canvas.Height / Size,
                    Width = Game_Canvas.Width / Size,
                    Fill = new ImageBrush(new BitmapImage(new Uri(Body_Path)))
                };
                Game_Canvas.Children.Add(New_Snake_Part_Visualisation);
                Game_Canvas.Children.Remove(Snake_Parts[Snake_Parts.Count - 1].Visualisation);
                Snake_Parts[Snake_Parts.Count - 1].Visualisation = New_Snake_Part_Visualisation;
                Canvas.SetTop(New_Snake_Part_Visualisation, Y * Game_Canvas.Height / Size);
                Canvas.SetLeft(New_Snake_Part_Visualisation, X * Game_Canvas.Width / Size);
            }
            Game_Canvas.Children.Add(Tail_Visualisation);
            Canvas.SetTop(Tail_Visualisation, Y * Game_Canvas.Height / Size);
            Canvas.SetLeft(Tail_Visualisation, X * Game_Canvas.Width / Size);
            Snake_Parts.Add(new Snake_Part { x = X, y = Y, Visualisation = Tail_Visualisation });
        }

        public void End(string Game_Status)
        {
            string Records_Path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Records.txt";

            if (Game_Status != "")
            {
                Main_Window.Sound_Player.SoundLocation = Main_Window.Deathsounds_Paths[Random_Generator.Next(0, 4)];
                Main_Window.Sound_Player.Load();
                Main_Window.Sound_Player.Play();

                Main_Window.Music_Player.Stop();

                Game_Stance = false;

                TextBlock Game_Over = new TextBlock();
                Game_Over.Text = Game_Status + "\nENTER to restart with current settings\nESC to quit to Main Menu!";
                Game_Over.TextAlignment = TextAlignment.Center;
                Game_Over.FontSize = 30;

                TextBlock Record_status = new TextBlock();

                int top_record = 0;
                int i = 0;
                while (i < Main_Window.Records_List.Count)
                {
                    if ((Convert.ToInt32(Main_Window.Records_List[i][2]) == Size) && (Convert.ToInt32(Main_Window.Records_List[i][1]) > top_record))
                        top_record = Convert.ToInt32(Main_Window.Records_List[i][1]);
                    i++;
                }

                if ((top_record == 0) || ((top_record < Score) && (top_record != 0)))
                {
                    Record_status.Text = "New record for this size!\n";
                    Record_status.Foreground = Brushes.Green;
                }
                Record_status.Text += "Score: " + Score.ToString();
                Record_status.TextAlignment = TextAlignment.Center;
                Record_status.FontSize = 30;

                DockPanel End_Dock = new DockPanel();
                End_Dock.Background = Brushes.White;
                End_Dock.Height = Game_Canvas.Height * 2 / 3;
                End_Dock.Width = Game_Canvas.Width * 2 / 3;
                End_Dock.Children.Add(Game_Over);
                End_Dock.Children.Add(Record_status);
                DockPanel.SetDock(Game_Over, Dock.Top);
                DockPanel.SetDock(Record_status, Dock.Bottom);

                Game_Canvas.Children.Add(End_Dock);
                Canvas.SetTop(End_Dock, Game_Canvas.Height / 6);
                Canvas.SetLeft(End_Dock, Game_Canvas.Width / 6);

                if (!File.Exists(Records_Path))
                {
                    FileStream Records_Creating = File.Create(Records_Path);
                    Records_Creating.Close();
                }
                string res = Main_Window.User_Name + ":" + Score + ":" + Size + '\n';
                string[] temp = res.Split(':');
                File.AppendAllText(Records_Path, res);
                Main_Window.Records_List.Add(res.Split(':'));

            }
            else
            {
                Game_Stance = false;
                Main_Window.Sound_Player.Stop();
                Main_Window.Music_Player.Stop();
            }
        }

        void Start()
        {
            Game_Timer.Tick += Game_Tick;
            Game_Timer.Start();
        }

        private void Game_Tick(object sender, EventArgs e)
        {
            if (Game_Stance)
            {
                Logic();
                Visualise();
            }
        }

    }
}