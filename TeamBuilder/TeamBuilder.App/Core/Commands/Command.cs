using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.App.Core.Commands.Contracts;

namespace TeamBuilder.App.Core.Commands
{
	public abstract class Command : ICommand
	{
		public abstract string Execute(IList<string> args);
	}
}
