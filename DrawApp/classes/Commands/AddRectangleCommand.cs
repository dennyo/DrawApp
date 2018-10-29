using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;

namespace DrawApp.classes
{
    class AddRectangleCommand : AddShapeCommand
    {
        public override InternalShape GetShape()
        {
            return new InternalShape(InternalRectangle.getInstance());
            //return new InternalRectangle
            //{
            //    //Stroke = Brushes.LightBlue,
            //    //Fill = Brushes.LightBlue,
            //    StrokeThickness = 6,
            //    Fill = Brushes.DarkBlue
            //};
        }
    }
}
