
namespace Repository.Logic.Models
{
    public sealed class Student : ModelObject, ICloneable
    {
        public Student()
        {
        }
        public string Number { get; set; } = string.Empty;
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;

        public string FullName => $"{Lastname} {Firstname}";

        public object Clone()
        {
            return new Student
            {
                Id = Id,
                Number = Number,
                Firstname = Firstname,
                Lastname = Lastname,
            };
        }
    }
}
