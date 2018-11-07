using DrawApp.classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using static DrawApp.classes.ShapeComponent;

namespace DrawApp
{
    public class InternalCanvas : Canvas, INotifyPropertyChanged
    {
        public List<ShapeComponent> Shapes { get; set; }

        private static InternalCanvas instance = new InternalCanvas();

        private InternalCanvas()
        {
            Shapes = new List<ShapeComponent>();
            Background = Brushes.LightSteelBlue;
        }

        public static InternalCanvas getInstance()
        {
            return instance;
        }

        public void AddShape(ShapeComponent newshape)
        {
            SetLeft(newshape, newshape.Location.X);
            SetTop(newshape, newshape.Location.Y);
            Shapes.Add(newshape);
            Children.Add(newshape);
        }

        public void SetNewShape(ShapeComponent newshape, double x, double y)
        {
            SetLeft(newshape, x);
            SetTop(newshape, y);
        }

        public void AddNewShape(ShapeComponent newshape)
        {
            Shapes.Add(newshape);
        }

        public Point GetPositionOfShapeInCanvas(ShapeComponent shape)
        {
            return new Point(GetLeft(shape), GetTop(shape));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void Reset()
        {
            Shapes = new List<ShapeComponent>();
            Children.Clear();
        }

        private void SetMouseCursor(ShapeComponent shape)
        {
            // See what cursor we should display.
            Cursor desired_cursor = Cursors.Arrow;
            switch (shape.MousePosition)
            {
                case MousePositionType.None:
                    desired_cursor = Cursors.Arrow;
                    break;
                case MousePositionType.Body:
                    desired_cursor = Cursors.ScrollAll;
                    break;
                case MousePositionType.UL:
                case MousePositionType.DR:
                    desired_cursor = Cursors.SizeNWSE;
                    break;
                case MousePositionType.DL:
                case MousePositionType.UR:
                    desired_cursor = Cursors.SizeNESW;
                    break;
                case MousePositionType.T:
                case MousePositionType.B:
                    desired_cursor = Cursors.SizeNS;
                    break;
                case MousePositionType.L:
                case MousePositionType.R:
                    desired_cursor = Cursors.SizeWE;
                    break;
            }

            // Display the desired cursor.
            if (Cursor != desired_cursor) Cursor = desired_cursor;
        }
    }
}
