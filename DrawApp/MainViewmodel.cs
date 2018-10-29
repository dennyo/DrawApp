using DrawApp;
using DrawApp.classes;
using DrawApp.classes.Commands;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DrawAppViewModel
{
    class MainViewmodel
    {
        public Test Coordinates
        {
            get;
            set;
        }
        public InternalCanvas Canvas
        {
            get;
        }

        public Actions Actions
        {
            get; set;
        }

        public List<ShapeComponent> SelectedShapes
        {
            get; set;
        }

        public CurrentAction Setting
        {
            get;
            set;
        }
        #region Commands

        public ICommand CircleCommand
        {
            get;
            internal set;
        }

        public ICommand RectangleCommand
        {
            get;
            internal set;
        }

        public ICommand SelectCommand
        {
            get;
            internal set;
        }

        public ICommand UndoCommand
        {
            get;
            internal set;
        }

        public ICommand MouseDownCommand
        {
            get;
            internal set;
        }
        public ICommand MouseUpCommand
        {
            get;
            internal set;
        }
        public ICommand MouseMoveCommand
        {
            get;
            internal set;
        }

        public ICommand ResizeCommand
        {
            get;
            internal set;
        }

        public ICommand LoadCommand
        {
            get;
            internal set;
        }

        public ICommand SaveCommand
        {
            get;
            internal set;
        }

        public ICommand GroupCommand
        {
            get;
            internal set;
        }

        public ICommand TextCommand
        {
            get;
            internal set;
        }

        #endregion

        public MainViewmodel()
        {
            Coordinates = new Test("car");
            Actions = new Actions();
            Setting = CurrentAction.Selection;
            Canvas = new InternalCanvas();
            SelectedShapes = new List<ShapeComponent>();
            RectangleCommand = new RelayCommand(SetRectangleSetting);
            CircleCommand = new RelayCommand(SetCircleSetting);
            SelectCommand = new RelayCommand(SetSelectSetting);
            MouseDownCommand = new RelayCommand(MouseDown);
            MouseUpCommand = new RelayCommand(MouseUp);
            MouseMoveCommand = new RelayCommand(MouseMove);
            SaveCommand = new RelayCommand(SaveFile);
            LoadCommand = new RelayCommand(LoadFile);
            UndoCommand = new RelayCommand(Undo);
            GroupCommand = new RelayCommand(SetGroupSetting);
            TextCommand = new RelayCommand(SetTextSetting);
        }

        private void SetTextSetting(object obj)
        {
            Setting = CurrentAction.Text;
        }

        private void SetGroupSetting(object obj)
        {
            Setting = CurrentAction.Group;
        }

        private void Undo(object obj)
        {
            Actions.Undo(Canvas);
        }

        private void LoadFile(object obj)
        {
            Actions.Execute(Canvas, new LoadCommand());
        }

        public void SetRectangleSetting(object obj)
        {
            Setting = CurrentAction.Rectangle;
        }

        public void SetCircleSetting(object obj)
        {
            Setting = CurrentAction.Circle;
        }

        public void SetSelectSetting(object obj)
        {
            Setting = CurrentAction.Selection;
        }

        public void MouseDown(object obj)
        {
            if (Setting == CurrentAction.Selection && Mouse.LeftButton == MouseButtonState.Pressed)
            {
                Actions.Execute(Canvas, new MoveShapeCommand());
            }
            else if (Setting == CurrentAction.Rectangle)
            {
                Actions.Execute(Canvas, new AddRectangleCommand());
            }
            else if (Setting == CurrentAction.Circle)
            {
                Actions.Execute(Canvas, new AddCircleCommand());
            }
            else if (Setting == CurrentAction.Group)
            {
                if (Actions.IsLastCommandCompleted())
                    Actions.Execute(Canvas, new AddGroupCommand());
            }
            else if (Setting == CurrentAction.Text)
            {
                Actions.Execute(Canvas, new AddTextCommand());
            }
        }

        public void MouseUp(object obj)
        {
            Actions.Execute(Canvas);
        }

        public void MouseMove(object obj)
        {
            Coordinates.Name = Mouse.GetPosition(Canvas).ToString();
            Actions.Execute(Canvas);
        }

        public void SaveFile(object a)
        {
            Actions.Execute(Canvas, new SaveCommand());
        }

        public bool CanChangeName()
        {
            return true;
        }
    }
}
