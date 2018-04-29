using Microsoft.VisualBasic;
using System;
namespace TeamBuilder.App.Core.Commands.Contracts
{
	using System.Collections.Generic;
	using System.Linq;

	using TeamBuilder.App.Utilities;
	using TeamBuilder.Data;
	using TeamBuilder.Models;
	
	public class DeclineInviteCommand : Command
	{
		public override string Execute(IList<string> args)
		{
			Check.CheckLength(1, args.ToArray());
			string teamName = args[0];

			// if logged
			AuthenticationManager.Authorize();
			User currentUser = AuthenticationManager.GetCurrentUser();

			// if team exists
			if (!CommandHelper.IsTeamExisting(teamName))
			{
				throw new ArgumentException(string.Format(Utilities.Constants.ErrorMessages.TeamNotFound, teamName));
			}

			// if invite not found
			if (!CommandHelper.IsInviteExisting(teamName, currentUser))
			{
				throw new ArgumentException(string.Format(Utilities.Constants.ErrorMessages.InviteNotFound,teamName));
			}

			using (TeamBuilderContext context = new TeamBuilderContext())
			{

				Invitation invitation = context
					.Invitations
					.First(i => i.InvitedUser == currentUser &&
					i.Team.Name == teamName);

				context.Invitations.Remove(invitation);
				context.SaveChanges();
				
			}

			return $"Invite from {teamName} declined.";
		}
	}
}
