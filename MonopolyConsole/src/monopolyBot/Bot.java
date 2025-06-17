/**
 * 
 */
package monopolyBot;

import java.util.Scanner;

/**
 * 
 */

public class Bot {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        System.out.println("I'm aactually running");

        while (scanner.hasNextLine()) {
            String input = scanner.nextLine();

            System.out.println("Thinking ... hm");
            System.out.flush();
            String response = handleCommand(input);
            System.out.println(response); 
            System.out.flush();
        }
        scanner.close();
    }

    private static String handleCommand(String input) {
        //- communicate with JSON?
        // maybe "{\"action\":\"rollDice\"}"
        return ":stop";
    }
}

