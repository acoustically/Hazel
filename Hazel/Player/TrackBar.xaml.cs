using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hazel.Player
{
    /// <summary>
    /// TrackBar.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TrackBar : UserControl
    {
        public bool pinCliked = false;
        private Point position;
        private Point mousePoisition;
        public event EventHandler positionChanged;

        public TrackBar()
        {
            InitializeComponent();
            Double x = Canvas.GetLeft(pin);
            Double y = Canvas.GetTop(pin);
            position = new Point(x, y);
        }

        public Double Max
        {
            get => 470;
        }

        public Double Min
        {
            get => 15;
        }

        private void PinMouseDown(object sender, MouseButtonEventArgs e)
        {
            pinCliked = true;
            Mouse.Capture(pin);
            mousePoisition = Mouse.GetPosition(this);
        }

        private void PinMouseUp(object sender, MouseButtonEventArgs e)
        {
            if(pinCliked)
            {
                eventEmit();
            }
            pinCliked = false;
            Mouse.Capture(null);
        }

        private void eventEmit()
        {
            DataEventArgs args = new DataEventArgs();
            args.Data1 = position.X - Min;
            args.Data2 = Max - Min;
            positionChanged?.Invoke(this, args);
        }

        private void PinMouseMove(object sender, MouseEventArgs e)
        {
            if(pinCliked)
            {
                Point currentMousePoisition = Mouse.GetPosition(this);
                double distance = currentMousePoisition.X - mousePoisition.X;
                position.X += distance;
                
                position.X -= Min;
                setPosition(position);
                mousePoisition = currentMousePoisition;
            }
        }

        public void setPosition(Point currentPosition)
        {
            currentPosition.X += Min;
            if (currentPosition.X < Min)
            {
                currentPosition.X = Min;
            }
            else if (currentPosition.X > Max)
            {
                currentPosition.X = Max;
            }
            Canvas.SetLeft(pin, currentPosition.X);
            line.X2 = currentPosition.X + 5;
            lineBorder.X2 = currentPosition.X + 5;
            position = currentPosition;
        }

        private void LineMouseDown(object sender, MouseButtonEventArgs e)
        {
            Point currentPosition = Mouse.GetPosition(this);
            currentPosition.X -= 10;
            
            currentPosition.X -= Min;
            setPosition(currentPosition);
            if(!pinCliked)
            {
                eventEmit();
            }
        }
        public void setTime(TimeSpan currentTime, TimeSpan totalTime)
        {
            currentTimeTextBlock.Text = TimeSpanToText(currentTime);
            totalTimeTextBlock.Text = TimeSpanToText(totalTime);
        }

        private String TimeSpanToText(TimeSpan time)
        {
            return time.Hours + ":" + time.Minutes + ":" + time.Seconds;
        }
    }
}
