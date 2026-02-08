// Quick test script pentru verificare Users
using SchoolCatalog.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace SchoolCatalog.Server
{
    public class TestDbConnection
    {
        public static void Main(string[] args)
        {
            var context = new SchoolCatalogContext(
                new DbContextOptionsBuilder<SchoolCatalogContext>()
                    .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SchoolCatalogDb2;Trusted_Connection=True;MultipleActiveResultSets=true")
                    .Options
            );

            var usersCount = context.Users.Count();
            Console.WriteLine($"Total users in DB: {usersCount}");

            if (usersCount == 0)
            {
                Console.WriteLine("?? DATABASE IS EMPTY! Run seeder manually.");
            }
            else
            {
                var users = context.Users.ToList();
                foreach (var user in users)
                {
                    Console.WriteLine($"? User: {user.Email} | Rol: {user.Rol}");
                }
            }
        }
    }
}
