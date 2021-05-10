using System.Linq;
using VaporStore.Data;
using VaporStore.Data.Models;

namespace VaporStore.DataProcessor
{
    public static class Bonus
	{
		public static string UpdateEmail(VaporStoreDbContext context, string username, string newEmail)
		{
			if (!context.Users.Any(u => u.Username == username))
            {
                return $"User {username} not found";
            }

            if (context.Users.Any(u => u.Email == newEmail))
            {
                return $"Email {newEmail} is already taken";
            }

            User user = context.Users.FirstOrDefault(u => u.Username == username);
            user.Email = newEmail;
            context.SaveChanges();

            return $"Changed {username}'s email successfully";
		}
	}
}