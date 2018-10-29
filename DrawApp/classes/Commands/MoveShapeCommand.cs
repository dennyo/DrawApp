using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using static DrawApp.classes.ShapeComponent;

namespace DrawApp.classes
{
    class MoveShapeCommand : Command
    {
        private Point oldLocation;
        private double oldHeight;
        private double oldWidth;
        private Point startPoint;
        private ShapeComponent shape;
        public bool IsCompleted { get; set; }

        public MoveShapeCommand()
        {
            IsCompleted = false;
        }

        public void Execute(InternalCanvas canvas)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed && shape == null)
            {
                ExecuteMouseDown(canvas);
            }
            else if (Mouse.LeftButton == MouseButtonState.Pressed && shape != null)
            {
                ExecuteMouseMove(canvas);
            }
            else if (Mouse.LeftButton == MouseButtonState.Released && shape != null)
            {
                ExecuteMouseUp(canvas);
            }
        }

        private void ExecuteMouseDown(InternalCanvas canvas)
        {
            startPoint = Mouse.GetPosition(canvas);
            foreach (ShapeComponent canvasshape in canvas.Shapes)
            {
                if (canvasshape.IsMouseOver)
                {
                    canvasshape.MousePosition = canvasshape.GetMousePositionType(startPoint);
                    shape = canvasshape;
                    oldLocation = canvas.GetPositionOfShapeInCanvas(shape);
                    oldHeight = shape.Height;
                    oldWidth = shape.Width;
                    shape.Stroke = Brushes.DarkBlue;
                    break;
                }
            }
        }

        private void ExecuteMouseMove(InternalCanvas canvas)
        {
            // See how much the mouse has moved.
            Point point = Mouse.GetPosition(canvas);
            double offset_x = point.X - startPoint.X;
            double offset_y = point.Y - startPoint.Y;

            // Get the rectangle's current position.
            double new_x, new_y, new_width, new_height;
            GetNewLocationAndSize(shape, offset_x, offset_y, out new_x, out new_y, out new_width, out new_height);

            // Don't use negative width or height.
            if ((new_width > 0) && (new_height > 0))
            {
                // Update the rectangle.
                //SetLeftOfShape(canvas, shape, new_x);
                //SetTopOfShape(canvas, shape, new_y);
                canvas.SetNewShape(shape, SetLeftOfShape(canvas, shape, new_x), SetTopOfShape(canvas, shape, new_y));
                shape.Width = new_width;
                shape.Height = new_height;

                // Save the mouse's new location.
                startPoint = point;
            }
            shape.Location = canvas.GetPositionOfShapeInCanvas(shape);
            //SetMouseCursor(shape);
        }

        private void ExecuteMouseUp(InternalCanvas canvas)
        {
            //shape.Selected = false;
            shape.Stroke = null;
            //Cursor = Cursors.Arrow;
        }

        public void Undo(InternalCanvas canvas)
        {
            shape.Width = oldWidth;
            shape.Height = oldHeight;
            canvas.SetNewShape(shape, oldLocation.X, oldLocation.Y);
        }

        private double SetTopOfShape(InternalCanvas canvas, ShapeComponent shape, double new_y)
        {
            if (new_y < 0)
            {
                return 0;
            }
            else if (new_y > (canvas.ActualHeight - shape.ActualHeight))
            {
                return canvas.ActualHeight - shape.ActualHeight;
            }
            else
            {
                return new_y;
            }
        }

        private double SetLeftOfShape(InternalCanvas canvas, ShapeComponent shape, double new_x)
        {
            if (new_x < 0)
            {
                return 0;
            }
            else if (new_x > (canvas.ActualWidth - shape.ActualWidth))
            {
                return canvas.ActualWidth - shape.ActualWidth;
            }
            else
            {
                return new_x;
            }
        }

        private static void GetNewLocationAndSize(ShapeComponent shape, double offset_x, double offset_y, out double new_x, out double new_y, out double new_width, out double new_height)
        {
            new_x = shape.Location.X;
            new_y = shape.Location.Y;
            new_width = shape.ActualWidth;
            new_height = shape.ActualHeight;

            // Update the rectangle.
            switch (shape.MousePosition)
            {
                case MousePositionType.Body:
                    new_x += offset_x;
                    new_y += offset_y;
                    break;
                case MousePositionType.UL:
                    new_x += offset_x;
                    new_y += offset_y;
                    new_width -= offset_x;
                    new_height -= offset_y;
                    break;
                case MousePositionType.UR:
                    new_y += offset_y;
                    new_width += offset_x;
                    new_height -= offset_y;
                    break;
                case MousePositionType.DR:
                    new_width += offset_x;
                    new_height += offset_y;
                    break;
                case MousePositionType.DL:
                    new_x += offset_x;
                    new_width -= offset_x;
                    new_height += offset_y;
                    break;
                case MousePositionType.L:
                    new_x += offset_x;
                    new_width -= offset_x;
                    break;
                case MousePositionType.R:
                    new_width += offset_x;
                    break;
                case MousePositionType.B:
                    new_height += offset_y;
                    break;
                case MousePositionType.T:
                    new_y += offset_y;
                    new_height -= offset_y;
                    break;
            }
        }

        //private void SetMouseCursor(InternalShape shape)
        //{
        //    // See what cursor we should display.
        //    Cursor desired_cursor = Cursors.Arrow;
        //    switch (shape.MousePosition)
        //    {
        //        case MousePositionType.None:
        //            desired_cursor = Cursors.Arrow;
        //            break;
        //        case MousePositionType.Body:
        //            desired_cursor = Cursors.ScrollAll;
        //            break;
        //        case MousePositionType.UL:
        //        case MousePositionType.DR:
        //            desired_cursor = Cursors.SizeNWSE;
        //            break;
        //        case MousePositionType.DL:
        //        case MousePositionType.UR:
        //            desired_cursor = Cursors.SizeNESW;
        //            break;
        //        case MousePositionType.T:
        //        case MousePositionType.B:
        //            desired_cursor = Cursors.SizeNS;
        //            break;
        //        case MousePositionType.L:
        //        case MousePositionType.R:
        //            desired_cursor = Cursors.SizeWE;
        //            break;
        //    }

        //    // Display the desired cursor.
        //    if (Cursor != desired_cursor) Cursor = desired_cursor;
        //}
    }
}
