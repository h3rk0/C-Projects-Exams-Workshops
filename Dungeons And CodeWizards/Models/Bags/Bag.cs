using System;
using System.Collections.Generic;
using System.Linq;
public abstract class Bag : IBag
{
	private int capacity;
	private int load;
	private ICollection<IItem> items;

	protected Bag(int capacity = 100)
	{
		this.Capacity = capacity;
		this.Load = 0;
		this.items = new List<IItem>();
	}

	public int Capacity
	{
		get { return capacity; }
		private set { capacity = value; }
	}


	public int Load
	{
		get { return load; }
		private set { load = Math.Min(Math.Max(value, 0), capacity); }
	}


	public IReadOnlyCollection<IItem> Items => (IReadOnlyCollection<Item>)this.items; 

	public void AddItem(IItem item)
	{
		if(this.Load + item.Weight > this.Capacity)
		{
			throw new InvalidOperationException($"Bag is full!");
		}

		this.Load += item.Weight;

		this.items.Add(item);
	}

	public IItem GetItem(string name)
	{
		if(this.items.Count==0)
		{
			throw new InvalidOperationException($"Bag is empty!");
		}
		IItem item = this.items.FirstOrDefault(i => i.GetType().Name == name);

		if(item == null)
		{
			throw new ArgumentException($"No item with name {name} in bag!");
		}

		this.items.Remove(item);

		return item;
	}

	public void RemoveItem(IItem item)
	{
		this.items.Remove(item);
		this.load -= item.Weight;
	}

}

