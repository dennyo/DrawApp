using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawApp.classes
{
    public interface IVisitor
    {
        void Visit(InternalShape shape);
        void Visit(ShapeGroup group);
    }
}
