using SoftJail.Data;
using SoftJail.Data.Models;
using System;

namespace SoftJail.DataProcessor
{
    public class Bonus
    {
        public static string ReleasePrisoner(SoftJailDbContext context, int prisonerId)
        {
            Prisoner prisoner = context.Prisoners.Find(prisonerId);
            if (prisoner.ReleaseDate == null)
            {
                prisoner.ReleaseDate = DateTime.Now.Date;
                prisoner.CellId = null;
                context.SaveChanges();

                return $"Prisoner {prisoner.FullName} is sentenced to life";
            }
            else
            {
                prisoner.ReleaseDate = DateTime.Now.Date;
                prisoner.CellId = null;
                context.SaveChanges();

                return $"Prisoner {prisoner.FullName} released";
            }
        }
    }
}