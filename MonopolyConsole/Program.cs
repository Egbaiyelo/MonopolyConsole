
using MonopolyConsole;
using MonopolyConsole.Utils;
using System;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

public class Solution
{
    public static void Main()
    {
        //Console.WriteLine("How many players");
        //int input = Convert.ToInt32(Console.ReadLine());
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

        Game game = new Game(2);

        //string javaBotJarPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "jbot.jar");
        //Console.WriteLine(javaBotJarPath);

        //Testing query
        //Console.WriteLine("\n\n\nwfe");
        //ConsoleQuery test = new ConsoleQuery("Hello", new List<string>{ "a", "b", "c" }, "yey", "ney");
        //int res = test.RunQuery();
        //Console.WriteLine($"{res} is fine");

        //Testing Bot
        //BotManager botManager = new BotManager();
        //int botIndex = botManager.Spawn();
        //Console.WriteLine("sending message");
        //string response = botManager.SendMessage(botIndex, "hello");
        //Console.WriteLine("total response " + response);


        //PropertyTile basetile = new PropertyTile(new Property("test", 300, 30));
        //Console.WriteLine($"rent is {basetile.CalculateRent()}");

        //PropertyTile test = new MortgageDecorator(basetile);
        //Console.WriteLine($"rent is {test.CalculateRent()}");

        //test = new HouseDecorator(basetile, 1);
        //Console.WriteLine($"rent is {test.CalculateRent()}");


    }
}