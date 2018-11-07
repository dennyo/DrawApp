using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawApp.classes
{
    public abstract class IOCommand : Command
    {
        public bool IsCompleted { get; set; }

        public abstract void Execute(InternalCanvas canvas);

        void Command.Redo(InternalCanvas canvas)
        {
        }

        void Command.Undo(InternalCanvas canvas)
        {
        }
    }
}
