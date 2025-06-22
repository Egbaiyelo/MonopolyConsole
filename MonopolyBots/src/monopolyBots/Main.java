/**
 * 
 */
package monopolyBots;

/**
 * Just initialize the bots
 */
public class Main {
	public static void main(String[] args) {
	    String type = args.length > 0 ? args[0] : "EasyBot";
	    String name = args.length > 1 ? args[1] : "bot1";
	    
	    Bot bot = switch (type) {
	        case "EasyBot" -> new EasyBot(1500, name);
	        default -> new EasyBot(1500, "bot2");
	    };
	    bot.listen();
	}
	
	
}
