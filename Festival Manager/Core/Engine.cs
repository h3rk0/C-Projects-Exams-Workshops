
using System;
using System.Linq;
namespace FestivalManager.Core
{
	using System.Reflection;
	using Contracts;
	using Controllers;
	using Controllers.Contracts;
	using FestivalManager.Core.IO;
	using FestivalManager.Entities;
	using FestivalManager.Entities.Contracts;
	using FestivalManager.Entities.Factories;
	using FestivalManager.Entities.Factories.Contracts;
	using FestivalManager.Entities.Instruments;
	using IO.Contracts;

	public class Engine : IEngine
	{
	    private IReader reader;
		private IWriter writer;

		public Engine(IFestivalController festivalController, ISetController setController)
		{
			this.festivalCоntroller = festivalController;
			this.setCоntroller = setController;
			this.instrumentFactory = new InstrumentFactory();
			this.performerFactory = new PerformerFactory();
			this.setFactory = new SetFactory();
			this.songFactory = new SongFactory();
			this.reader = new ConsoleReader();
			this.writer = new ConsoleWriter();
		}
		private IStage stage;
		private IFestivalController festivalCоntroller;
		private ISetController setCоntroller;

		private IInstrumentFactory instrumentFactory;
		private IPerformerFactory performerFactory;
		private ISetFactory setFactory;
		private ISongFactory songFactory;
		
		public void Run()
		{
			while (true) 
			{
				var input = reader.ReadLine();

				if (input == "END")
					break;

				try
				{
					var result = this.ProcessCommand(input);
					this.writer.WriteLine(result);
				}
				catch (Exception ex) 
				{
					this.writer.WriteLine("ERROR: " + ex.Message);
				}
			}

			var end = this.festivalCоntroller.ProduceReport();

			this.writer.WriteLine("Results:");
			this.writer.WriteLine(end);
		}

		public string ProcessCommand(string input)
		{
			string result = string.Empty;
			var args = input.Split();
			var command = args.First();
			//var argsParams = args.Skip(1).ToArray();

			if (command == "RegisterSet")
			{
				result = this.festivalCоntroller.RegisterSet(args);
			}
			else if (command == "SignUpPerformer")
			{
				result = this.festivalCоntroller.SignUpPerformer(args);
			}
			else if (command == "RegisterSong")
			{
				result = this.festivalCоntroller.RegisterSong(args);
			}
			else if (command == "AddSongToSet")
			{
				result = this.festivalCоntroller.AddSongToSet(args);
			}
			else if (command == "AddPerformerToSet")
			{
				result = this.festivalCоntroller.AddPerformerToSet(args);
			}
			else if (command == "RepairInstruments")
			{
				result = this.festivalCоntroller.RepairInstruments(args);
			}
			else if (command == "LetsRock")
			{
				result = this.setCоntroller.PerformSets();
			}
			
			return result;
		}
	}
}