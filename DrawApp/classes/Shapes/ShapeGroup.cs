﻿using System;
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

        protected override Geometry DefiningGeometry
        {
            get
            {
                return Geometry;
            }
        }

        private void SetNewGeometry()
        {
            Geometry = new GeometryGroup();
            //List<ShapeComponent> Nongroupshapes = GetShapesOfGroup();
            foreach (ShapeComponent component in Shapes)
            {
                if (component is InternalShape shape)
                {
                    Geometry.Children.Add(shape.GetGeometry(shape.Location.X - Location.X, shape.Location.Y - Location.Y, shape.Width, shape.Height));
                }
                else if (component is ShapeGroup group)
                {
                    if (group.Location.X > Location.X)
                    {
                        group.Location = new Point(Location.X + 0.0001, group.Location.Y);
                    }
                    if (group.Location.Y > Location.Y)
                    {
                        group.Location = new Point(group.Location.X, Location.Y + 0.0001);
                    }
                    if (group.Location.X > Location.X || group.Location.Y > Location.Y)
                    {
                        group.SetNewGeometry();
                    }
                    foreach (var item in group.Geometry.Children)
                    {
                        Geometry.Children.Add(item);
                    }
                }
                else if (component is TextDecorator decorator)
                {
                    ShapeComponent decoratedShape = decorator.ShapeComponent;
                    Geometry.Children.Add(decoratedShape.GetGeometry(decoratedShape.Location.X - Location.X, decoratedShape.Location.Y - Location.Y, decoratedShape.Width, decoratedShape.Height));
                    Geometry texts = decorator.GetGeometry();
                    texts.Transform = new TranslateTransform(decoratedShape.Location.X - Location.X, decoratedShape.Location.Y - Location.Y);
                    Geometry.Children.Add(texts);
                }
            }
        }

        //public List<ShapeComponent> GetShapesOfGroup()
        //{
        //    List<ShapeComponent> list = new List<ShapeComponent>();
        //    foreach (ShapeComponent shape in Shapes)
        //    {
        //        if (shape is ShapeGroup group)
        //        {
        //            list.AddRange(group.GetShapesOfGroup());
        //        }
        //        else
        //        {
        //            list.Add(shape);
        //        }
        //    }
        //    return list;
        //}

        public ShapeGroup()
        {
            Location = new Point(int.MaxValue, int.MaxValue);
            Shapes = new List<ShapeComponent>();
            Fill = Brushes.DarkBlue;
        }

        public void Add(ShapeComponent component)
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
            SetNewGeometry();
        }

        //public void Refresh(Point oldLocation)
        //{
        //    double x = Location.X - oldLocation.X;
        //    double y = Location.Y - oldLocation.Y;
        //    foreach (ShapeComponent component in Shapes)
        //    {
        //        SetNewGeometry();
        //    }
        //}

        public void Remove(ShapeComponent component)
        {
            Shapes.Remove(component);
        }

        public bool Contains(ShapeComponent component)
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

        public override Geometry GetGeometry(double x = 0, double y = 0, double width = 5, double height = 5)
        {
            return GetGeometry();
        }
    }
}
