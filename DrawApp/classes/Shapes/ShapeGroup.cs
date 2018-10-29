using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DrawApp.classes
{
    public class ShapeGroup : ShapeComponent
    {
        public List<ShapeComponent> Shapes { get; set; }

        public GeometryGroup Geometry { get; set; }

        public Point LocationG { get; set; }

        protected override Geometry DefiningGeometry
        {
            get
            {
                return Geometry;
            }
        }

        private void SetNewGeometry()
        {
            Geometry = new GeometryGroup
            {
                FillRule = FillRule.EvenOdd
            };
            foreach (ShapeComponent component in Shapes)
            {
                if (component is InternalShape shape)
                {
                    Geometry.Children.Add(shape.GetGeometry(shape.Location.X - LocationG.X, shape.Location.Y - LocationG.Y, shape.Width, shape.Height));
                }
                else if (component is ShapeGroup group)
                {
                    //GeometryGroup a = GetGroupGeometry(group);
                    foreach (var item in group.Geometry.Children)
                    {
                        //Geometry.Children.Add(item.GetGeometry(shape.Location.X - LocationG.X, shape.Location.Y - LocationG.Y, shape.Width, shape.Height));
                        Geometry.Children.Add(item);
                    }
                }
                else if (component is TextDecorator decorator)
                {
                    foreach (var item in decorator.GetGeometryGroup().Children)
                    {
                        Geometry.Children.Add(item);
                    }
                }
            }
        }

        //private GeometryGroup GetGroupGeometry(ShapeGroup group)
        //{
        //    GeometryGroup = new GeometryGroup();
        //    foreach (ShapeComponent component in Shapes)
        //    {
        //        if (component is InternalShape shape)
        //        {
        //            Geometry.Children.Add(shape.GetGeometry(shape.Location.X - LocationG.X, shape.Location.Y - LocationG.Y, shape.Width, shape.Height));
        //        }
        //        else if (component is ShapeGroup group)
        //        {
        //            GeometryGroup a = GetGroupGeometry(group);
        //            foreach (var item in group.Geometry.Children)
        //            {
        //                Geometry.Children.Add(item.GetGeometry(shape.Location.X - LocationG.X, shape.Location.Y - LocationG.Y, shape.Width, shape.Height));
        //                //Geometry.Children.Add(item);
        //            }
        //        }
        //    }
        //    return GeometryGroup
        //}

        public ShapeGroup()
        {
            LocationG = new Point(int.MaxValue, int.MaxValue);
            Location = new Point(int.MaxValue, int.MaxValue);
            Shapes = new List<ShapeComponent>();
            Fill = Brushes.LightBlue;
        }

        public override void Add(ShapeComponent component)
        {
            Shapes.Add(component);
            if (Location.X > component.Location.X)
            {
                Location = new Point(component.Location.X, Location.Y);
            }
            if (Location.Y > component.Location.Y)
            {
                Location = new Point(Location.X, component.Location.Y);
            }
            if (LocationG.X > component.Location.X)
            {
                LocationG = new Point(component.Location.X, LocationG.Y);
            }
            if (LocationG.Y > component.Location.Y)
            {
                LocationG = new Point(LocationG.X, component.Location.Y);
            }
            SetNewGeometry();
        }

        public override void Remove(ShapeComponent component)
        {
            Shapes.Remove(component);
        }

        public override bool Contains (ShapeComponent component)
        {
            return Shapes.Contains(component);
        }

        public override void Save(SaveVisitor visitor)
        {
            visitor.Visit(this);
            visitor.spaces += "   ";
            foreach (ShapeComponent shape in Shapes)
            {
                shape.Save(visitor);
            }
            visitor.spaces = visitor.spaces.Substring(0, visitor.spaces.Length - 3);
        }

        public override Geometry GetGeometry()
        {
            return Geometry;
        }
    }
}
