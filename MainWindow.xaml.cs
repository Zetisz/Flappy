using System.Text;
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

namespace Flappy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Movement movementSys;
        private Rectangle burd;
        private Label LblScore;
        private bool isGoing;
        private bool afterStart;
        private List<Pipe> pipes = [];
        int pipeNumber = 2;
        private int score;
        
        public MainWindow()
        {
            InitializeComponent();
            movementSys = new Movement();
            CompositionTarget.Rendering += OnRender;
        }

        private void OnRender(object sender, EventArgs e)
        {
            if (isGoing)
            {
                isGoing = movementSys.BurdMovement(burd);
                
                foreach (var pipe in pipes)
                {
                    movementSys.PipeMovement(pipe, pipes);
                    isGoing = isGoing && IsColliding(pipe);
                }
            }
            else if (afterStart) // End Game
            {
                GridMain.Children.Clear();
                afterStart = false;
                movementSys.ResetMovement();

                var lblGameOver = new Label
                {
                    Content = "Game Over",
                    FontSize = 60,
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                var lblScore = new Label
                {
                    Content = "Score: " + score,
                    FontSize = 60,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(50)
                };

                var btnPlayAgain = new Button
                {
                    Content = "Play Again",
                    FontSize = 30,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Height = 100,
                    Width = 200
                };
                
                btnPlayAgain.Click += BtnStart_Click;
                GridMain.Children.Add(lblGameOver);
                GridMain.Children.Add(lblScore);
                GridMain.Children.Add(btnPlayAgain);
            }
        }
        
        private bool IsColliding(Pipe pipe)
        {
            var burdRect = new Rect(Canvas.GetLeft(burd), Canvas.GetTop(burd), burd.Width, burd.Height);
            var pipeTopRect = new Rect(Canvas.GetLeft(pipe.Top), Canvas.GetTop(pipe.Top), pipe.Top.Width, pipe.Top.Height);
            var pipeBottomRect = new Rect(Canvas.GetLeft(pipe.Bottom), Canvas.GetTop(pipe.Bottom), pipe.Bottom.Width, pipe.Bottom.Height);

            if (burdRect.IntersectsWith(pipeBottomRect) || burdRect.IntersectsWith(pipeTopRect))
            {
                return false;
            }
            if (!pipe.HasScored && Canvas.GetLeft(pipe.Top) + pipe.Top.Width < Canvas.GetLeft(burd))
            {
                score++;
                LblScore.Content = score.ToString();
                pipe.HasScored = true;
            }

            return true;
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e) // Start Game & Setup
        {
            GridMain.Children.Clear();
            pipes.Clear();
            score = 0;

            var canv = new Canvas { Background = Brushes.AliceBlue };

            // init burd
            
            burd = new Rectangle
            {
                Width = 50,
                Height = 50,
                Fill = Brushes.GreenYellow
            };

            canv.Children.Add(burd);
            GridMain.Children.Add(canv);
            Canvas.SetLeft(burd, 0 + 150);
            Canvas.SetTop(burd, 200);

            // init pipe
            
            for (var i = 0; i < pipeNumber; i++) // init obstacles
            {
                var part1 = new Rectangle
                {
                    Width = 70,
                    Height = 500,
                    Fill = Brushes.DarkOliveGreen
                };
                
                var part2 = new Rectangle
                {
                    Width = 70,
                    Height = 500,
                    Fill = Brushes.DarkOliveGreen
                };

                var pipe = new Pipe(part1, part2);

                canv.Children.Add(pipe.Top);
                canv.Children.Add(pipe.Bottom);
                
                Canvas.SetLeft(pipe.Top,  Width * (pipeNumber - i));
                Canvas.SetLeft(pipe.Bottom,  Width *  (pipeNumber - i));
                Canvas.SetTop(pipe.Top, -200);
                Canvas.SetTop(pipe.Bottom, 500);
                pipes.Add(pipe);
            }
            
            // init score label
            
            LblScore = new Label
            {
                Content = "0",
                FontSize = 60,
                Foreground = Brushes.Gray,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top
            };

            Canvas.SetLeft(LblScore, 20);
            Canvas.SetTop(LblScore, 20);

            canv.Children.Add(LblScore);
            
            afterStart = true;
            isGoing = true; // Game has started
        }

        
    }
}