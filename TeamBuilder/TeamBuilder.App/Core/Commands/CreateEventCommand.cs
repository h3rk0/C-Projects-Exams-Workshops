using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Commands
{
	public class CreateEventCommand : Command
	{
		public override string Execute(IList<string> args)
		{
			//CreateEvent CrackIT ITHardware 22/10/2013 12:00 22/10/2013 22:00
			//CreateEvent CrackIT2 ITHardware2 22/10/2013 22:00 22/10/2013 12:00
			Check.CheckLength(6, args.ToArray());
			string eventName = args[0];
			string description = args[1];
			string startDateString = $"{args[2]} {args[3]}";
			string endDateString = $"{args[4]} {args[5]}";

			//Please insert the dates in format: [dd/MM/yyyy HH:mm]!
			bool startDateValid = DateTime.TryParseExact(startDateString,
				"dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture,
				DateTimeStyles.None,  out DateTime firstDate);

			bool endDateValid = DateTime.TryParseExact(endDateString,
				"dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture,
				DateTimeStyles.None, out DateTime secondDate);

			if (!startDateValid || !endDateValid)
			{
				throw new ArgumentException($"Please insert the dates in format: [dd/MM/yyyy HH:mm]!");

			}

			if(firstDate>secondDate)
			{
				throw new ArgumentException("Start date should be before end date.");
			}

			AuthenticationManager.Authorize();

			User currentUser = AuthenticationManager.GetCurrentUser();

			using (TeamBuilderContext context = new TeamBuilderContext())
			{

				User creator = context.Users.Single(u => u.Username == currentUser.Username);
				Event @event = new Event(eventName, description, firstDate, secondDate, creator);
				context.Events.Add(@event);
				context.SaveChanges();
			}


				return $"Event {eventName} was created successfully!";
		}
	}
}
