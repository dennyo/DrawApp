using DrawApp.classes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawApp.classes
{
    public class InternalShape : ShapeComponent
    {
        public bool Selected { get; set; }

        private IStrategy Strategy { get; set; }

        //public enum MousePositionType
        //{
        //    None, Body, UL, UR, DR, DL, R, L, T, B
        //};

        //public MousePositionType MousePosition { get; set; }
        
        public InternalShape (IStrategy strategy)
        {
            //Stroke = Brushes.LightBlue;
            Fill = Brushes.DarkBlue;
            Stretch = Stretch.Fill;
            Strategy = strategy;
        }
        protected override Geometry DefiningGeometry
        {
            get { return Strategy.GetGeometry(); }
        }

        public string GetName()
        {
            return Strategy.GetName();
        }

        public override Geometry GetGeometry()
        {
            return Strategy.GetGeometry(Location.X, Location.Y, Width, Height);
        }

        public Geometry GetGeometry(double x = 0, double y = 0, double width = 5, double height = 5)
        {
            return Strategy.GetGeometry(x,y,width,height);
        }

        //public Point Location { get; set; }

        //public MousePositionType GetMousePositionType(Point point)
        //{
        //    double left = Location.X;
        //    double top = Location.Y;
        //    double right = left + ActualWidth;
        //    double bottom = top + ActualHeight;
        //    if (point.X < left || point.X > right || point.Y < top || point.Y > bottom ) return MousePositionType.None;

        //    const double GAP = 10;
        //    if (point.X - left < GAP)
        //    {
        //        // Left edge.
        //        if (point.Y - top < GAP) return MousePositionType.UL;
        //        if (bottom - point.Y < GAP) return MousePositionType.DL;
        //        return MousePositionType.L;
        //    }
        //    else if (right - point.X < GAP)
        //    {
        //        // Right edge.
        //        if (point.Y - top < GAP) return MousePositionType.UR;
        //        if (bottom - point.Y < GAP) return MousePositionType.DR;
        //        return MousePositionType.R;
        //    }
        //    if (point.Y - top < GAP) return MousePositionType.T;
        //    if (bottom - point.Y < GAP) return MousePositionType.B;
        //    return MousePositionType.Body;
        //}

        public override void Add(ShapeComponent d)
        {
            throw new NotSupportedException();
        }

        public override void Remove(ShapeComponent d)
        {
            throw new NotSupportedException();
        }

        public override bool Contains(ShapeComponent component)
        {
            throw new NotSupportedException();
        }

        public override void Save(SaveVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
