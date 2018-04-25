using System;
using System.Collections.Generic;
using System.Linq;

public abstract class Soldier : ISoldier
{
	private const int maxEndurance = 100;
    private double endurance;
	private int baseRegenerateIncrease = 10;

	//Soldier [Type] [Name] [Age] [Experience] [Endurance]
	protected Soldier(string name,int age,double experience,double endurance)
	{
		this.Name = name;
		this.Age = age;
		this.Experience = experience;
		this.Endurance = endurance;
		this.Weapons = InitializeWeapons();
	}

	private IDictionary<string, IAmmunition> InitializeWeapons()
	{
		Dictionary<string, IAmmunition> weapons = new Dictionary<string, IAmmunition>();
		foreach (string weapon in this.WeaponsAllowed)
		{
			weapons.Add(weapon, null);
		}

		return weapons;
	}

	public IDictionary<string, IAmmunition> Weapons { get; }
    
    protected abstract IReadOnlyList<string> WeaponsAllowed { get; }

	public string Name { get; private set; }

	public int Age { get; private set; }

	public double Experience { get; private set; }

	public double Endurance
	{
		get
		{
			return this.endurance;
		}
		protected set
		{
			this.endurance = Math.Min(value, maxEndurance);
		}
	}

	protected abstract double OverallSkillMultiplier { get; }
	protected virtual int RegenerationIncrease => baseRegenerateIncrease;

	public double OverallSkill => (this.Age + this.Experience) * OverallSkillMultiplier; 

	public bool ReadyForMission(IMission mission)
    {
        if (this.Endurance < mission.EnduranceRequired)
        {
            return false;
        }

        bool hasAllEquipment = this.Weapons.Values.Count(weapon => weapon == null) == 0;

        if (!hasAllEquipment)
        {
            return false;
        }

        return this.Weapons.Values.Count(weapon => weapon.WearLevel <= 0) == 0;
    }
	
    public override string ToString() => string.Format(OutputMessages.SoldierToString, this.Name, this.OverallSkill);
	
	public virtual void Regenerate()
	{
		this.Endurance += RegenerationIncrease + this.Age;
	}

	public void CompleteMission(IMission mission)
	{
		this.Experience += mission.EnduranceRequired;
		Endurance -= mission.EnduranceRequired;
		foreach (var weapon in this.Weapons.Values.Where(w => w!=null).ToList())
		{
			weapon.DecreaseWearLevel(mission.WearLevelDecrement);
			if(weapon.WearLevel<=0)
			{
				this.Weapons[weapon.Name] = null;
			}
		}
	}
}