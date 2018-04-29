namespace TeamBuilder.App.Core.Commands
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using TeamBuilder.App.Utilities;
	using TeamBuilder.Data;
	using TeamBuilder.Models;


	public class RegisterUserCommand : Command
	{
		public override string Execute(IList<string> args)
		{

			Check.CheckLength(7, args.ToArray());

			string username = args[0];
			if(username.Length < Constants.MinUsernameLength ||
				username.Length > Constants.MaxUsernameLength)
			{
				throw new ArgumentException
					(string.Format(Constants.ErrorMessages.UsernameNotValid, username));
			}

			string password = args[1];
			if(!password.Any(char.IsDigit) || !password.Any(char.IsUpper))
			{
				throw new ArgumentException
					(string.Format(Constants.ErrorMessages.PasswordNotValid, password));
			}

			string repeatPassword = args[2];
			if (password != repeatPassword) 
			{
				throw new InvalidOperationException(Constants.ErrorMessages.PasswordDoesNotMatch);
			}

			string firstName = args[3];
			string lastName = args[4];

			int age;
			bool isNumber = int.TryParse(args[5], out age);
			if (!isNumber || age <= 0)
			{
				throw new ArgumentException(Constants.ErrorMessages.AgeNotValid);
			}

			Gender gender;
			bool isGenderValid = Enum.TryParse(typeof(Gender), args[6], out object result);
			if(!isGenderValid)
			{
				throw new ArgumentException(Constants.ErrorMessages.GenderNotValid);
			}
			gender = (Gender)result;

			if(CommandHelper.IsUserExisting(username))
			{
				throw new InvalidOperationException(string.Format(Constants.ErrorMessages.UsernameIsTaken,
					username));
			}

			this.RegisterUser(username, password, firstName, lastName, age, gender);

			return $"User {username} was registered successfully!";
		}

		private void RegisterUser(string username, string password, string firstName, string lastName, int age, Gender gender)
		{
			using (TeamBuilderContext context = new TeamBuilderContext())
			{
				User user = new User(username, password, firstName, lastName, age, gender);

				context.Users.Add(user);
				context.SaveChanges();
			}
		}
	}
}
