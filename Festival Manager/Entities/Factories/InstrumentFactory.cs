﻿namespace FestivalManager.Entities.Factories
{
	using System;
	using System.Linq;
	using System.Reflection;

	using Contracts;
	using Entities.Contracts;

	public class InstrumentFactory : IInstrumentFactory
	{


		public IInstrument CreateInstrument(string type)
		{
			Assembly assembly = Assembly.GetCallingAssembly();
			Type instrumentType = assembly.GetTypes().FirstOrDefault(t => t.Name == type);
			IInstrument instrument = (IInstrument)Activator.CreateInstance(instrumentType);
			return instrument;

			//if (type == "Drums")
			//{
			//	return new Drums();
			//}
			//else if (type == "Guitar")
			//{
			//	return new Guitar();
			//}
			//else
			//{
			//	return new Microphone();
			//}
		}
	}
}