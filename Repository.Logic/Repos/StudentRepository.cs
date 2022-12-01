using Repository.Logic.Models;

namespace Repository.Logic.Repos
{
    public class StudentRepository : Repository<Models.Student>
    {
        public StudentRepository()
            : base()
        {

        }
        public StudentRepository(string filePath)
            : base(filePath)
        {

        }
        public override Student Create()
        {
            return new Student();
        }
    }
}
