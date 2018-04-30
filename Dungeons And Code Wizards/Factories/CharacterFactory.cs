using System;
using System.Linq;
using System.Reflection;

public class CharacterFactory : ICharacterFactory
{
	public ICharacter CreateCharacter(string faction, string characterType, string name)
	{

		bool isValid = Enum.TryParse(typeof(Faction), faction, out object result);

		if(!isValid)
		{
			throw new ArgumentException($"Invalid faction \"{faction}\"!");
		}

		if(string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentException($"Name cannot be null or whitespace!");
		}

		Assembly assembly = Assembly.GetExecutingAssembly();
		Type type = assembly.GetTypes().FirstOrDefault(t => t.Name == characterType);

		if(type == null)
		{
			throw new ArgumentException($"Invalid character \"{ characterType }\"!");
		}

		return (ICharacter)Activator.CreateInstance(type, name, result);
		
	}
}

