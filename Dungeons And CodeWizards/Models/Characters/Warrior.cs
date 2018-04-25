using System;
public class Warrior : Character , IAttackable
{
	public Warrior(string name, Faction faction) : base(name, 100, 50, 40,new Satchel(), faction)
	{
		this.BaseHealth = 100;
		this.BaseArmor = 50;
	}

	public void Attack(ICharacter character)
	{
		if (!this.IsAlive || !character.IsAlive)
		{
			throw new InvalidOperationException($"Must be alive to perform this action!");
		}

		if (character == this)
		{
			throw new InvalidOperationException($"Cannot attack self!");
		}

		if(this.Faction == character.Faction)
		{
			throw new ArgumentException($"Friendly fire! Both characters are from {this.Faction} faction!");
		}

		character.TakeDamage(this.AbilityPoints);
	}
}

