public interface ICharacter
{
	double AbilityPoints { get; }
	double Armor { get; }
	IBag Bag { get; }
	double BaseArmor { get; }
	double BaseHealth { get; }
	Faction Faction { get; }
	double Health { get; }
	bool IsAlive { get; }
	string Name { get; }
	double RestHealMultiplier { get; }

	void AffectHealth(double health);
	void CharacterIsDead();
	void ReceiveItem(IItem item);
	void Rest();
	void SetArmor(double armor);
	void TakeDamage(double hitPoints);
	void UseItem(IItem item);
	void UseItemOn(IItem item, ICharacter character);
}