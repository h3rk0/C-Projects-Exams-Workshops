using System;
using System.Collections.Generic;
using System.Text;
using TeamBuilder.App.Utilities;

namespace TeamBuilder.App.Core
{
    public class Engine
    {
		private CommandInterpreter commandDispatcher;
		public Engine(CommandInterpreter commandDispatcher)
		{
			this.commandDispatcher = commandDispatcher;
		}

		public void Run()
		{
			while (true)
			{
				
				try
				{
					string input = Console.ReadLine();
					string output = this.commandDispatcher.ExecuteCommand(input);

					if(output == "Bye!")
					{
						break;
					}

					Console.WriteLine(output);

				}
				catch (Exception e)
				{
					Console.WriteLine(e.GetBaseException().Message);
				}
			}
		}
    }
}
