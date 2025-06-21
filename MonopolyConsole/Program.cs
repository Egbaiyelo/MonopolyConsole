
using MonopolyConsole;
using MonopolyConsole.Utils;
using System;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

public static class Solution
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

        Game game = new Game();
        game.SetGame();
        game.Start();
        //TestProperties();


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

    }

    public static void TestProperties()
    {
        PropertyTile basetile = new PropertyTile(new Property("test", 300, 30));
        Console.WriteLine($"rent is {basetile.getRent()}");

        basetile.Property.Houses = 1;
        Console.WriteLine($"rent is {basetile.getRent()}");

        basetile.Property.Mortgaged = true;
        Console.WriteLine($"rent is {basetile.getRent()}");

        int res = 3 / 2;
        Console.WriteLine(res );
        // 
        Player ere = new Player(new Game(), "ere", 1000);
        StationTile stat = new StationTile(new Property("reed station", 200, 25));
        StationTile other = new StationTile(new Property("other station", 200, 25));
        stat.Property.Owner = other.Property.Owner = ere;
        //stat.Owner = other.Owner = ere;
        ere.StationsOwned = 2;
        Console.WriteLine($"rent is {stat.getRent()}");

        ere.Balance -= 3000;



    }


}