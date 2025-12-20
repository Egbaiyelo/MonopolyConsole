import java.util.Scanner;

public class BotMain {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        while (scanner.hasNextLine()) {
            String input = scanner.nextLine();

            String response = handleCommand(input);
            System.out.println(response); 
        }
        scanner.close();
    }

    private static String handleCommand(String input) {
        return "money money money";
    }
}
