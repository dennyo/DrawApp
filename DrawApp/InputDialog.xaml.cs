using DrawApp.classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DrawApp
{
    public partial class InputDialog : Window
    {
        public InputDialog()
        {
            InitializeComponent();
        }
        public InputDialog(List<string> texts)
        {
            InitializeComponent();
            InputTop.Text = texts[0];
            InputRight.Text = texts[1];
            InputBottom.Text = texts[2];
            InputLeft.Text = texts[3];
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            //txtAnswer.SelectAll();
            //txtAnswer.Focus();
        }

        public List<string> Texts
        {
            get
            {
                return new List<string>() { InputTop.Text, InputRight.Text, InputBottom.Text, InputLeft.Text };
            }
        }
    }
}
