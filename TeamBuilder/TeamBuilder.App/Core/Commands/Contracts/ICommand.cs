using System;
using System.Collections.Generic;
using System.Text;

namespace TeamBuilder.App.Core.Commands.Contracts
{
    public interface ICommand
    {
		string Execute(IList<string> args);
    }
}
