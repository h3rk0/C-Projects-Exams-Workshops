namespace FestivalManager.Entities.Factories
{
	using System;
	using System.Linq;
	using System.Reflection;
	using Contracts;
	using Entities.Contracts;

	public class SongFactory : ISongFactory
	{
		public ISong CreateSong(string name, TimeSpan duration)
		{
			//RegisterSong {name} {mm:ss}
			//Assembly assembly = Assembly.GetCallingAssembly();
			//Type song = assembly.GetTypes().FirstOrDefault(t => t.Name == name);
			var song = new Song(name, duration);
			return song;
		}
	}
}