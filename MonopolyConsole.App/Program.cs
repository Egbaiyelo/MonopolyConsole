
using MonopolyConsole.App;

public class Solution
{
    public static void Main(string[] args)
    {
        //new GameEngine
        var game = new GameRunner();

        game.SetupGame();
        game.Run();

        //Query -> create players, numof, names, state them
        //Gameengine setup(players)
        //program start() -> play till one person left
        //gameengine process turn(player)
        //gameengine rolldice
        //gameengine processlanding
        //gameengine 
    }
}