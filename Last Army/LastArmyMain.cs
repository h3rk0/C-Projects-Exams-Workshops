public class LastArmyMain
{
    static void Main()
    {
		IWriter writer = new ConsoleWriter();
		IReader reader = new ConsoleReader();
		Engine engine = new Engine(reader, writer);
		engine.Run();

    }
}
