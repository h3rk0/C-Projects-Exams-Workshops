public interface ICharacterFactory
{
	ICharacter CreateCharacter(string faction, string characterType, string name);
}