using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeamBuilder.App.Utilities;

namespace TeamBuilder.App.Core.Commands
{
	public class ExitCommand : Command
	{
		public override string Execute(IList<string> args)
		{
			Check.CheckLength(0, args.ToArray());

			return "Bye!";
		}
	}
}
