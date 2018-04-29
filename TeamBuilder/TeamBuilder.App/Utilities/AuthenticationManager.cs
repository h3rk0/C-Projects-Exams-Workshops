namespace TeamBuilder.App.Utilities
{
	using System;
	
	using TeamBuilder.Models;
	
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
