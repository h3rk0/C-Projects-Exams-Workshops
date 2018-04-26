using System.Collections.Generic;
using System.Linq;
using TeamBuilder.App.Utilities;
using TeamBuilder.Models;

namespace TeamBuilder.App.Core.Commands
{
	public class LogoutCommand : Command
	{
		public override string Execute(IList<string> args)
		{
			Check.CheckLength(0, args.ToArray());
			AuthenticationManager.Authorize();
			User currentUser = AuthenticationManager.GetCurrentUser();
			AuthenticationManager.Logout();
			return $"User {currentUser.Username} successfully logged out!";
		}
	}
}
