namespace StudentManager.ConApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Demo-Data-Generator!");

            var repo = new Repository.Logic.Repos.StudentRepository(@"C:\Users\Gerhard Gehrer\source\repos\StudentManager\Student.json");

            for (int i = 0; i < 15; i++)
            {
                var student = repo.Create();

                student.Number = $"INF{i + 1,4}";
                student.Firstname = $"Vorname{i + 1,2}";
                student.Lastname = $"Nachname{i + 1,2}";

                repo.Add(student);
            }
            repo.SaveChanges();
        }
    }
}