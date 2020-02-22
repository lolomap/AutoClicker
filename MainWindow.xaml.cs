using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

namespace AutoClicker
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;

        List<System.Drawing.Point> ClickPoints = new List<System.Drawing.Point>();

        public MainWindow()
        {
            InitializeComponent();
            Background = new SolidColorBrush(Color.FromArgb(50, 150, 150, 150));
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var CursorPos = e.GetPosition(this);
            ClickPoints.Add(new System.Drawing.Point((int)CursorPos.X, (int)CursorPos.Y));
        }

        private void Window_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Start.Visibility = Visibility.Visible;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
            foreach(var point in ClickPoints)
            {
                System.Windows.Forms.Cursor.Position = point;
                int x = System.Windows.Forms.Cursor.Position.X;
                int y = System.Windows.Forms.Cursor.Position.Y;
                mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
                mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, 0);

                Thread.Sleep(2000);
            }
        }
    }
}
