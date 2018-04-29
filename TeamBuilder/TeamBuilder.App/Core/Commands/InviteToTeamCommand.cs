namespace TeamBuilder.App.Core.Commands
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using TeamBuilder.App.Utilities;
	using TeamBuilder.Data;
	using TeamBuilder.Models;


	public class InviteToTeamCommand : Command
	{
		
		public override string Execute(IList<string> args)
		{
			Check.CheckLength(2, args.ToArray());
			string[] commandArgs = args.ToArray();
			string teamName = args[0];
			string username = args[1];

			// currently not logged in
			AuthenticationManager.Authorize();

			// current logged user
			User currentUser = AuthenticationManager.GetCurrentUser();

			Team team;
			User user;
			using (TeamBuilderContext context = new TeamBuilderContext())
			{
				team = context.Teams.SingleOrDefault(t => t.Name == teamName);
				user = context.Users.SingleOrDefault(u => u.Username == username);
			}

			// team or user doesnt exists
			if (team == null || user == null)
			{
				throw new ArgumentException($"Team or user does not exist!");
			}
			
			if (!CommandHelper.IsUserCreatorOfTeam(team.Name, currentUser) ||
						CommandHelper.IsMemberOfTeam(team.Name, currentUser.Username) ||
						CommandHelper.IsMemberOfTeam(team.Name, user.Username))
			{
				throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
			}

			// if invite is already sent
			if (CommandHelper.IsInviteExisting(teamName, user))
			{
				throw new InvalidOperationException(Constants.ErrorMessages.InviteIsAlreadySent);
			}



			using (TeamBuilderContext context = new TeamBuilderContext())
			{
				Invitation invitation = new Invitation()
				{
					InvitedUserId = user.Id,
					TeamId=team.Id
				};

				context.Invitations.Add(invitation);
				context.SaveChanges();
			}

			return $"Team {teamName} invited {username}!";
		}

		
	}
}
