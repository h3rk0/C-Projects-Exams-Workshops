using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsAndCodeWizards.Contracts
{
    public interface ICommand
    {
		string Execute(string[] args);
    }
}
