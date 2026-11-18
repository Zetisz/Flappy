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
        private Label burd = new()
        {
            Width = 50,
            Height = 50,
            Background = Brushes.GreenYellow
        };
        private List<Label> obstacles = [];
        private bool isGoing = false;
        private bool canJump = true;
        private DateTime lastJump;
        private int jumpCooldown = 100;
        double fallSpeed = 10;
        double obstacleSpeed = 10;

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

            if (Keyboard.IsKeyDown(Key.Space) && canJump)
            {
                newPos = Canvas.GetTop(burd) - 80;
                canJump = false;
                lastJump = DateTime.Now;
                fallSpeed = 10;
            }
            else
            {
                newPos = Canvas.GetTop(burd) + (fallSpeed++ / 10);
            }

            // Check jump cooldown
            if (!canJump && (DateTime.Now - lastJump).TotalMilliseconds >= jumpCooldown)
            {
                canJump = true;
            }

            Canvas.SetTop(burd, newPos);
        }

        private void ObstacleMovement() // Obstacle Movement
        {
            foreach (var obs in obstacles)
            {
                double newPos = Canvas.GetRight(obs) + 2;
                Canvas.SetRight(obs, newPos);

                if (newPos > 600)
                {
                    Canvas.SetRight(obs, -100);
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
                    Height = 300,
                    Background = Brushes.DarkOliveGreen
                };

                canv.Children.Add(obstacle);
                obstacles.Add(obstacle);
                Canvas.SetRight(obstacle, 0 - spawnIndex * 100);

                if (i % 2 == 0)
                {
                    Canvas.SetTop(obstacle, 0);
                }
                else
                {
                    Canvas.SetTop(obstacle, this.Height - obstacle.Height);
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