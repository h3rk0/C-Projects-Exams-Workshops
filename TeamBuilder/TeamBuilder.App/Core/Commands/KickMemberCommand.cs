namespace TeamBuilder.App.Core.Commands
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using TeamBuilder.App.Utilities;
	using TeamBuilder.Data;
	using TeamBuilder.Models;
	
	public class KickMemberCommand : Command
	{
		public override string Execute(IList<string> args)
		{
			//  KickMember &lt;teamName&gt; &lt;username&gt;

			string teamName = args[0];
			string username = args[1];

			// if not logged in
			AuthenticationManager.Authorize();
			User currentLogged = AuthenticationManager.GetCurrentUser();

			// get team and user to kick
			Team team;
			User user;
			using (TeamBuilderContext context = new TeamBuilderContext())
			{
				team = context.Teams.SingleOrDefault(t => t.Name == teamName);
				user = context.Users.SingleOrDefault(u => u.Username == username);
			}

			// check if team exists
			if (team == null)
			{
				throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
			}

			// check if user exists
			if (user == null)
			{
				throw new ArgumentException(string.Format(Constants.ErrorMessages.UserNotFound, username));
			}

			// if current logged user is not creator of team
			if (!CommandHelper.IsUserCreatorOfTeam(teamName, currentLogged))
			{
				throw new ArgumentException(Constants.ErrorMessages.NotAllowed);
			}

			// if user to kick is creator of team
			if (CommandHelper.IsUserCreatorOfTeam(teamName, user))
			{
				throw new InvalidOperationException(string.Format(Constants.ErrorMessages.CommandNotAllowed, "DisbandTeam"));
			}


			// if member to kick is not part of team
			if (!CommandHelper.IsMemberOfTeam(teamName,username))
			{
				throw new ArgumentException(string.Format(Constants.ErrorMessages.NotPartOfTeam, username, teamName));
			}
			
			// removing user from userTeam mapping table
			using (TeamBuilderContext context = new TeamBuilderContext())
			{

				UserTeam userTeam = context
					.UserTeams
					.Single(ut => ut.UserId == user.Id
					&& ut.TeamId == team.Id);

				context.UserTeams.Remove(userTeam);
				context.SaveChanges();
			}

			return $"User {username} was kicked from {teamName}";
		}
	}
}
