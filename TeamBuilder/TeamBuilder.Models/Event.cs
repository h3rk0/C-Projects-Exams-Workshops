namespace TeamBuilder.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	
	public class Event
    {
		public Event()
		{
			this.ParticipatingEventTeams = new List<EventTeam>();
		}

		public Event(string name,string description,DateTime startDate,DateTime endDate,User creator)
			:this()
		{
			this.Name = name;
			this.Description = description;
			this.StartDate = startDate;
			this.EndDate = endDate;
			this.Creator = creator;
		}

		public int Id { get; set; }

		[MaxLength(25)]
		public string Name { get; set; }


		[MaxLength(250)]
		public string Description { get; set; }

		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }

		public int CreatorId { get; set; }
		public User	Creator { get; set; }

		public ICollection<EventTeam> ParticipatingEventTeams { get; set; }

	}
}
