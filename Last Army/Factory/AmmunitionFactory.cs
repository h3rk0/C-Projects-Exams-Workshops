
using System;
using System.Linq;
using System.Reflection;


public class AmmunitionFactory
{
    public IAmmunition CreateAmmunition(string name)
    {
		Assembly assembly = Assembly.GetExecutingAssembly();
		Type type = assembly.GetTypes().FirstOrDefault(t => t.Name == name);
		return (IAmmunition)Activator.CreateInstance(type);
    }

        
}
