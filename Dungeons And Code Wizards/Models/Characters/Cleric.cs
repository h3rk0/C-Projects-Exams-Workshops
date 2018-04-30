using System;
public class Cleric : Character ,IHealable
{
	public Cleric(string name, Faction faction) : base(name, 50, 25, 40, new Backpack(), faction)
	{
		this.BaseHealth = 50;
		this.BaseArmor = 25;
	}

	public override double RestHealMultiplier => 0.5;

	public void Heal(ICharacter character)
	{
		if (!this.IsAlive || !character.IsAlive)
		{
			throw new InvalidOperationException($"Must be alive to perform this action!");
		}

		if(this.Faction != character.Faction)
		{
			throw new InvalidOperationException($"Cannot heal enemy character!");
		}

		character.AffectHealth(this.AbilityPoints);
	}
}

