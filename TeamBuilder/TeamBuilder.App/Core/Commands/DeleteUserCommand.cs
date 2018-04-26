using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamBuilder.App.Utilities;
using TeamBuilder.Data;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Commands
{
	public class DeleteUserCommand : Command
	{ 
		public override string Execute(IList<string> args)
		{
			Check.CheckLength(0, args.ToArray());
			AuthenticationManager.Authorize();

			User user = AuthenticationManager.GetCurrentUser();

			using (TeamBuilderContext context = new TeamBuilderContext())
			{
				User userToDelete = context.Users.First(u => u.Username == user.Username && u.Password == user.Password);
				userToDelete.IsDeleted = true;
				context.SaveChanges();

				AuthenticationManager.Logout();
			}

			return $"User {user.Username} was deleted successfully!";
		}
	}
}
