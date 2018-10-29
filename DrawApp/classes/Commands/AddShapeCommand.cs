using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DrawApp.classes
{
    abstract class AddShapeCommand : Command
    {
        public InternalShape newshape;
        public Point startPoint;
        public bool IsCompleted { get; set; }

        public AddShapeCommand()
        {
            IsCompleted = false;
        }

        public void ExecuteMouseDown(InternalCanvas canvas)
        {
            startPoint = Mouse.GetPosition(canvas);
            newshape = GetShape();
            canvas.SetNewShape(newshape, startPoint.X, startPoint.Y);
            canvas.Children.Add(newshape);
        }

        public abstract InternalShape GetShape();

        public void ExecuteMouseMove(InternalCanvas canvas)
        {
            var pos = Mouse.GetPosition(canvas);

            var x = Math.Min(pos.X, startPoint.X);
            var y = Math.Min(pos.Y, startPoint.Y);

            var w = Math.Max(pos.X, startPoint.X) - x;
            var h = Math.Max(pos.Y, startPoint.Y) - y;

            newshape.Width = w;
            newshape.Height = h;

            canvas.SetNewShape(newshape, x, y);
        }

        public void ExecuteMouseUp(InternalCanvas canvas)
        {
            newshape.Location = canvas.GetPositionOfShapeInCanvas(newshape);
            canvas.AddNewShape(newshape);
            IsCompleted = true;
        }

        public void Undo(InternalCanvas canvas)
        {
            canvas.Children.Remove(newshape);
            canvas.Shapes.Remove(newshape);
        }

        public void Execute(InternalCanvas canvas)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed && newshape == null)
            {
                ExecuteMouseDown(canvas);
            }
            else if (Mouse.LeftButton == MouseButtonState.Pressed && newshape != null)
            {
                ExecuteMouseMove(canvas);
            }
            else if (Mouse.LeftButton == MouseButtonState.Released && newshape != null)
            {
                ExecuteMouseUp(canvas);
            }
        }
    }
}
