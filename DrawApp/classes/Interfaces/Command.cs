using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawApp.classes
{
    public interface Command
    {
        void Execute(InternalCanvas canvas);
        void Undo(InternalCanvas canvas);
        void Redo(InternalCanvas canvas);
        bool IsCompleted { get; set; }
    }
}
