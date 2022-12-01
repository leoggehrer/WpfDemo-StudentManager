using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Repository.Logic;
using Repository.Logic.Models;
using Repository.Logic.Repos;

namespace StudentManager.WpfApp.ViewModels
{
    public class StudentViewModel : BaseViewModel
    {
        private ICommand? saveCommand = null;
        private ICommand? closeCommand = null;

        public Action? CloseAction { get; set; }
        public StudentViewModel()
        {
            StudentRepo = new StudentRepository(StudentFilePath);
            Model = StudentRepo.Create();
        }
        private StudentRepository StudentRepo { get; set; }
        private Student Model { get; set; }

        public string Number
        {
            get { return Model.Number; }
            set { Model.Number = value; }
        }
        public string Lastname
        {
            get { return Model.Lastname; }
            set { Model.Lastname = value; }
        }
        public string Firstname
        {
            get { return Model.Firstname; }
            set { Model.Firstname = value; }
        }

        public ICommand SaveCommand
        {
            get
            {
                return RelayCommand.CreateCommand(ref saveCommand, (p) => SaveModel());
            }
        }
        public ICommand CloseCommand
        {
            get
            {
                return RelayCommand.CreateCommand(ref closeCommand, (p) => CloseWindow());
            }
        }

        private void SaveModel()
        {
            if (Model.Id > 0)
            {
                StudentRepo.Update(Model);
            }
            else
            {
                StudentRepo.Add(Model);
            }
            StudentRepo.SaveChanges();
            CloseWindow();
        }

        private void CloseWindow()
        {
            CloseAction?.Invoke();
        }
    }
}
