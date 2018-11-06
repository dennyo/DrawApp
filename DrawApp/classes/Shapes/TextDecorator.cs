using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace DrawApp.classes
{
    public class TextDecorator : ComponentDecorator
    {
        public enum TextLocations
        {
            top, right, bottom, left
        };
        public List<string> Texts { get; set; }

        public TextDecorator(ShapeComponent shapeComponent, List<string> texts) : base(shapeComponent)
        {
            Fill = Brushes.DarkRed;
            Texts = texts;
        }

        public GeometryGroup GetGeometryGroup()
        {
            GeometryGroup group = new GeometryGroup();
            group.Children.Add(GetGeometry());
            group.Children.Add(ShapeComponent.GetGeometry());
            return group;
        }

        public override void Add(ShapeComponent component)
        {
            throw new NotImplementedException();
        }

        public override bool Contains(ShapeComponent component)
        {
            throw new NotImplementedException();
        }

        public override void Remove(ShapeComponent component)
        {
            throw new NotImplementedException();
        }

        public override void Save(SaveVisitor visitor)
        {
            visitor.Visit(this);
            ShapeComponent.Save(visitor);
        }

        public static int GetLocation(TextLocations location)
        {
            switch (location)
            {
                case TextLocations.top:
                    return 0;
                case TextLocations.right:
                    return 1;
                case TextLocations.bottom:
                    return 2;
                case TextLocations.left:
                    return 3;
            }
            return -1;
        }

        public override Geometry GetGeometry()
        {
            GeometryGroup group = new GeometryGroup();
            for (int a = 0; a < Texts.Count; a++)
            {
                if (!string.IsNullOrEmpty(Texts[a]))
                {
                    FormattedText formattedText = NewFormattedText(Texts[a]);
                    double locx = 0;
                    double locy = 0;
                    if (a % 2 == 0)
                        locx = (ShapeComponent.Width / 2) - (formattedText.Width / 2);
                    if (a % 2 == 1)
                        locy = (ShapeComponent.Height / 2) - (formattedText.Height / 2);
                    if (a % 4 == 2)
                        locy = ShapeComponent.Height - formattedText.Height;
                    if (a % 4 == 1)
                        locx = ShapeComponent.Width - formattedText.Width;
                    group.Children.Add(formattedText.BuildGeometry(new Point(locx, locy)));
                }
            }
            return group;
        }

        private FormattedText NewFormattedText(string description)
        {
            FormattedText text = new FormattedText(description,
                 CultureInfo.CurrentCulture,
                 FlowDirection.LeftToRight,
                 new Typeface("Tahoma"),
                 20,
                 Brushes.Black);
            return text;
        }

        public override Geometry GetGeometry(double x = 0, double y = 0, double width = 5, double height = 5)
        {
            return GetGeometry();
        }
    }
}
