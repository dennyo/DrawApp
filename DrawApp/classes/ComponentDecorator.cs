using DrawApp.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DrawApp
{
    public abstract class ComponentDecorator : ShapeComponent
    {
        protected ShapeComponent ShapeComponent;
        protected override Geometry DefiningGeometry
        {
            get
            {
                GeometryGroup combinedGeometry = new GeometryGroup();
                combinedGeometry.Children.Add(GetGeometry());
                ShapeComponent.Location = new Point(0, 0);
                combinedGeometry.Children.Add(ShapeComponent.GetGeometry());
                return combinedGeometry;
            }
        }

        public ComponentDecorator(ShapeComponent shape)
        {
            Fill = Brushes.LightBlue;
            ShapeComponent = shape;
            Location = shape.Location;
        }
    }
}
