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
        Random rand = new Random();
        Label burd = new()
        {
            Width = 50,
            Height = 50,
            Background = Brushes.GreenYellow
        };
        List<Label> obstacles = [];
        bool isGoing = false;
        bool canJump = true;
        DateTime lastJump;
        int flapCooldown = 200;
        int flapHeight = 80;
        double fallSpeed = 10;
        double obstacleSpeed = 1000;
        

        public MainWindow()
        {
            InitializeComponent();
            CompositionTarget.Rendering += OnRender;
        }

        private void OnRender(object sender, EventArgs e)
        {
            if (isGoing)
            {
                Movement();
                ObstacleMovement();

                /*
                if (Canvas.GetTop(burd) == Canvas.(obstacle))
                {
                    GridMain.Children.Clear();
                }*/
            }
        }

        private void Movement() // Burd movement
        {
            double newPos;

            if (Keyboard.IsKeyDown(Key.Space) && canJump && (Canvas.GetTop(burd) - flapHeight > 0)) // flap
            {
                newPos = Canvas.GetTop(burd) - flapHeight;
                canJump = false;
                lastJump = DateTime.Now;
                fallSpeed = 10;
            }
            else
            {
                newPos = Canvas.GetTop(burd) + (fallSpeed++ / 10); // Exponential fall speed
            }

            // Check jump cooldown
            if (!canJump && (DateTime.Now - lastJump).TotalMilliseconds >= flapCooldown)
            {
                canJump = true;
            }

            Canvas.SetTop(burd, newPos);
        }

        private void ObstacleMovement() // Obstacle Movement
        {
            int randomX = rand.Next(-200, 200);
            for (int i = 0; i < obstacles.Count(); i++)
            {
                double newPos = Canvas.GetRight(obstacles[i]) + obstacleSpeed++ / 1000;
                Canvas.SetRight(obstacles[i], newPos);

                if (newPos > 600)
                {
                    Canvas.SetRight(obstacles[i], -100);
                    
                    if (i % 2 == 0)
                    {
                        Canvas.SetTop(obstacles[i], (20 - obstacles[i].Height / 2) + randomX);
                    }
                    else
                    {
                        Canvas.SetBottom(obstacles[i], (20 - obstacles[i].Height / 2) - randomX);
                    }
                }
            }
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e) // Start Game & Setup
        {
            GridMain.Children.Clear();

            var canv = new Canvas()
            {
                Background = Brushes.AliceBlue
            };

            var spawnIndex = 0;
            for (int i = 0; i < 2; i++) // init obstacles
            {
                var obstacle = new Label()
                {
                    Width = 70,
                    Height = 500,
                    Background = Brushes.DarkOliveGreen
                };

                canv.Children.Add(obstacle);
                obstacles.Add(obstacle);
                Canvas.SetRight(obstacle, 0 - spawnIndex * 100);

                if (i % 2 == 0)
                {
                    Canvas.SetTop(obstacle, 20 - obstacle.Height / 2);
                }
                else
                {
                    Canvas.SetBottom(obstacle, 20 - obstacle.Height / 2);
                    spawnIndex++;
                }
                
            }

            canv.Children.Add(burd);
            GridMain.Children.Add(canv);
            Canvas.SetLeft(burd, 150);
            Canvas.SetTop(burd, 200);

            isGoing = true; // Game has started
        }
    }
}