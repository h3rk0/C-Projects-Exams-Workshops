using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DungeonMaster 
{
	private ICollection<ICharacter> Party { get;}
	private Stack<IItem> ItemPool { get;}
	private IItemFactory ItemFactory { get;}
	private ICharacterFactory CharacterFactory { get;}
	private ICharacter LastSurvivor { get;}
	private int SoloRounds { get; set; }

	public DungeonMaster()
	{
		this.Party = new List<ICharacter>();
		this.ItemPool = new Stack<IItem>();
		this.ItemFactory = new ItemFactory();
		this.CharacterFactory = new CharacterFactory();
		this.SoloRounds = 0;
	}

	public string JoinParty(string[] args)
	{
		//CSharp Warrior Gosho
		string faction = args[0];
		string characterType = args[1];
		string name = args[2];
		ICharacter character = CharacterFactory.CreateCharacter(faction, characterType, name);

		this.Party.Add(character);
		return $"{name} joined the party!";
	}

	public string AddItemToPool(string[] args)
	{
		string itemStr = args[0];

		IItem item = ItemFactory.CreateItem(itemStr);

		this.ItemPool.Push(item);
		return $"{itemStr} added to pool.";
	}

	public string PickUpItem(string[] args)
	{
		string characterStr = args[0];
		ICharacter character = this.Party.SingleOrDefault(c => c.Name == characterStr);

		if(character == null)
		{
			throw new ArgumentException($"Character {characterStr} not found!");
		}

		if(this.ItemPool.Count==0)
		{
			throw new InvalidOperationException($"No items left in pool!");
		}

		IItem item = this.ItemPool.Pop();

		character.Bag.AddItem(item);

		return $"{characterStr} picked up {item.GetType().Name}!";
	}

	public string UseItem(string[] args)
	{
		string characterName = args[0];
		string itemName = args[1];

		ICharacter character = this.Party.SingleOrDefault(c => c.Name == characterName);

		if (character == null)
		{
			throw new ArgumentException($"Character {characterName} not found!");
		}

		IItem item = character.Bag.GetItem(itemName);
		character.UseItem(item);
		return $"{character.Name} used {itemName}.";
	}

	public string UseItemOn(string[] args)
	{
		string giverName = args[0];
		string receiverName = args[1];
		string itemName = args[2];

		ICharacter giver = this.Party.SingleOrDefault(c => c.Name == giverName);

		if (giver == null)
		{
			throw new ArgumentException($"Character {giverName} not found!");
		}

		ICharacter receiver = this.Party.SingleOrDefault(c => c.Name == receiverName);

		if (receiver == null)
		{
			throw new ArgumentException($"Character {giverName} not found!");
		}

		IItem item = giver.Bag.GetItem(itemName);

		giver.UseItemOn(item, receiver);

		return $"{giverName} used {itemName} on {receiverName}.";
	}

	public string GiveCharacterItem(string[] args)
	{
		string giverName = args[0];
		string receiverName = args[1];
		string itemName = args[2];

		ICharacter giver = this.Party.SingleOrDefault(c => c.Name == giverName);

		if (giver == null)
		{
			throw new ArgumentException($"Character {giverName} not found!");
		}

		ICharacter receiver = this.Party.SingleOrDefault(c => c.Name == receiverName);

		if (receiver == null)
		{
			throw new ArgumentException($"Character {giverName} not found!");
		}

		IItem item = giver.Bag.GetItem(itemName);
		receiver.ReceiveItem(item);

		return $"{giverName} gave {receiverName} {itemName}.";
	}

	public string GetStats()
	{
		StringBuilder sb = new StringBuilder();
		foreach (Character character in this.Party.OrderByDescending(p => p.IsAlive).ThenByDescending(p => p.Health))
		{
			string status;
			if(character.IsAlive)
			{
				status = "Alive";
			}
			else
			{
				status = "Dead";
			}
			sb.AppendLine($"{character.Name} - HP: {character.Health}/{character.BaseHealth}, AP: {character.Armor}/{character.BaseArmor}, Status: {status}");
			
		}
		return sb.ToString().Trim();
	}

	public string Attack(string[] args)
	{
		string attackerName = args[0];
		string receiverName = args[1];
		ICharacter giver = this.Party.SingleOrDefault(c => c.Name == attackerName);

		if (giver == null)
		{
			throw new ArgumentException($"Character {attackerName} not found!");
		}

		ICharacter receiver = this.Party.SingleOrDefault(c => c.Name == receiverName);

		if (receiver == null)
		{
			throw new ArgumentException($"Character {receiverName} not found!");
		}

		if(giver == receiver)
		{
			throw new InvalidOperationException($"Cannot attack self!");
		}

		if(giver.GetType().Name=="Cleric")
		{
			throw new ArgumentException($"{giver.Name} cannot attack!");
		}

		Warrior warrior = (Warrior)giver;
		warrior.Attack(receiver);
		StringBuilder sb = new StringBuilder();

		
		sb.AppendLine($"{attackerName} attacks {receiverName} for {giver.AbilityPoints} hit points! {receiverName} has {receiver.Health}/{receiver.BaseHealth} HP and {receiver.Armor}/{receiver.BaseArmor} AP left!");
		if(!receiver.IsAlive)
		{
			sb.AppendLine($"{receiver.Name} is dead!");
		}

		return sb.ToString().Trim();
	}

	public string Heal(string[] args)
	{
		string healerName = args[0];
		string healReceiverName = args[1];

		ICharacter healer = this.Party.SingleOrDefault(c => c.Name == healerName);

		if (healer == null)
		{
			throw new ArgumentException($"Character {healerName} not found!");
		}

		ICharacter receiver = this.Party.SingleOrDefault(c => c.Name == healReceiverName);

		if (receiver == null)
		{
			throw new ArgumentException($"Character {healReceiverName} not found!");
		}

		if (healer is Warrior)
		{
			throw new ArgumentException($"{healer.Name} cannot heal!");
		}

		Cleric cleric = (Cleric)healer;
		cleric.Heal(receiver);

		return $"{healer.Name} heals {receiver.Name} for {healer.AbilityPoints}! {receiver.Name} has {receiver.Health} health now!";
	}

	public string EndTurn(string[] args)
	{

		StringBuilder sb = new StringBuilder();

		foreach (Character character in this.Party)
		{
			double healthBeforeReset = character.Health;
			if(character.IsAlive)
			{
				character.Rest();
				sb.AppendLine($"{character.Name} rests ({healthBeforeReset} => {character.Health})");
			}
		}

		var alive = this.Party.Where(p => p.IsAlive == true);
		if(alive.Count() ==1|| alive.Count() ==0)
		{
			this.SoloRounds++;
		}

		return sb.ToString().Trim();
	}

	public bool IsGameOver()
	{
		var alive = this.Party.Where(p => p.IsAlive == true);

		if (alive.Count() == 1 && this.SoloRounds > 1)
		{
			return true;
		}

			return false;
		
	}
	
}

