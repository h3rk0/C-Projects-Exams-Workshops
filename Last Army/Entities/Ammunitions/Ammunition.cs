using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public abstract class Ammunition : IAmmunition
{
	// Warehouse [Name] [Count]
	private const int wearLevelMultiplier = 100;

	protected Ammunition()
	{
		this.WearLevel = this.Weight * wearLevelMultiplier;
	}

	public string Name => this.GetType().Name;

	public abstract double Weight { get;}

	public double WearLevel { get; private set; }

	public void DecreaseWearLevel(double wearAmount)
	{
		this.WearLevel -= wearAmount;
	}
}

