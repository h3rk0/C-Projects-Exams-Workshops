

public class Medium : Mission
{
	private const string name = "Capturing dangerous criminals";
	public Medium(double scoreToComplete) : base(scoreToComplete)
	{
	}

	public override string Name => name;

	public override double EnduranceRequired => 50;

	public override double WearLevelDecrement => 50;
}

