namespace FestivalManager.Entities.Instruments
{
    public class Microphone : Instrument
    {
		private const int drumsRepairAmount = 80;

		protected override int RepairAmount => drumsRepairAmount;
    }
}
