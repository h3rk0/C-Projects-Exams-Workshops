public class PoisonPotion : Item
{
	public PoisonPotion() : base(5)
	{
	}

	public override void AffectCharacter(Character character)
	{
		base.AffectCharacter(character);

		character.AffectHealth(-20);

		if(character.Health <= 0)
		{
			character.CharacterIsDead();
		}
	}
}

