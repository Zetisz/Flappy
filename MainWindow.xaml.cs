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
        private Label obstacle = new()
        {
            Width = 50,
            Height = 200,
            Background = Brushes.DarkOliveGreen
        };

        private List<Label> obstacles = [];
        public MainWindow()
        {
            InitializeComponent();
            CompositionTarget.Rendering += OnRender;
        }

        private void OnRender(object sender, EventArgs e)
        {
            Movement();
            ObstacleMovement();

            /*
            if (Canvas.GetTop(burd) == Canvas.(obstacle))
            {
                GridMain.Children.Clear();
            }*/
        }

        private void Movement()
        {
            double newPos;

            if (Keyboard.IsKeyDown(Key.Space))
            {
                newPos = Canvas.GetTop(burd) - 20;
            }
            else
            {
                newPos = Canvas.GetTop(burd) + 3;
            }
            Canvas.SetTop(burd, newPos);
        }

        private void ObstacleMovement()
        {
            double newPos = Canvas.GetRight(obstacle) + 2;
            Canvas.SetRight(obstacle, newPos);

            if (newPos > 600)
            {
                Canvas.SetRight(obstacle, -20);
            }
        }


        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            GridMain.Children.Clear();

            var canv = new Canvas()
            {
                Background = Brushes.AliceBlue
            };

            canv.Children.Add(burd);
            canv.Children.Add(obstacle);
            GridMain.Children.Add(canv);
            obstacles.Add(obstacle);

            Canvas.SetLeft(burd, 150);
            Canvas.SetTop(burd, 200);
            Canvas.SetRight(obstacle, 0);
            Canvas.SetTop(obstacle, 0);
        }
    }
}