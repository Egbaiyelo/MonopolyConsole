package monopolyBots;

import java.util.HashMap;
import java.util.Map;
import java.util.ArrayList;
import java.util.List;

import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.databind.node.ObjectNode;

/**
* Keeps track of balance and doesn't want it to go below certain threshold
* Keeps track of networth (assets) to see if debt is preventable
*/

public class EasyBot extends Bot {
    private String name;
	private int balance;
    private int networth;  
    private int threshold;
    private boolean inJail;
    private int jailCount;
    
    private ObjectMapper comms;
    private List<String> nextActions;
    
    private class Property {
        public String name;
        public int cost;
        public int rent;
        public int houses;
        public int hotels;

        public Property() {} 
    }

    public EasyBot(int startingBalance, String name) {
        this.balance = startingBalance;
        this.networth = startingBalance;
        this.threshold = 200; // maybe allow set threshold
        this.inJail = false;
        this.jailCount = 0;
        this.name = name;
        
        this.nextActions = new ArrayList<> ();
        // Maybe add command list so all the setters can package them
        this.comms = new ObjectMapper();
    }

    /** Events
     * Setters with operations */
    
    public void setBalance(int amount) {
        balance += amount;
        networth += amount;
    }

    public void setNetWorth(int amount) {
        //- Usually resell price is half house cost
        networth += amount;
    }
    
    public void setInJail(boolean status) {
    	inJail = status;
    	if (status == true) {
    		// InJail Strategy -> really nothing, have card or wait till 3rd and pay fine
    		// Can't hold cards yet though.
    	}
    }
    
    public String handleCommand(String command) {
    	try {
    		ObjectMapper mapper = new ObjectMapper();
            JsonNode root = mapper.readTree(command);
            String action = root.get("action").asText();

            switch (action) {
//	            case "init":
//	                int balance = root.get("startingBalance").asInt();
//	                return Initialize(balance);

            	// case -> lands on tax, jail, go, commchest/chance (money), someone elses property
	            case "update":
	                String field = root.get("field").asText();
	                JsonNode value = root.get("value");
	                return HandleUpdate(field, value);
	                
	            // case -> lands on fresh tile
	            case "buy":
	                return HandleBuy(root);
	                
	            // case -> lands on own tile
	            case "safe":
	            	return DoSomething();
	            	
	            // case -> no luck, still in jail
	            case "stale":
	            	return HandleInJail();
	            			
	            // Case for cards and all

                default:
                    return "Unknown action: " + action;
            }
    	} catch (Exception e) {
            return "Error parsing command: " + e.getMessage();
        }

    }
    
    /** Service methods
     * Handle queries from game */
    	
    
//    private String Initialize(int startingBalance) {
//    	balance = startingBalance;
//
//    	return "";
//    }
    
    private String HandleUpdate(String field, JsonNode value) {
    	
    	//- error handling for cases
    	switch (field) {
	        case "InJail":
	            this.inJail = value.asBoolean();
	            return "Updated InJail";
	
	        case "balance":
	            this.balance = value.asInt();
	            return "Updated startingBalance";
	
	        case "netWorth":
	            this.networth = value.asInt();
	            return "Updated netWorth";
	
	        default:
	            return "Unknown update field: " + field;
	    }

    }
     
    private String HandleBuy(JsonNode root) {
    	// Bot lands on property
    	// Expect property details only if its empty
    	
    	JsonNode propertiesNode = root.get("property");
    	try {
    		Property prop = comms.treeToValue(propertiesNode, Property.class);
    	} catch (Exception e) {
    		return "nothing to buy?";
    	}
    	
    	return "";
    	
    	
    }
    
    
    private String DoSomething() {
    	return "";
    }
    
    
    
    private String HandleInJail() {
    	// really just sit and wait
    	jailCount += 1;
    	
    	if (jailCount >= 3) {
    		if (balance > 50) return "buyout";
    		else return Quit("life sentence!"); // proven guilty!,
    	}
    	return "wait";
    }
    
    
    /** Behaviour methods 
     * Methods for acting on something */
    
    
    private String Quit(String reason) {
        try {
            ObjectNode response = comms.createObjectNode();
            response.put("status", "quit");
            response.put("reason", reason);
            return comms.writeValueAsString(response);
        } catch (Exception e) {
            return "{\"status\":\"error\",\"message\":\"Failed to generate quit message.\"}";
        }
    }

    //- Strategypattern?
    public boolean shouldBuyProperty(Property prop) {
    	// perceived values for harder bots, networth always greater than balance, all it takes is perceived value greater than certain percentage of cost like (75)
    	int afterValue = balance - prop.cost;
        return threshold <= afterValue; 
    }

    // Still works when in debt, balance and networth for liquation factor
    public boolean shouldSellProperty(String propertyName) {
    	// specifically balance so no need to mortgage later 
        return balance < threshold; 
    }
    
    
    
    
}


