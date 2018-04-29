namespace TeamBuilder.App.Core.Commands
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using TeamBuilder.App.Utilities;
	using TeamBuilder.Data;
	using TeamBuilder.Models;
	
	public class DisbandCommand : Command
	{
		public override string Execute(IList<string> args)
		{
			string teamName = args[0];

			AuthenticationManager.Authorize();
			User currentlyLoggedUser = AuthenticationManager.GetCurrentUser();

			if(!CommandHelper.IsTeamExisting(teamName))
			{
				throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
			}

			if(!CommandHelper.IsUserCreatorOfTeam(teamName,currentlyLoggedUser))
			{
				throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
			}

			using (TeamBuilderContext context = new TeamBuilderContext())
			{
				Team team = context.Teams.Single(t => t.Name == teamName);

				context.Teams.Remove(team);
				context.SaveChanges();
			}

			return $"{teamName} has disbanded!";
		}
	}
}
