using System.Collections.Generic;

public interface IBag
{
	int Capacity { get; }
	IReadOnlyCollection<IItem> Items { get; }
	int Load { get; }

	void AddItem(IItem item);
	IItem GetItem(string name);
	void RemoveItem(IItem item);
}