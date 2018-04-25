namespace FestivalManager.Entities.Instruments
{
    public class Guitar : Instrument
    {
		private const int drumsRepairAmount = 60;

		protected override int RepairAmount => drumsRepairAmount;
    }
}
