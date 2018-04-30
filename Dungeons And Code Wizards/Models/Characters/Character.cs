using System;
public abstract class Character : ICharacter
{
	private string name;
	private double baseHealth;
	private double health;
	private double baseArmor;
	private double armor;
	private bool isAlive;
	private IBag bag;
	private Faction faction;
	private double abilityPoints;
	private double restHealMultiplier;

	protected Character(string name, double health, double armor, double abilityPoints, Bag bag, Faction faction)
	{
		this.Name = name;
		this.Health = health;
		this.Armor = armor;
		this.AbilityPoints = abilityPoints;
		this.Bag = bag;
		this.Faction = faction;
		this.isAlive = true;
		this.RestHealMultiplier = 0.2;
	}

	public string Name
	{
		get { return name; }
		private set
		{
			if(string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException($"Name cannot be null or whitespace!");
			}
			name = value;
		}
	}
	
	public double BaseHealth
	{
		get { return baseHealth; }
		protected set { baseHealth = value; }
	}
	
	public double Health
	{
		get { return health; }
		private set { health = value; }
	}
	
	public double BaseArmor
	{
		get { return baseArmor; }
		protected set { baseArmor = value; }
	}
	
	public double Armor
	{
		get { return armor; }
		private set { armor = value; }
	}


	public double AbilityPoints
	{
		get { return abilityPoints; }
		private set { abilityPoints = value; }
	}


	public bool IsAlive
	{
		get { return isAlive; }
		private set { isAlive = value; }
	}


	public virtual double RestHealMultiplier
	{
		get { return restHealMultiplier; }
		private set { restHealMultiplier = value; }
	}


	public Faction Faction
	{
		get { return faction; }
		private set { faction = value; }
	}

	public IBag Bag
	{
		get { return bag; }
		private set { bag = value; }
	}

	public void AffectHealth(double health)
	{
		this.Health += health;

		if(this.Health >= this.baseHealth)
		{
			this.Health = this.BaseHealth;
		}

		if (this.Health <= 0)
		{
			this.health = 0;
			this.isAlive = false;
		}
	}

	public void CharacterIsDead()
	{
		this.IsAlive = false;
	}

	public void SetArmor(double armor)
	{
		this.Armor = armor;
	}

	public void TakeDamage(double hitPoints)
	{
		if (!this.IsAlive)
		{
			throw new InvalidOperationException($"Must be alive to perform this action!");
		}

		if(hitPoints > this.armor)
		{
			hitPoints -= this.Armor;
			this.Armor = 0;
			this.Health -= hitPoints;
		}
		else
		{
			this.armor -= hitPoints;
		}

		if(this.Health <= 0 )
		{
			this.Health = 0;
			this.IsAlive = false;
		}
	}

	public void Rest()
	{
		if (!this.IsAlive)
		{
			throw new InvalidOperationException($"Must be alive to perform this action!");
		}

		this.Health += this.BaseHealth * this.RestHealMultiplier;

		if(this.Health>=this.baseHealth)
		{
			this.Health = this.baseHealth;
		}
	}

	public void UseItem(IItem item)
	{
		if (!this.IsAlive)
		{
			throw new InvalidOperationException($"Must be alive to perform this action!");
		}

		item.AffectCharacter(this);
	}

	public void UseItemOn(IItem item, ICharacter character)
	{
		if (!this.IsAlive || !character.IsAlive)
		{
			throw new InvalidOperationException($"Must be alive to perform this action!");
		}

		character.Bag.AddItem(item);
		character.UseItem(item);
	}

	public void ReceiveItem(IItem item)
	{
		if (!this.IsAlive)
		{
			throw new InvalidOperationException($"Must be alive to perform this action!");
		}

		this.Bag.AddItem(item);
	}


}

