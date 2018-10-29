using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace DrawApp.classes
{
    class LoadCommand : Command
    {
        public bool IsCompleted { get; set; }
        private List<string> ReadedLines { get; set; }

        public void Execute(InternalCanvas canvas)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            List<ShapeComponent> loadedshapes = new List<ShapeComponent>();
            if (openFileDialog.ShowDialog() == true)
            {
                string fileContent = File.ReadAllText(openFileDialog.FileName);
                ReadedLines = FileContentToList(fileContent);
                while (ReadedLines.Count > 0)
                {
                    List<string> splittedLine = ReadedLines.First().Split(' ').ToList();
                    ReadedLines.Remove(ReadedLines.First());
                    if (splittedLine.First() == "group")
                    {
                        loadedshapes.Add(GetGroup(3));
                    }
                    else if (splittedLine.First() == "ellipse" || splittedLine.First() == "rectangle")
                    {
                        InternalShape loadedshape = GetShape(0, splittedLine);
                        loadedshapes.Add(loadedshape);
                    }
                }
            }
            canvas.Reset();
            foreach (ShapeComponent loadedshape in loadedshapes)
            {
                canvas.AddShape(loadedshape);
            }
            IsCompleted = true;
        }

        private static InternalShape GetShape(int newspacecount, List<string> splittedLine)
        {
            InternalShape loadedshape;
            if (splittedLine[newspacecount] == "ellipse")
            {
                loadedshape = new InternalShape(InternalEllipse.getInstance());
                //loadedshape = new InternalEllipse();
                loadedshape.Location = new Point(Convert.ToDouble(splittedLine[newspacecount + 1]), Convert.ToDouble(splittedLine[newspacecount + 2]));
                loadedshape.Width = Convert.ToDouble(splittedLine[newspacecount + 3]);
                loadedshape.Height = Convert.ToDouble(splittedLine[newspacecount + 4]);
                return loadedshape;
            }
            else if (splittedLine[newspacecount] == "rectangle")
            {
                loadedshape = new InternalShape(InternalRectangle.getInstance());
                //loadedshape = new InternalRectangle();
                loadedshape.Location = new Point(Convert.ToDouble(splittedLine[newspacecount + 1]), Convert.ToDouble(splittedLine[newspacecount + 2]));
                loadedshape.Width = Convert.ToDouble(splittedLine[newspacecount + 3]);
                loadedshape.Height = Convert.ToDouble(splittedLine[newspacecount + 4]);
                return loadedshape;
            }
            //loadedshape.Location = new Point(Convert.ToDouble(splittedLine[newspacecount + 1]), Convert.ToDouble(splittedLine[newspacecount + 2]));
            //loadedshape.Width = Convert.ToDouble(splittedLine[newspacecount + 3]);
            //loadedshape.Height = Convert.ToDouble(splittedLine[newspacecount + 4]);
            return null;
        }

        public ShapeGroup GetGroup(int originalspacecount)
        {
            ShapeGroup newgroup = new ShapeGroup();
            int newspacecount = originalspacecount;
            while (originalspacecount <= newspacecount && ReadedLines.Count > 0)
            {
                List<string> splittedLine = ReadedLines.First().Split(' ').ToList();
                ReadedLines.Remove(ReadedLines.First());
                if (splittedLine[newspacecount] == "group")
                {
                    newgroup.Add(GetGroup(newspacecount + 3));
                }
                else if (splittedLine[newspacecount] == "ellipse" || splittedLine[newspacecount] == "rectangle")
                {
                    InternalShape loadedshape = GetShape(newspacecount, splittedLine);
                    newgroup.Add(loadedshape);
                }
                if (ReadedLines.Count > 0)
                    newspacecount = CheckAmountOfSpaces(ReadedLines.First());
            }
            return newgroup;
        }

        public List<string> FileContentToList(string fileContent)
        {
            StringReader stringReader = new StringReader(fileContent);
            List<string> result = new List<string>();
            string line;
            while ((line = stringReader.ReadLine()) != null)
            {
                result.Add(line);
            }
            return result;
        }

        public int CheckAmountOfSpaces(string line)
        {
            int counter = 0;
            foreach (char character in line)
            {
                if (char.IsWhiteSpace(character))
                    counter++;
                else
                    break;
            }
            return counter;
        }

        public void Undo(InternalCanvas canvas)
        {
            throw new NotImplementedException();
        }
    }
}
