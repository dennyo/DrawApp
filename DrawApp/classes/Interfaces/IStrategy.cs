using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DrawApp.classes.Interfaces
{
    public interface IStrategy
    {
        Geometry GetGeometry(double x = 0, double y = 0, double width = 5, double height = 5);
        string GetName();
    }
}
