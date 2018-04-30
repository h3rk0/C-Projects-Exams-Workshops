namespace FestivalManager.Entities
{
	using System.Collections.Generic;

	using Contracts;

	public class Performer : IPerformer
	{
		//SignUpPerformer {name} {age} {instrument1} {instrument2} {instrumentN}
		private readonly List<IInstrument> instruments;

		public Performer(string name, int age)
		{
			this.Name = name;
			this.Age = age;

			this.instruments = new List<IInstrument>();
		}

		public string Name { get; }

		public int Age { get; }

		public IReadOnlyCollection<IInstrument> Instruments => this.instruments;

		public void AddInstrument(IInstrument instrument)
		{
			this.instruments.Add(instrument);
		}

		public override string ToString()
		{
			return this.Name;
		}
	}
}
