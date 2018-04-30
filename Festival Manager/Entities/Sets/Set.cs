namespace FestivalManager.Entities.Sets
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using FestivalManager.Entities.Contracts;

	public abstract class Set : ISet
	{
		//RegisterSet {name} {type}
		private IList<IPerformer> performers;
		private IList<ISong> songs;

		protected Set(string name, TimeSpan maxDuration)
		{
			this.Name = name;
			this.MaxDuration = maxDuration;

			this.performers = new List<IPerformer>();
			this.songs = new List<ISong>();
		}

		public string Name { get; }

		public TimeSpan MaxDuration { get; }

		public TimeSpan ActualDuration => new TimeSpan(this.Songs.Sum(s => s.Duration.Ticks));

		public IReadOnlyCollection<IPerformer> Performers => (List<IPerformer>)this.performers;

		public IReadOnlyCollection<ISong> Songs => (List<ISong>)this.songs;

		public void AddPerformer(IPerformer performer)
		{
			this.performers.Add(performer);
		}

		public void AddSong(ISong song)
		{
			if (song.Duration + this.ActualDuration > this.MaxDuration)
			{
				throw new InvalidOperationException("Song is over the set limit!");
			}

			this.songs.Add(song);
		}

		public bool CanPerform()
		{
			if(this.performers.Count==0)
			{
				return false;
			}

			if (this.songs.Count == 0)
			{
				return false;
			}

			var allPerformersHaveInstruments = this.Performers.All(p => p.Instruments.Any(i => !i.IsBroken));

			if (!allPerformersHaveInstruments)
			{
				return false;
			}

			return true;

		}
	}
}
