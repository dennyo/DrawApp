using DrawApp.classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawApp.classes
{
    class SaveCommand : IOCommand
    {
        public override void Execute(InternalCanvas canvas)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt|C# file (*.cs)|*.cs";
            if (saveFileDialog.ShowDialog() == true)
            {
                SaveVisitor save = new SaveVisitor();
                foreach (ShapeComponent shape in canvas.Shapes)
                {
                    shape.Save(save);
                }
                File.WriteAllText(saveFileDialog.FileName, save.stringBuilder.ToString());
            }
            IsCompleted = true;
        }
    }
}
