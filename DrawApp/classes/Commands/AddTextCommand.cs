using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawApp.classes.Commands
{
    class AddTextCommand : Command
    {
        public bool IsCompleted { get; set; }
        public TextDecorator Text { get; set; }
        public ShapeComponent OldShapeComponent { get; set; }

        public AddTextCommand()
        {
            IsCompleted = false;
        }

        public void Execute(InternalCanvas canvas)
        {
            foreach (ShapeComponent canvasshape in canvas.Shapes)
            {
                if (canvasshape.IsMouseOver)
                {
                    OldShapeComponent = canvasshape;
                    if (!(canvasshape is TextDecorator))
                    {
                        AddTextDecorator(canvas, canvasshape);
                    }
                    else if (canvasshape is TextDecorator textshape)
                    {
                        ModifyTextDecorator(textshape);
                    }
                    IsCompleted = true;
                    break;
                }
            }
        }

        private static void ModifyTextDecorator(TextDecorator textshape)
        {
            InputDialog inputDialog = new InputDialog(textshape.Texts);
            if (inputDialog.ShowDialog() == true)
            {
                textshape.Texts = inputDialog.Texts;
            }
        }

        private void AddTextDecorator(InternalCanvas canvas, ShapeComponent canvasshape)
        {
            InputDialog inputDialog = new InputDialog();
            if (inputDialog.ShowDialog() == true)
            {
                Text = new TextDecorator(canvasshape, inputDialog.Texts);

                canvas.Shapes.Add(Text);
                canvas.Children.Add(Text);
                canvas.SetNewShape(Text, Text.Location.X, Text.Location.Y);

                canvas.Shapes.Remove(canvasshape);
                canvas.Children.Remove(canvasshape);
            }
        }


        public void Undo(InternalCanvas canvas)
        {
            if (!(OldShapeComponent is TextDecorator))
            {
                canvas.Shapes.Remove(Text);
                canvas.Children.Remove(Text);
                canvas.Shapes.Add(OldShapeComponent);
                canvas.Children.Add(OldShapeComponent);
            }
            else if (OldShapeComponent is TextDecorator textshape)
            {
                throw new NotImplementedException();
            }
        }
    }
}
