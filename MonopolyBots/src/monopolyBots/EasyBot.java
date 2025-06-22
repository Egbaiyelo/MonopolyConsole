package monopolyBots;

/**
* Keeps track of balance and doesnt want it to go below certain threshold
* Keeps track of networth (assets) to see if debt is preventable
*/

//Normal Bot
//Takes risk
//More dynamic strategies
//Personal valuations
//

public class EasyBot extends Bot {
    private int balance;
    private int networth;  
    private int threshold;
    

    public EasyBot(int startingBalance) {
        this.balance = startingBalance;
        this.threshold = 200;
//        this.propertyValues = new HashMap<>();
    }

    public void updateBalance(int amount) {
        balance += amount;
    }

    public void updateNetWorth(int amount) {
        //- Usually resell price is half house cost
        networth += amount;
    }
    
    public String handleCommand(String command) {
    	return "Buy Mayfair";
    }

    //- Strategypattern?
//    public boolean shouldBuyProperty(String propertyName, int cost) {
//        int perceivedValue = propertyValues.getOrDefault(propertyName, 50);
//        return balance >= cost && perceivedValue >= cost / 2; // simple valuation logic
//    }
//
//    // Still works when in debt, balance and networth for liquation factor
//    public boolean shouldSellProperty(String propertyName) {
//        return balance < threshold; 
//    }
}


