namespace TeamBuilder.App.Core
{
	using System;

	using TeamBuilder.App.Utilities;
	
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
					/////TRY
					string input = Console.ReadLine();
					string output = this.commandDispatcher.ExecuteCommand(input);

					if (output == "Bye!")
					{
						break;
					}

					Console.WriteLine(output);
					/////CATCH

				}
				catch (Exception e)
				{
					Console.WriteLine(e.GetBaseException().Message);
				}
			}
		}
    }
}
