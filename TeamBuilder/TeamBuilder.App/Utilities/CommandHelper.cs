namespace TeamBuilder.App.Utilities
{
	using System.Linq;

	using TeamBuilder.Data;
	using TeamBuilder.Models;


	public static class CommandHelper
    {
		public static bool IsTeamExisting(string teamName)
		{
			using (TeamBuilderContext context = new TeamBuilderContext())
			{
				return context.Teams.Any(t => t.Name == teamName);
			}
		}

		public static bool IsTeamPartOfEvent(string teamName,string eventName)
		{
			using (TeamBuilderContext context = new TeamBuilderContext())
			{
				return context.EventTeams.Any(et => et.Event.Name == eventName
				&& et.Team.Name==teamName);
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
				var userTeam = context.UserTeams
					.Any(ut => ut.Team.Name == teamName &&
					ut.User.Username == userName);

				var team = context.Teams
					.Single(t => t.Name == teamName);

				var check = team
					.UserTeams.Any(ut => ut.User.Username == userName);
				return userTeam;
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
