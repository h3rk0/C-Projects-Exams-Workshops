using System.Collections.Generic;
using System.Linq;

public class WareHouse : IWareHouse
{
	private Dictionary<string, int> ammunitions;
	private AmmunitionFactory ammunitionFactory;
	public WareHouse()
	{
		this.ammunitions = new Dictionary<string, int>();
		this.ammunitionFactory = new AmmunitionFactory();
	}

	public void AddAmmunition(string ammunition,int quantity)
	{
		if(this.ammunitions.ContainsKey(ammunition))
		{
			this.ammunitions[ammunition] += quantity;
		}
		else
		{
			this.ammunitions.Add(ammunition, quantity);
		}
	}

	public void EquipArmy(IArmy army)
	{
		foreach (var soldier in army.Soldiers)
		{
			this.TryEquipSoldier(soldier);
		}
	}

	public bool TryEquipSoldier(ISoldier soldier)
	{
		var usedWeapons = soldier.Weapons.Where(w => w.Value == null).Select(w => w.Key).ToList(); 
		bool equiped = true;
		foreach (var weapon in usedWeapons)
		{
			if(this.ammunitions.ContainsKey(weapon) && this.ammunitions[weapon]>0)
			{
				soldier.Weapons[weapon] = this.ammunitionFactory.CreateAmmunition(weapon);
				this.ammunitions[weapon]--;
			}
			else
			{
				equiped = false;
			}
		}
		return equiped;
	}
}

