using Repository.Logic.Models;

namespace Repository.Logic.Repos
{
    public class PersonalRepository : Repository<Models.Person>
    {
        public PersonalRepository()
            : base()
        {

        }
        public override Person Create()
        {
            return new Person();
        }
    }
}
