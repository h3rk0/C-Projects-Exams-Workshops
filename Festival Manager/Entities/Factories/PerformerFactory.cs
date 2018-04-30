namespace FestivalManager.Entities.Factories
{
	using Contracts;
	using Entities.Contracts;

	public class PerformerFactory : IPerformerFactory
	{
		public IPerformer CreatePerformer(string name, int age)
		{
			//SignUpPerformer {name} {age} {instrument1} {instrument2} {instrumentN}

			var performer = new Performer(name, age);

			return performer;
		}
	}
}