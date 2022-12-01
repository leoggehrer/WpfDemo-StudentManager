using System.ComponentModel;

namespace StudentManager.WpfApp.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected string StudentFilePath { get; set; } = @"C:\Users\Gerhard Gehrer\source\repos\StudentManager\Student.json";
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
