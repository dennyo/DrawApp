using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawApp.classes
{
    public class Actions
    {
        private List<Command> commands = new List<Command>();
        private List<Command> redocommands = new List<Command>();

        public void Execute(InternalCanvas canvas, Command command)
        {
            commands.Add(command);
            command.Execute(canvas);
        }

        public void Execute(InternalCanvas canvas)
        {
            if (commands.Count > 0 && !commands.Last().IsCompleted)
            {
                commands.Last().Execute(canvas);
                redocommands.Clear();
            }
        }

        public void Undo(InternalCanvas canvas)  
        {
            if (commands.Count > 0)
            {
                Command oldcommand = commands.Last();
                commands.Remove(oldcommand);
                oldcommand.Undo(canvas);
                redocommands.Add(oldcommand);
            }
        }

        public void Redo(InternalCanvas canvas)
        {
            if (redocommands.Count > 0)
            {
                Command redocommand = redocommands.Last();
                redocommand.Redo(canvas);
                commands.Add(redocommand);
                redocommands.Remove(redocommand);
            }
        }

        public bool IsLastCommandCompleted()
        {
            if (commands.Last() is AddGroupCommand)
                return commands.Last().IsCompleted;
            return true;
        }
    }
}
