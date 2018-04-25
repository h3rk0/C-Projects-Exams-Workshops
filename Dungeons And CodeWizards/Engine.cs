using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonsAndCodeWizards
{
	public class Engine : IRunnable
	{
		private DungeonMaster dungeonMaster;

		public Engine(DungeonMaster dungeonMaster)
		{
			this.dungeonMaster = dungeonMaster;
		}

		public void Run()
		{
			StringBuilder sb = new StringBuilder();
			while (true)
			{
				string input = Console.ReadLine();

				if (string.IsNullOrWhiteSpace(input))
				{
					Console.WriteLine($"Final stats:");
					Console.WriteLine(dungeonMaster.GetStats());
					return;
				}

				string[] arguments = input.Split();
				string[] argumentsForPass = arguments.Skip(1).ToArray();
				string command = arguments[0];

				try
				{
					switch (command)
					{

						case "JoinParty":
							Console.WriteLine(dungeonMaster.JoinParty(argumentsForPass));
							break;
						case "AddItemToPool":
							Console.WriteLine(dungeonMaster.AddItemToPool(argumentsForPass));
							break;
						case "PickUpItem":
							Console.WriteLine(dungeonMaster.PickUpItem(argumentsForPass));
							break;
						case "UseItem":
							Console.WriteLine(dungeonMaster.UseItem(argumentsForPass));
							break;
						case "UseItemOn":
							Console.WriteLine(dungeonMaster.UseItemOn(argumentsForPass));
							break;
						case "GiveCharacterItem":
							Console.WriteLine(dungeonMaster.GiveCharacterItem(argumentsForPass));
							break;
						case "GetStats":
							Console.WriteLine(dungeonMaster.GetStats());
							break;
						case "Attack":
							Console.WriteLine(dungeonMaster.Attack(argumentsForPass));
							break;
						case "Heal":
							Console.WriteLine(dungeonMaster.Heal(argumentsForPass));
							break;
						case "EndTurn":
							Console.WriteLine(dungeonMaster.EndTurn(argumentsForPass));
							if (dungeonMaster.IsGameOver() == true)
							{
								Console.WriteLine($"Final stats:");
								Console.WriteLine(dungeonMaster.GetStats());
								return;
							}
							break;

					}
				}
				catch (Exception e)
				{
					if (e is ArgumentException)
					{
						Console.WriteLine($"Parameter Error: {e.Message}");
					}
					else if (e is InvalidOperationException)
					{
						Console.WriteLine($"Invalid Operation: {e.Message}");
					}
				}

			}
		}
	}
}
