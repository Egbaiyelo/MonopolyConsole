package monopolyBots;

import java.util.Scanner;

public abstract class Bot {

	public void listen() {
        Scanner scanner = new Scanner(System.in);
        System.out.println("I'm aactually running");

        while (scanner.hasNextLine()) {
            String input = scanner.nextLine();
            
            if (input.trim().equalsIgnoreCase("quit")) {
                break;
            }
            
            System.out.println("Thinking ... hm");
            System.out.flush();
            
            String response = handleCommand(input);
            System.out.println(response);
            System.out.flush();
        }
        
        System.out.println("closing");
        scanner.close();
    }

    protected abstract String handleCommand(String input);


}




//Normal Bot
//Takes risk
//More dynamic strategies
//Personal valuations
//