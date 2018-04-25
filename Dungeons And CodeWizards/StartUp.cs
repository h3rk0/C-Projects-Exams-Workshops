using System;
using System.Linq;
using System.Text;

namespace DungeonsAndCodeWizards
{
	public class StartUp
	{
		
		public static void Main(string[] args)
		{
			
			DungeonMaster dm = new DungeonMaster();
			Engine engine = new Engine(dm);
			engine.Run();
			
		}
	}
}