namespace TeamBuilder.App.Core.Commands
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	using TeamBuilder.App.Utilities;
	using TeamBuilder.Data;
	using TeamBuilder.Models;

	public class ShowEventCommand : Command
	{
		public override string Execute(IList<string> args)
		{
			string eventName = args[0];

			// if event exists
			if (!CommandHelper.IsEventExisting(eventName))
			{
				throw new ArgumentException(string.Format(Constants.ErrorMessages.EventNotFound, eventName));
			}

			// creating stringBuilder for result
			StringBuilder sb = new StringBuilder();
			using (TeamBuilderContext context = new TeamBuilderContext())
			{
				var @event = context.Events.Select(e => new
				{
					e.Name,
					e.StartDate,
					e.EndDate,
					e.Description,
					e.ParticipatingEventTeams
				}).Single(e => e.Name == eventName);

				//01/01/2012 12:00
				sb.AppendLine($"{@event.Name} {@event.StartDate.ToString("dd/mm/yyyy hh:mm")} " +
					$"{@event.EndDate.ToString("dd/MM/yyyy HH:mm")} {@event.Description}");
				sb.AppendLine($"Teams:");
				foreach (EventTeam eventTeam in @event.ParticipatingEventTeams)
				{
					Team team = context.Teams.Single(t => t.Id == eventTeam.TeamId);
					sb.AppendLine($"-{team.Name}");
				}
			}

			return sb.ToString().Trim();
		}
	}
}
