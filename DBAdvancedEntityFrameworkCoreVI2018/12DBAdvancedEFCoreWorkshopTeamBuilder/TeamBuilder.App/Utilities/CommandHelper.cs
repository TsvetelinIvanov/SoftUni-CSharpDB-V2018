using System.Linq;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Utilities
{
    public static class CommandHelper
    {
        //If the class isn't staic:

        //private readonly TeamBuilderContext context;

        //public CommandHelper(TeamBuilderContext context)
        //{
        //    this.context = context;
        //}

        //public CommandHelper()
        //{
        //    this.context = new TeamBuilderContext();
        //}

        public static bool IsTeamExisting(string teamName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Teams.Any(t => t.Name == teamName);
            }
        }

        public static bool IsEventExisting(string eventName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Events.Any(e => e.Name == eventName);
            }
        }

        public static bool IsUserExicting(string username)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Users.Any(u => u.Username == username && u.IsDeleted == false);
            }
        }

        public static bool IsInviteExisting(string teamName, User user)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Invitations.Any(i => i.Team.Name == teamName && i.InvitedUserId == user.Id && i.IsActive);
            }
        }

        public static bool IsMemberOfTeam(string teamName, string userName)
        {
            using (TeamBuilderContext context = new TeamBuilderContext())
            {
                return context.Teams.Single(t => t.Name == teamName).Members.Any(ut => ut.User.Username == userName);
            }
        }        

        public static bool IsUseCreatorOfEvent(string eventName, User user)
        {
            return user.CreatedEvents.Any(ce => ce.Name == eventName);
        }

        public static bool IsUserCreatorOfTeam(string teamName, User user)
        {
            return user.CreatedUserTeams.Any(cut => cut.Team.Name == teamName);
        }
    }
}