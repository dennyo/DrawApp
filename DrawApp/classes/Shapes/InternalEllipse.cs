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
    public class InternalEllipse : IStrategy
    {
        private static InternalEllipse instance = new InternalEllipse();
        private InternalEllipse()
        {
        }

        public static InternalEllipse getInstance()
        {
            return instance;
        }

        public Geometry GetGeometry(double x = 0, double y = 0, double width = 5, double height = 5)
        {
            return new EllipseGeometry(new Rect(x, y, width, height));
        }

        public string GetName()
        {
            return "ellipse";
        }
    }
}
