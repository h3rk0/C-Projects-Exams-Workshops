namespace TeamBuilder.App.Core.Commands.Contracts
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using TeamBuilder.App.Utilities;
	using TeamBuilder.Data;
	using TeamBuilder.Models;
	
	public class CreateTeamCommand : Command
	{
		public override string Execute(IList<string> args)
		{

			Check.CheckLength(2, args.ToArray());

			string teamName = args[0];

			bool check = CommandHelper.IsTeamExisting(teamName);
			if(check)
			{
				throw new ArgumentException($"Team {teamName} exists!");
			}

			string acronym = args[1];
			if(acronym.Length!=3)
			{
				throw new ArgumentException($"Acronym {acronym} not valid!");
			}

			string description = null;
			if (args.Count == 2)
			{

			}
			else if (args.Count == 3)
			{
				description = args[2];
			}
			else
			{
				throw new FormatException(Constants.ErrorMessages.InvalidArgumentsCount);
			}
			//CreateTeam(skipped) name acronym description?

			AuthenticationManager.Authorize();
			User currentUser = AuthenticationManager.GetCurrentUser();

			using (TeamBuilderContext context = new TeamBuilderContext())
			{
				User creator = context.Users.Single(u => u.Username == currentUser.Username);
				Team team = new Team(teamName, acronym, creator, description);

				context.Teams.Add(team);
				context.SaveChanges();
			}

				return $"Team {teamName} successfully created!";
		}
	}
}
