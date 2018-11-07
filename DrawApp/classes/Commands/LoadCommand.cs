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
    class LoadCommand : IOCommand
    {
        private List<string> ReadedLines { get; set; }
        private List<ShapeComponent> LoadedShapes { get; set; }

        public override void Execute(InternalCanvas canvas)
        {
            LoadedShapes = new List<ShapeComponent>();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string fileContent = File.ReadAllText(openFileDialog.FileName);
                ReadedLines = FileContentToList(fileContent);
                LoadedShapes = ReadedLinesToShapes();
            }
            canvas.Reset();
            foreach (ShapeComponent loadedshape in LoadedShapes)
            {
                canvas.AddShape(loadedshape);
            }
            IsCompleted = true;
        }

        private ShapeGroup GetGroup(int spaces)
        {
            ShapeGroup newgroup = new ShapeGroup();
            List<ShapeComponent> loadedgroupshapes = ReadedLinesToShapes(spaces);
            foreach (ShapeComponent groupitem in loadedgroupshapes)
            {
                newgroup.Add(groupitem);
            }
            return newgroup;
        }

        private List<ShapeComponent> ReadedLinesToShapes(int spaces = 0)
        {
            List<string> texts = null; 
            List<ShapeComponent> loadeshapes = new List<ShapeComponent>();
            while (ReadedLines.Count > 0 && CheckAmountOfSpaces(ReadedLines.First()) == spaces)
            {
                List<string> splittedLines = ReadedLines.First().Split(' ').ToList();
                List<string> items = splittedLines.GetRange(spaces, splittedLines.Count() - spaces);
                ReadedLines.Remove(ReadedLines.First());
                ShapeComponent loadedshape = null;
                if (items.First() == "group")
                {
                    loadedshape = GetGroup(spaces + 3);
                }
                else if (items.First() == InternalEllipse.getInstance().GetName())
                {
                    loadedshape = InternalShape.Load(InternalEllipse.getInstance(), items);
                }
                else if (items.First() == InternalRectangle.getInstance().GetName())
                {
                    loadedshape = InternalShape.Load(InternalRectangle.getInstance(), items);
                }
                else if (items.First() == "ornament")
                {
                    if (texts == null)
                    {
                        texts = new List<string>() { "", "", "", "" };
                    }
                    texts[TextDecorator.GetLocation((TextDecorator.TextLocations)Enum.Parse(typeof(TextDecorator.TextLocations), items[1]))] = items[2];
                }
                if (texts != null && loadedshape != null)
                {
                    loadeshapes.Add(new TextDecorator(loadedshape, texts));
                    texts = null;
                }
                else if (loadedshape != null)
                {
                    loadeshapes.Add(loadedshape);
                }
            }
            return loadeshapes;
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
    }
}
