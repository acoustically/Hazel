using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Hazel.Player
{
    /// <summary>
    /// VolumeTrackBar.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class VolumeTrackBar : UserControl
    {
        private bool pinClicked;
        private Point mousePosition;
        public const double MAX = 110;
        public const double MIN = 10;
        public event EventHandler volumeChanged;

        public VolumeTrackBar()
        {
            InitializeComponent();
            pinClicked = false;
        }

        private void PinMouseDown(object sender, MouseButtonEventArgs e)
        {
            pinClicked = true;
            Mouse.Capture(pin);
            mousePosition = Mouse.GetPosition(this);
        }

        private void PinMouseUp(object sender, MouseButtonEventArgs e)
        {
            pinClicked = false;
            Mouse.Capture(null);
        }

        private void PinMouseMove(object sender, MouseEventArgs e)
        {
            if (pinClicked)
            {
                Point currentMousePosition = Mouse.GetPosition(this);
                double distance = currentMousePosition.X - mousePosition.X;
                double pinPosition = Canvas.GetLeft(pin);
                SetPosition(pinPosition + distance - MIN);
                SetVolume(pinPosition + distance - MIN);
                mousePosition = currentMousePosition;
            }
        }

        public void SetPosition(double pinPosition)
        {
            pinPosition += MIN;
            if(pinPosition < MIN)
            {
                pinPosition = MIN;
            }
            else if(pinPosition > MAX)
            {
                pinPosition = MAX;
            }
            Canvas.SetLeft(pin, pinPosition);
            line.X2 = pinPosition + 5;
            lineBorder.X2 = pinPosition + 5;
        }

        public void SetVolume(double pinPosition)
        {
            pinPosition += MIN;
            if(pinPosition < MIN)
            {
                pinPosition = MIN;
            }
            else if(pinPosition > MAX)
            {
                pinPosition = MAX;
            }
            DataEventArgs args = new DataEventArgs();
            args.Data1 = pinPosition - MIN;
            args.Data2 = MAX - MIN;
            volumeChanged?.Invoke(this, args);
        }

        private void TrackBarMouseDown(object sender, MouseButtonEventArgs e)
        {
            double position = Mouse.GetPosition(trackGrid).X - MIN - 5;
            SetPosition(position);
            SetVolume(position);
        }
    }
}
