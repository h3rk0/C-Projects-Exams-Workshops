namespace FestivalManager.Entities
{
	using System.Collections.Generic;
	using System.Linq;

	using Contracts;

	public class Stage : IStage
	{

		private List<ISet> sets;
		private List<ISong> songs;
		private List<IPerformer> performers;

		public Stage()
		{
			this.sets = new List<ISet>();
			this.songs = new List<ISong>();
			this.performers = new List<IPerformer>();
		}


		public IReadOnlyCollection<ISet> Sets => (IReadOnlyCollection<ISet>)this.sets;

		public IReadOnlyCollection<ISong> Songs => (IReadOnlyCollection<ISong>)this.songs;

		public IReadOnlyCollection<IPerformer> Performers => (IReadOnlyCollection<IPerformer>)this.performers;

		public void AddPerformer(IPerformer performer)
		{
			this.performers.Add(performer);
		}

		public void AddSet(ISet set)
		{
			this.sets.Add(set);
		}

		public void AddSong(ISong song)
		{
			this.songs.Add(song);
		}

		public IPerformer GetPerformer(string name)
		{
			// got it or null
			IPerformer performer = this.performers.FirstOrDefault(p => p.Name == name);
			return performer;
		}

		public ISet GetSet(string name)
		{
			ISet set = this.sets.FirstOrDefault(s => s.Name == name);
			return set;
		}

		public ISong GetSong(string name)
		{
			ISong song = this.songs.FirstOrDefault(s => s.Name == name);
			return song;
		}

		public bool HasPerformer(string name)
		{
			IPerformer performer = this.performers.FirstOrDefault(p => p.Name == name);
			if(performer == null)
			{
				return false;
			}
			return true;
		}

		public bool HasSet(string name)
		{
			ISet set = this.sets.FirstOrDefault(s => s.Name == name);
			if(set == null)
			{
				return false;
			}
			return true;
		}

		public bool HasSong(string name)
		{
			ISong song = this.songs.FirstOrDefault(s => s.Name == name);
			if(song == null)
			{
				return false;
			}
			return true;
		}
	}
}
