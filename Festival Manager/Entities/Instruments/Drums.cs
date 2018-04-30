namespace FestivalManager.Entities.Instruments
{
	public class Drums : Instrument
	{
		private const int drumsRepairAmount = 20;

		protected override int RepairAmount => drumsRepairAmount;
	}
}
