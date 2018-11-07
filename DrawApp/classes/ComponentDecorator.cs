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
        public GeometryGroup Geometry { get; set; }
        protected override Geometry DefiningGeometry
        {
            get
            {
                return Geometry;
                //GeometryGroup combinedGeometry = new GeometryGroup();
                //combinedGeometry.Children.Add(ShapeComponent.GetGeometry(0,0, ShapeComponent.Width, ShapeComponent.Height));
                //combinedGeometry.Children.Add(GetGeometry());
                //return combinedGeometry;
            }
        }

        public void SetNewGeometry()
        {
            Geometry = new GeometryGroup();
            Geometry.Children.Add(ShapeComponent.GetGeometry(0, 0, ShapeComponent.Width, ShapeComponent.Height));
            Geometry.Children.Add(GetGeometry());
        }

        public ComponentDecorator(ShapeComponent shape)
        {
            Fill = Brushes.DarkBlue;
            ShapeComponent = shape;
            Location = shape.Location;
        }
    }
}
