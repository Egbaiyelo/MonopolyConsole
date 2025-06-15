
using MonopolyConsole;
using MonopolyConsole.Utils;

public class Solution
{
    public static void Main()
    {
        //Console.WriteLine("How many players");
        //Console.WriteLine("How many players");
        //Console.WriteLine("How many players");
        Console.WriteLine("How many players");
        int input = Convert.ToInt32(Console.ReadLine());
        //ConsoleQuery<bool> ght = new ConsoleQuery<bool>(
        //    "Do you want", "success", "error",
        //    new List<(string, bool)>()
        //    {
        //        ("yes", true),
        //        ("no", false)
        //    });
        //ght.RunQuery();
        //ConsoleDisplay hello = new ConsoleDisplay(["hefwefwfe", "uhwevwefwefw", "weuvwevwe"], true);
        //hello.RunDisplay();
        Game game = new Game(input);
    }
}