public class Easy : Mission
{
	private const string name = "Suppression of civil rebellion";
	public Easy(double scoreToComplete) : base(scoreToComplete)
	{
	}

	public override string Name => name;

	public override double EnduranceRequired => 20;

	public override double WearLevelDecrement => 30;
}

