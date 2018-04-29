namespace TeamBuilder.App.Core.Commands
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using TeamBuilder.App.Utilities;
	using TeamBuilder.Data;
	using TeamBuilder.Models;


	public class AddTeamToCommand : Command
	{
		public override string Execute(IList<string> args)
		{
			Check.CheckLength(2, args.ToArray());
			string eventName = args[0];
			string teamName = args[1];

			// if user is logged in
			AuthenticationManager.Authorize();
			User currentLoggedUser = AuthenticationManager.GetCurrentUser();

			// if teams is existing
			if(!CommandHelper.IsTeamExisting(teamName))
			{
				throw new ArgumentException(string.Format(Constants.ErrorMessages.TeamNotFound, teamName));
			}

			// if event is existing
			if(!CommandHelper.IsEventExisting(eventName))
			{
				throw new ArgumentException(string.Format(Constants.ErrorMessages.EventNotFound, eventName));
			}

			// if user is not creator of event
			if(!CommandHelper.IsUserCreatorOfEvent(eventName,currentLoggedUser))
			{
				throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
			}

			// is team already part of event
			if(CommandHelper.IsTeamPartOfEvent(teamName,eventName))
			{
				throw new InvalidOperationException(Constants.ErrorMessages.CannotAddSameTeamTwice);
			}

			// creating and adding new eventTeam object in mapping table
			using (TeamBuilderContext context = new TeamBuilderContext())
			{
				Team team = context.Teams.Single(t => t.Name == teamName);
				Event @event = context.Events.Single(e => e.Name == eventName);

				EventTeam eventTeam = new EventTeam
				{
					Team = team,
					TeamId = team.Id,
					Event = @event,
					EventId = @event.Id
				};

				context.EventTeams.Add(eventTeam);
				context.SaveChanges();
			}

			return $"Team {teamName} added for {eventName}";
		}
	}
}
