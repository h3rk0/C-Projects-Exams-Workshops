using System.Linq;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Utilities
{
    public static class CommandHelper
    {
		public static bool IsTeamExisting(string teamName)
		{
			using (TeamBuilderContext context = new TeamBuilderContext())
			{
				return context.Teams.Any(t => t.Name == teamName);
			}
		}

		public static bool IsUserExisting(string userName)
		{
			using (TeamBuilderContext context = new TeamBuilderContext())
			{
				return context.Users
					.Any(u => u.Username == userName
					&& u.IsDeleted == false);
			}
		}

		public static bool IsInviteExisting(string teamName, User user)
		{
			using (TeamBuilderContext context = new TeamBuilderContext())
			{
				return context
					.Invitations
					.Any(i => i.Team.Name == teamName && i.InvitedUserId == user.Id && i.IsActive);
			}
		}

		public static bool IsMemberOfTeam(string teamName, string userName)
		{
			using (TeamBuilderContext context = new TeamBuilderContext())
			{
				return context.Teams
					.Single(t => t.Name == teamName)
					.UserTeams.Any(ut => ut.User.Username == userName);
			}
		}

		public static bool IsEventExisting(string eventName)
		{
			using (TeamBuilderContext context = new TeamBuilderContext())
			{
				return context.Events
					.Any(e => e.Name == eventName);
			}
		}

		public static bool IsUserCreatorOfEvent(string eventName,User user)
		{
			using (TeamBuilderContext context = new TeamBuilderContext())
			{
				Event @event = context.Events
					.SingleOrDefault(e => e.Name == eventName 
					&& e.Creator.Username == user.Username);

				if(@event == null)
				{
					return false;
				}

				return true;
			}
		}

		public static bool IsUserCreatorOfTeam(string teamName,User user)
		{
			using (TeamBuilderContext context = new TeamBuilderContext())
			{
				Team team = context.Teams
					.SingleOrDefault(t => t.Name == teamName
					&& t.Creator.Username == user.Username);

				if(team == null)
				{
					return false;
				}

				return true;
			}
		}
	}
}
