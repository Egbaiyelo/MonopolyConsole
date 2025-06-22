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
	    Bot bot = switch (type) {
	        case "EasyBot" -> new EasyBot(1500);
	        default -> new EasyBot(1500);
	    };
	    bot.listen();
	}
}
