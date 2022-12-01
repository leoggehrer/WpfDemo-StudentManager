using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Repository.Logic;
using Repository.Logic.Models;
using Repository.Logic.Repos;

namespace StudentManager.WpfApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private ICommand? addCommand = null;
        private ICommand? editCommand = null;

        public MainViewModel()
        {
        }
        public ObservableCollection<Student> Students
        {
            get
            {
                var studentRepo = new StudentRepository(StudentFilePath);
                var students = studentRepo.GetAll();

                return new ObservableCollection<Student>(students);
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
        private Student? selectedItem;

        public Student? SelectedItem
        {
            get { return selectedItem; }
            set 
            { 
                selectedItem = value; 
            }
        }
    }
}
