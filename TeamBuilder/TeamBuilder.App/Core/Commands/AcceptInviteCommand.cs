namespace TeamBuilder.App.Core.Commands
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using TeamBuilder.App.Utilities;
	using TeamBuilder.Data;
	using TeamBuilder.Models;
	
	public class AcceptInviteCommand : Command
	{
		public override string Execute(IList<string> args)
		{
			Check.CheckLength(1, args.ToArray());
			string teamName = args[0];

			// if logged
			AuthenticationManager.Authorize();
			User currentUser = AuthenticationManager.GetCurrentUser();

			// if team exists
			if(!CommandHelper.IsTeamExisting(teamName))
			{
				throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound,teamName));
			}

			// if invite not found
			if(!CommandHelper.IsInviteExisting(teamName,currentUser))
			{
				throw new ArgumentException(string.Format(Constants.ErrorMessages.InviteNotFound));
			}

			// adding user to team
			using (TeamBuilderContext context = new TeamBuilderContext())
			{

				Invitation invitation = context
					.Invitations
					.First(i => i.InvitedUser == currentUser &&
					i.Team.Name == teamName);

				invitation.IsActive = false;

				Team team = context.Teams.First(t => t.Name == teamName);
				User user = context.Users.First(u => u.Username == currentUser.Username);
				UserTeam userTeam = new UserTeam
				{
					UserId = currentUser.Id,
					TeamId = team.Id
				};
				team.UserTeams.Add(userTeam);
				user.UserTeams.Add(userTeam);
				//context.UserTeams.Add(userTeam);
				context.SaveChanges();
			}

			return $"User {currentUser.Username} joined team {teamName}";
		}
	}
}
