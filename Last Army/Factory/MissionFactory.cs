using System;
using System.Linq;
using System.Reflection;

public class MissionFactory : IMissionFactory
{
	// Mission Medium 79
	public IMission CreateMission(string difficultyLevel, double neededPoints)
	{
		Assembly assembly = Assembly.GetExecutingAssembly();
		Type type = assembly.GetTypes().FirstOrDefault(t => t.Name == difficultyLevel);
		return (IMission)Activator.CreateInstance(type, neededPoints);
	}
}

