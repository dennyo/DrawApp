using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DrawApp.classes
{
    class AddCircleCommand : AddShapeCommand
    {
        public override InternalShape GetShape()
        {
            return new InternalShape(InternalEllipse.getInstance());
            //return new InternalEllipse
            //{
            //    StrokeThickness = 6
            //};
        }
    }
}
