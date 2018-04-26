using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Commands.Contracts
{
	public class LoginCommand : Command
	{
		public override string Execute(IList<string> args)
		{
			Check.CheckLength(2, args.ToArray());
			string username = args[0];
			string password = args[1];

			if(AuthenticationManager.IsAuthenticated())
			{
				throw new InvalidOperationException(Constants.ErrorMessages.LogoutFirst);
			}

			User user = this.GetUserByCredentials(username, password);

			if(user == null )
			{
				throw new ArgumentException(Constants.ErrorMessages.UserOrPasswordIsInvalid);
			}

			AuthenticationManager.Login(user);
			return $"User {user.Username} successfully logged in!";
		}

		private User GetUserByCredentials(string username, string password)
		{
			using (TeamBuilderContext context = new TeamBuilderContext())
			{
				User user = context.Users.FirstOrDefault(u => u.Username == username &&
				u.Password == password && !u.IsDeleted);
				return user;
			}
		}
	}
}
