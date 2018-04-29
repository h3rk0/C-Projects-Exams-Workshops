namespace TeamBuilder.Models
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	
	public class User
    {
		public User()
		{
			this.CreatedEvents = new List<Event>();
			this.UserTeams = new List<UserTeam>();
			this.CreatedTeams = new List<Team>();
			this.ReceivedInvitations = new List<Invitation>();
		}
		public User(string username,string password,string firstName,
			string lastName,int age,Gender gender,bool isDeleted = false)
			:this()
		{
			this.Username = username;
			this.Password = password;
			this.FirstName = firstName;
			this.LastName = lastName;
			this.Age = age;
			this.Gender = gender;
			this.IsDeleted = isDeleted;
		}

		public int Id { get; set; }
		
		[StringLength(25,MinimumLength = 3)]
		public string Username { get; set; }

		[MinLength(6)]
		public string Password { get; set; }
		
		[MaxLength(25)]
		public string FirstName { get; set; }

		[MaxLength(25)]
		public string LastName { get; set; }

		public int Age { get; set; }

		public Gender Gender { get; set; }

		public bool IsDeleted { get; set; }

		public ICollection<Event> CreatedEvents { get; set; }
		public ICollection<UserTeam> UserTeams { get; set; }
		public ICollection<Team> CreatedTeams { get; set; }
		public ICollection<Invitation> ReceivedInvitations { get; set; }
	}
}
