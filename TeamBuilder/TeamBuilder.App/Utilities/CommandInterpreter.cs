namespace TeamBuilder.App.Utilities
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	using TeamBuilder.App.Core.Commands.Contracts;
	
	public class CommandInterpreter
    {
		public string ExecuteCommand(string input)
		{
			
			List<string> inputList = input.Split().ToList();
			var command = inputList[0];
			inputList = inputList.Skip(1).ToList();

			
			var commandName = command + "Command";
			Type commandType = Assembly
							  .GetExecutingAssembly()
							  .GetTypes()
							  .FirstOrDefault(t =>
								t.Name == commandName);

			// if command is not supported 
			if(commandName == null || !typeof(ICommand).IsAssignableFrom(commandType))
			{
				throw new NotSupportedException($"Command {command} not supported!");
			}
			
			// Create instance 
			ICommand commandToActivate = (ICommand)Activator.CreateInstance(commandType);

			// Invoke Command Execute
			var result = commandToActivate.Execute(inputList);

			return result;
		}
    }
}
