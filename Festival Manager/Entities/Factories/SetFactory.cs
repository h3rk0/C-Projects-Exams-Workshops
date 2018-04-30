namespace FestivalManager.Entities.Factories
{
	using System;
	using System.Linq;
	using System.Reflection;

	using Contracts;
	using Entities.Contracts;

	public class SetFactory : ISetFactory
	{
		public ISet CreateSet(string name, string type)
		{
			Assembly assembly = Assembly.GetCallingAssembly();
			Type setType = assembly.GetTypes().FirstOrDefault(t => t.Name == type);
			ISet set = (ISet) Activator.CreateInstance(setType, name);
			return set;
		}
	}
}
