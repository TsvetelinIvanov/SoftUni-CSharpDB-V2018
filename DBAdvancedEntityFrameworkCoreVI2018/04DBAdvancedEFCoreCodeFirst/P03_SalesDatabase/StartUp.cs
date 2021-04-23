using P03_SalesDatabase.Data;

namespace P03_SalesDatabase
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            using (SalesContext context = new SalesContext())
            {
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();
            }
        }
    }
}