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

namespace RulerSample
{
    public partial class MainWindow : Window
    {
        private Point _startPoint;
        private Ruler? _ruler;
        private bool _isMoving = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void DrawingCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isMoving = true;
            Point clickPosition = e.GetPosition(DrawingCanvas);

            _startPoint = clickPosition;
            _ruler = new Ruler()
            {
                Stroke = Brushes.Red,
                StrokeThickness = 2,
                X1 = _startPoint.X,
                Y1 = _startPoint.Y,
                X2 = _startPoint.X,
                Y2 = _startPoint.Y
            };
            DrawingCanvas.Children.Add(_ruler);
            DrawingCanvas.CaptureMouse();
        }

        private void DrawingCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMoving && _ruler != null)
            {
                Point currentPosition = e.GetPosition(DrawingCanvas);
                _ruler.X2 = currentPosition.X;
                _ruler.Y2 = currentPosition.Y;
            }
        }

        private void DrawingCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DrawingCanvas.ReleaseMouseCapture();
            _isMoving = false;
            _ruler = null;
        }
    }
}