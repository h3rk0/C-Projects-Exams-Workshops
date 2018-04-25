namespace FestivalManager.Core.Controllers
{
	using System;
	using System.Globalization;
	using System.Linq;
	using System.Text;
	using Contracts;
	using Entities.Contracts;
	using FestivalManager.Entities.Factories;
	using FestivalManager.Entities.Factories.Contracts;
	using FestivalManager.Entities.Instruments;

	public class FestivalController : IFestivalController
	{
		private const string TimeFormat = "mm\\:ss";
		private const string TimeFormatLong = "{0:2D}:{1:2D}";
		private const string TimeFormatThreeDimensional = "{0:3D}:{1:3D}";


		private IInstrumentFactory instrumentFactory;
		private IPerformerFactory performerFactory;
		private ISongFactory songFactory;
		private ISetFactory setFactory;
		private readonly IStage stage;

		public FestivalController(IStage stage)
		{
			this.stage = stage;
			this.setFactory = new SetFactory();
			this.songFactory = new SongFactory();
			this.instrumentFactory = new InstrumentFactory();
			this.performerFactory = new PerformerFactory();
		}

		public string Report()
		{
			var result = string.Empty;

			var totalFestivalLength = new TimeSpan(this.stage.Sets.Sum(s => s.ActualDuration.Ticks));

			result += ($"Festival length: {totalFestivalLength}") + "\n";

			foreach (var set in this.stage.Sets)
			{
				result += ($"--{set.Name} ({set.ActualDuration}):") + "\n";

				var performersOrderedDescendingByAge = set.Performers.OrderByDescending(p => p.Age);
				foreach (var performer in performersOrderedDescendingByAge)
				{
					var instruments = string.Join(", ", performer.Instruments
						.OrderByDescending(i => i.Wear));

					result += ($"---{performer.Name} ({instruments})") + "\n";
				}

				if (!set.Songs.Any())
					result += ("--No songs played") + "\n";
				else
				{
					result += ("--Songs played:") + "\n";
					foreach (var song in set.Songs)
					{
						result += ($"----{song.Name} ({song.Duration.ToString(TimeFormat)})") + "\n";
					}
				}
			}

			return result.ToString();
		}

		public string RegisterSet(string[] args)
		{
			string setName = args[1];
			string setType = args[2];
			ISet set = this.setFactory.CreateSet(setName, setType);
			this.stage.AddSet(set);
			return $"Registered {setType} set";		
		}

		public string SignUpPerformer(string[] args)
		{
			string performerName = args[1];
			int performerAge = int.Parse(args[2]);
			IPerformer performer = this.performerFactory.CreatePerformer(performerName, performerAge);
			if (args.Length > 3)
			{
				for (int instrumentIndex = 3; instrumentIndex < args.Length; instrumentIndex++)
				{
					var instrumentType = args[instrumentIndex];
					IInstrument instrument = this.instrumentFactory.CreateInstrument(instrumentType);
					performer.AddInstrument(instrument);
				}
			}
			this.stage.AddPerformer(performer);
			return $"Registered performer {performer.Name}";
		}

		public string RegisterSong(string[] args)
		{
			// TODO: scheduled for next month

			string songName = args[1];
			string timeSpanString = args[2];
			TimeSpan songTime = TimeSpan.ParseExact(timeSpanString, "mm\\:ss",CultureInfo.InvariantCulture);
			ISong song = this.songFactory.CreateSong(songName, songTime);
			this.stage.AddSong(song);
			return $"Registered song {songName} ({songTime:mm\\:ss})";
		}

		//public string SongRegistration(string[] args)
		//{
		//	var songName = args[0];
		//	var setName = args[1];

		//	if (!this.stage.HasSet(setName))
		//	{
		//		throw new InvalidOperationException("Invalid set provided");
		//	}

		//	if (!this.stage.HasSong(songName))
		//	{
		//		throw new InvalidOperationException("Invalid song provided");
		//	}

		//	var set = this.stage.GetSet(setName);
		//	var song = this.stage.GetSong(songName);

		//	set.AddSong(song);

		//	return $"Added {song} to {set.Name}";
		//}

		// Временно!!! Чтобы работало делаем срез на конец месяца
		public string AddPerformerToSet(string[] args)
		{
			string performerName = args[1];
			string setName = args[2];

			bool performerExists = this.stage.HasPerformer(performerName);
			if (!performerExists)
			{
				throw new InvalidOperationException("Invalid performer provided");
			}

			bool setExists = this.stage.HasSet(setName);
			if (!setExists)
			{
				throw new InvalidOperationException("Invalid set provided");

			}
			ISet set = this.stage.GetSet(setName);
			IPerformer performer = this.stage.GetPerformer(performerName);
			set.AddPerformer(performer);
			return $"Added {performerName} to {setName}";
		}
		

		public string RepairInstruments(string[] args)
		{

			//var instruments = this.stage.Performers.Select(p => p.Instruments).ToList();
			//foreach (var item in instruments)
			//{
			//	item.
			//}
			//var forRepair = instruments
			//var repairedInstrumentsCount = instruments.Count;
			//foreach (var instrument in instruments)
			//{
			//	Instrument current = (Instrument)instrument;
			//	current.Repair();
			//}
			//return $"Repaired {repairedInstrumentsCount} instruments";

			var instrumentsToRepair = this.stage.Performers
				.SelectMany(p => p.Instruments)
				.Where(i => i.Wear < 100)
				.ToArray();

			foreach (var instrument in instrumentsToRepair)
			{
				instrument.Repair();
			}

			return $"Repaired {instrumentsToRepair.Length} instruments";
		}

		public string ProduceReport()
		{
			var result = string.Empty;

			var totalFestivalLength = new TimeSpan(this.stage.Sets.Sum(s => s.ActualDuration.Ticks));
			var totalFestivalLengthHours = totalFestivalLength.Hours;
			int minutes = 0;
			int seconds = 0;
			if (totalFestivalLengthHours >= 1)
			{
				minutes = totalFestivalLengthHours * 60 + totalFestivalLength.Minutes;
				seconds = totalFestivalLength.Seconds;
				//result += ($"Festival length: {totalFestivalLength:mm\\:ss}") + "\n";
				if (seconds < 10)
				{
					result += ($"Festival length: {minutes}:0{seconds}") + "\n";
				}
				else
					result += ($"Festival length: {minutes}:{seconds}") + "\n";
			}
			else
			{
				result += ($"Festival length: {totalFestivalLength:mm\\:ss}") + "\n";
			}

			

			foreach (var set in this.stage.Sets)
			{
				int minutesSet = 0;
				int secondsSet = 0;
				int totalSetHours = set.ActualDuration.Hours;

				if(totalSetHours >=1)
				{
					minutesSet = 60;
					secondsSet = set.ActualDuration.Seconds;

					if (seconds < 10)
					{
						result += ($"--{set.Name} ({minutesSet}:0{seconds}):") + "\n";
					}
					else
						result += ($"--{set.Name} ({minutesSet}:0{seconds}):") + "\n";
				}
				else
				{
					result += ($"--{set.Name} ({set.ActualDuration:mm\\:ss}):") + "\n";
				}

				


				//result += ($"--{set.Name} ({set.ActualDuration:mm\\:ss}):") + "\n";

				var performersOrderedDescendingByAge = set.Performers.OrderByDescending(p => p.Age);
				foreach (var performer in performersOrderedDescendingByAge)
				{
					var instruments = string.Join(", ", performer.Instruments
						.OrderByDescending(i => i.Wear));

					result += ($"---{performer.Name} ({instruments})") + "\n";
				}

				if (!set.Songs.Any())
					result += ("--No songs played") + "\n";
				else
				{
					result += ("--Songs played:") + "\n";
					foreach (var song in set.Songs)
					{
						result += ($"----{song.Name} ({song.Duration.ToString(TimeFormat)})") + "\n";
					}
				}
			}

			return result.ToString().Trim();
		}

		public string AddSongToSet(string[] args)
		{
			string songName = args[1];
			string setName = args[2];

			bool songExists = this.stage.HasSong(songName);
			if (!songExists)
			{
				throw new InvalidOperationException("Invalid song provided");
			}

			bool setExists = this.stage.HasSet(setName);
			if (!setExists)
			{
				throw new InvalidOperationException("Invalid set provided");

			}

			ISong song = this.stage.GetSong(songName);
			ISet set = this.stage.GetSet(setName);
			set.AddSong(song);
			return $"Added {songName} ({song.Duration:mm\\:ss}) to {setName}";
		}
	}
}