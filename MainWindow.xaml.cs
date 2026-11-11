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
        private Label burd = new Label()
        {
            Width = 50,
            Height = 50,
            Background = Brushes.GreenYellow
        };
        private Label obstacle = new Label()
        {
            Width = 50,
            Height = 200,
            Background = Brushes.DarkOliveGreen
        };
        public MainWindow()
        {
            InitializeComponent();
            CompositionTarget.Rendering += OnRender;
        }

        private void OnRender(object sender, EventArgs e)
        {
            Movement();

            double newPos = Canvas.GetRight(obstacle) + 2;
            Canvas.SetRight(obstacle, newPos);

            if (newPos > 600) {
                Canvas.SetRight(obstacle, -20);
            }
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

            Canvas.SetLeft(burd, 150);
            Canvas.SetTop(burd, 200);
            Canvas.SetRight(obstacle, 0);
            Canvas.SetTop(obstacle, 0);
        }
    }
}