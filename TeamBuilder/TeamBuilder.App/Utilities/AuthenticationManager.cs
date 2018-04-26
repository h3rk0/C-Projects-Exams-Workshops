using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.App.Core.Commands.Contracts;
using TeamBuilder.Models;

namespace TeamBuilder.App.Utilities
{
	public class AuthenticationManager 
	{
		private static User currentUser;

		public static void Authorize()
		{
			if(currentUser == null)
			{
				throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
			}
		}

		public static User GetCurrentUser()
		{
			Authorize();

			return currentUser;
		}

		public static bool IsAuthenticated()
		{
			return currentUser != null;
		}

		public static void Login(User user)
		{
			currentUser = user;
		}

		public static void Logout()
		{
			Authorize();

			currentUser = null;
		}
	}
}
