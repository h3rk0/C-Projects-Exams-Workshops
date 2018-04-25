

public class Hard : Mission
{
	private const string name = "Disposal of terrorists";
	public Hard(double scoreToComplete) : base(scoreToComplete)
	{
	}

	public override string Name => name;

	public override double EnduranceRequired => 80;

	public override double WearLevelDecrement => 70;
}

