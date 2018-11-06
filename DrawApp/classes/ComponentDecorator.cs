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
        public ShapeComponent ShapeComponent;
        protected override Geometry DefiningGeometry
        {
            get
            {
                GeometryGroup combinedGeometry = new GeometryGroup();
                combinedGeometry.Children.Add(ShapeComponent.GetGeometry(0,0, ShapeComponent.Width, ShapeComponent.Height));
                combinedGeometry.Children.Add(GetGeometry());
                return combinedGeometry;
            }
        }

        public ComponentDecorator(ShapeComponent shape)
        {
            Fill = Brushes.DarkBlue;
            ShapeComponent = shape;
            Location = shape.Location;
        }
    }
}
