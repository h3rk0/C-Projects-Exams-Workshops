namespace TeamBuilder.App.Core.Commands
{
	using System.Collections.Generic;

	using TeamBuilder.App.Core.Commands.Contracts;


	public abstract class Command : ICommand
	{
		public abstract string Execute(IList<string> args);
	}
}
