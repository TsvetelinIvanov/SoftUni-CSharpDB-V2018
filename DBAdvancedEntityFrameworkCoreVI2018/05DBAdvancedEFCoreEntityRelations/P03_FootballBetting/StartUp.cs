using P03_FootballBetting.Data;

namespace P03_FootballBetting
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (FootballBettingContext contex = new FootballBettingContext())
            {
                contex.Database.EnsureDeleted();
                contex.Database.EnsureCreated();
                
                //contex.Database.Migrate();
            }
        }
    }
}
