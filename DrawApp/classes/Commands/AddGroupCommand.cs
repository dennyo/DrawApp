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
                //if (canvasshape is ShapeGroup shapegroup)
                //{
                //    SearchShapeGroup(canvas, shapegroup);
                //}
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

        //public void SearchShapeGroup(InternalCanvas canvas, ShapeGroup shapegroup)
        //{
        //    foreach (ShapeComponent canvasshape in shapegroup.Shapes)
        //    {
        //        if (canvasshape is ShapeGroup shapegroup2)
        //        {
        //            SearchShapeGroup(canvas, shapegroup2);
        //        }
        //        if (canvasshape is InternalShape shape && shape.IsMouseOver)
        //        {
        //            if (group.Contains(shape))
        //            {
        //                group.Remove(shape);
        //                shape.Stroke = Brushes.LightBlue;
        //            }
        //            else
        //            {
        //                group.Add(shape);

        //            }
        //        }
        //    }
        //}

        public void AddGroupToCanvas(InternalCanvas canvas)
        {
            //oldShapeComponents = canvas.Shapes;
            RemoveShapeComponent(canvas, group.Shapes);
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

        public void RemoveShapeComponent(InternalCanvas canvas, List<ShapeComponent> shapeComponents)
        {
            foreach (ShapeComponent shapeComponent in shapeComponents)
            {
                if (shapeComponent is InternalShape && canvas.Shapes.Contains(shapeComponent))
                {
                    canvas.Shapes.Remove(shapeComponent);
                }
                else if (shapeComponent is ShapeGroup group)
                {
                    RemoveShapeComponent(canvas, group.Shapes);
                }
                else if (shapeComponent is TextDecorator && canvas.Shapes.Contains(shapeComponent))
                {
                    canvas.Shapes.Remove(shapeComponent);
                }
            }
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
    }
}
