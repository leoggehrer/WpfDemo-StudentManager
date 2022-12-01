using System.Collections.ObjectModel;
using System.Windows.Input;
using Repository.Logic.Repos;
using Logic = Repository.Logic;

namespace StudentManager.WpfApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private ICommand? addCommand = null;
        private ICommand? editCommand = null;

        public MainViewModel()
        {
        }
        public ObservableCollection<Logic.Models.Student> Students
        {
            get
            {
                var studentRepo = new StudentRepository(StudentFilePath);
                var students = studentRepo.GetAll();

                return new ObservableCollection<Logic.Models.Student>(students);
            }
        }

        public ICommand AddCommand 
        { 
            get
            {
                return RelayCommand.CreateCommand(ref addCommand, (p) => AddStudent());
            }
        }

        public ICommand EditCommand
        {
            get
            {
                return RelayCommand.CreateCommand(ref editCommand, (p) => EditStudent(SelectedItem!.Id), o => SelectedItem != null);
            }
        }
        private void AddStudent()
        {
            var window = new StudentWindow();

            window.ShowDialog();
            OnPropertyChanged(nameof(Students));
        }
        private void EditStudent(int id)
        {
            // ToDo
        }
        private Logic.Models.Student? selectedItem;

        public Logic.Models.Student? SelectedItem
        {
            get { return selectedItem; }
            set 
            { 
                selectedItem = value; 
            }
        }
    }
}
