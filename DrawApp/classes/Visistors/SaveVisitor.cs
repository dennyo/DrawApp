using DrawApp.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawApp
{
    public class SaveVisitor : IVisitor
    {
        public StringBuilder stringBuilder = new StringBuilder();
        public string spaces { get; set; }
        public void Visit(ShapeGroup group)
        {
            string line = string.Format("{0}group", spaces);
            stringBuilder.AppendLine(line);
        }

        public void Visit(InternalShape shape)
        {
            string line = string.Format("{0}{1} {2} {3} {4} {5}", spaces, shape.GetName(), shape.Location.X, shape.Location.Y, shape.Width, shape.Height);
            stringBuilder.AppendLine(line);
        }
    }
}
