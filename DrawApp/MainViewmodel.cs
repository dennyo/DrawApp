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

        public CurrentSetting Setting
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

        public ICommand RedoCommand
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
            Setting = CurrentSetting.Selection;
            Canvas = InternalCanvas.getInstance();
            SelectedShapes = new List<ShapeComponent>();
            RectangleCommand = new RelayCommand(x => SetSetting(CurrentSetting.Rectangle));
            CircleCommand = new RelayCommand(x => SetSetting(CurrentSetting.Circle));
            SelectCommand = new RelayCommand(x => SetSetting(CurrentSetting.Selection));
            GroupCommand = new RelayCommand(x => SetSetting(CurrentSetting.Group));
            TextCommand = new RelayCommand(x => SetSetting(CurrentSetting.Text));
            MouseDownCommand = new RelayCommand(MouseDown); 
            MouseUpCommand = new RelayCommand(MouseUp);
            MouseMoveCommand = new RelayCommand(MouseMove);
            SaveCommand = new RelayCommand(SaveFile);
            LoadCommand = new RelayCommand(LoadFile);
            UndoCommand = new RelayCommand(Undo);
            RedoCommand = new RelayCommand(Redo);
        }

        private void SetSetting(CurrentSetting setting)
        {
            Setting = setting;
        }

        private void Redo(object obj)
        {
            Actions.Redo(Canvas);
        }

        private void Undo(object obj)
        {
            Actions.Undo(Canvas);
        }

        private void LoadFile(object obj)
        {
            Actions.Execute(Canvas, new LoadCommand());
        }

        public void MouseDown(object obj)
        {
            if (Setting == CurrentSetting.Selection && Mouse.LeftButton == MouseButtonState.Pressed)
            {
                Actions.Execute(Canvas, new MoveShapeCommand());
            }
            else if (Setting == CurrentSetting.Rectangle)
            {
                Actions.Execute(Canvas, new AddRectangleCommand());
            }
            else if (Setting == CurrentSetting.Circle)
            {
                Actions.Execute(Canvas, new AddCircleCommand());
            }
            else if (Setting == CurrentSetting.Group)
            {
                if (Actions.IsLastCommandCompleted())
                    Actions.Execute(Canvas, new AddGroupCommand());
            }
            else if (Setting == CurrentSetting.Text)
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
