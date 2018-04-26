using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TeamBuilder.App.Core.Commands.Contracts;

namespace TeamBuilder.App.Utilities
{
    public class CommandInterpreter
    {
		public string ExecuteCommand(string input)
		{
			//string result = string.Empty;
			//string[] inputArgs = input
			//	.Split(new[] { ' ', '\t' },
			//	StringSplitOptions.RemoveEmptyEntries);

			//string commandName = inputArgs.Length > 0 ? inputArgs[0] : string.Empty;
			//inputArgs = inputArgs
			//	.Skip(1)
			//	.ToArray();

			//switch (commandName)
			//{
			//	default:
			//		throw new NotSupportedException($"Command {commandName} not supported!");
			//}
			List<string> inputList = input.Split().ToList();
			var command = inputList[0];
			inputList = inputList.Skip(1).ToList();

			// Invoke Command
			var commandName = command + "Command";
			Type commandType = Assembly
							  .GetExecutingAssembly()
							  .GetTypes()
							  .FirstOrDefault(t =>
								t.Name == commandName);

			if(commandName == null || !typeof(ICommand).IsAssignableFrom(commandType))
			{
				throw new NotSupportedException($"Command {command} not supported!");
			}
			

			ICommand commandToActivate = (ICommand)Activator.CreateInstance(commandType);

			var result = commandToActivate.Execute(inputList);

			return result;
		}
    }
}
