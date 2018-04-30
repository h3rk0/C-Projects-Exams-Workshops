namespace DungeonsAndCodeWizards.Contracts
{
	public interface ICommand
    {
		string Execute(string[] args);
    }
}
