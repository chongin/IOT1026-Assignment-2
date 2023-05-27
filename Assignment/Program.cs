
namespace Assignment
{
    static class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello, World!");
            TreasureChest chest = new TreasureChest();
            var state = chest.Manipulate(TreasureChest.Action.Unlock);
            Console.WriteLine($"Unlock it success, and return state:{state}");
            Console.WriteLine(chest.ToString());
        }
    }
}
