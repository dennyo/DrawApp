using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace DrawApp.classes
{
    class AddGroupCommand : Command
    {
        private ShapeGroup group;
        private List<ShapeComponent> oldShapeComponents;

        public bool IsCompleted { get; set; }

        public AddGroupCommand()
        {
            IsCompleted = false;
            oldShapeComponents = new List<ShapeComponent>();
            group = new ShapeGroup();
        }

        public void Execute(InternalCanvas canvas)
        {
            //Key.LeftCtrl == KeyStates.Down
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                ExecuteMouseDown(canvas);
            }
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && group.Shapes.Count() > 0)
            {
                AddGroupToCanvas(canvas);
            }
        }

        public void ExecuteMouseDown(InternalCanvas canvas)
        {
            foreach (ShapeComponent canvasshape in canvas.Shapes)
            {
                if (canvasshape.IsMouseOver)
                {
                    if (group.Contains(canvasshape))
                    {
                        group.Remove(canvasshape);
                        oldShapeComponents.Remove(canvasshape);
                        canvasshape.Stroke = Brushes.LightBlue;
                    }
                    else
                    {
                        group.Add(canvasshape);
                        oldShapeComponents.Add(canvasshape);
                        canvasshape.Stroke = Brushes.Red;
                    }
                }
            }
        }

        public void AddGroupToCanvas(InternalCanvas canvas)
        {
            canvas.Shapes.Add(group);
            canvas.Children.Add(group);
            canvas.SetNewShape(group, group.Location.X, group.Location.Y);
            foreach (ShapeComponent component in oldShapeComponents)
            {
                canvas.Shapes.Remove(component);
                canvas.Children.Remove(component);
            }
            IsCompleted = true;
        }

        public void Undo(InternalCanvas canvas)
        {
            canvas.Shapes.Remove(group);
            canvas.Children.Remove(group);
            foreach (ShapeComponent component in oldShapeComponents)
            {
                canvas.Shapes.Add(component);
                canvas.Children.Add(component);
            }
        }

        public void Redo(InternalCanvas canvas)
        {
            canvas.Shapes.Add(group);
            canvas.Children.Add(group);
            foreach (ShapeComponent component in oldShapeComponents)
            {
                canvas.Shapes.Remove(component);
                canvas.Children.Remove(component);
            }
        }
    }
}
