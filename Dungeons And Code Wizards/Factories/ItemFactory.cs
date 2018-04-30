using System;
using System.Linq;
using System.Reflection;

public class ItemFactory : IItemFactory
{
	public IItem CreateItem(string itemType)
	{
		Assembly assembly = Assembly.GetExecutingAssembly();
		Type type = assembly.GetTypes().FirstOrDefault(t => t.Name == itemType);

		if(type == null)
		{
			throw new ArgumentException($"Invalid item \"{itemType}\"!");
		}

		return (IItem)Activator.CreateInstance(type);
		
	}
}

