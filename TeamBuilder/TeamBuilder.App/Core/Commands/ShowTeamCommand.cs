namespace TeamBuilder.App.Core.Commands
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	using TeamBuilder.App.Utilities;
	using TeamBuilder.Data;
	using TeamBuilder.Models;


	public class ShowTeamCommand : Command
	{
		public override string Execute(IList<string> args)
		{
			string teamName = args[0];

			if(!CommandHelper.IsTeamExisting(teamName))
			{
				throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
			}

			// creating stringBuilder for result
			StringBuilder sb = new StringBuilder();
			using (TeamBuilderContext context = new TeamBuilderContext())
			{
				var team = context.Teams.Select(t => new
				{
					t.Name,
					t.Acronym,
					t.UserTeams
				}).Single(t => t.Name == teamName);

				//01/01/2012 12:00
				sb.AppendLine($"{team.Name} {team.Acronym}");
				sb.AppendLine($"Members:");
				foreach (UserTeam userTeam in team.UserTeams)
				{
					User user = context.Users.Single(u => u.Id == userTeam.UserId);
					sb.AppendLine($"-{user.Username}");
				}
			}

			return sb.ToString().Trim();
		}
	}
}
