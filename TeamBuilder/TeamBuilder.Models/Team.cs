namespace TeamBuilder.Models
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	
	public class Team
    {
		public Team()
		{
			this.UsersInvitations = new List<Invitation>();
			this.UserTeams = new List<UserTeam>();
			this.EventTeams = new List<EventTeam>();
		}
		public Team(string name,string acronym,User creator,string description = null)
			:this()
		{
			this.Name = name;
			this.Acronym = acronym;
			this.Description = description;
			this.Creator = creator;
		}
		//CreateTeam &lt;name&gt; &lt;acronym&gt; &lt;description&gt;
		public int Id { get; set; }

		[MaxLength(25)]
		public string Name { get; set; }

		[MaxLength(25)]
		public string Description { get; set; }

		[StringLength(3,MinimumLength = 3)]
		public string Acronym { get; set; }

		public int CreatorId { get; set; }
		public User Creator { get; set; }

		public ICollection<Invitation> UsersInvitations { get; set; }

		public ICollection<UserTeam> UserTeams { get; set; }

		public ICollection<EventTeam> EventTeams { get; set; }

		//creator, members, events
	}
}
