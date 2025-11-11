using MotherloadDB.Data;

namespace MotherloadDB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new AppDbContext();

            var dbService = new DbService(context);

        }
    }
}
