using System.ComponentModel;

namespace DrawAppViewModel
{
    internal class Test : INotifyPropertyChanged
    {
        public Test(string name)
        {
            this.Name = name;
        }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}