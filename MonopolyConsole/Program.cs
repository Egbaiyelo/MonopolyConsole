
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

        //Game game = new Game(2);

        //string javaBotJarPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "jbot.jar");
        //Console.WriteLine(javaBotJarPath);

        BotManager botManager = new BotManager();
        int botIndex = botManager.Spawn();
        Console.WriteLine("sending message");
        string response = botManager.SendMessage(botIndex, "hello");
        Console.WriteLine("total response " + response);

    }
}