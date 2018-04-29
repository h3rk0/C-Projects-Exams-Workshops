namespace TeamBuilder.App.Core.Commands
{
	using System.Collections.Generic;
	using System.Linq;

	using TeamBuilder.App.Utilities;
	
	public class ExitCommand : Command
	{
		public override string Execute(IList<string> args)
		{
			Check.CheckLength(0, args.ToArray());

			return "Bye!";
		}
	}
}
