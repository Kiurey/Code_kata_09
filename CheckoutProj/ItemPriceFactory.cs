namespace CheckoutProj;

public static class ItemPriceFactory
{
    public static List<ItemPrice> CreateItemPrices(string pricingRules)
    {
        //copied the structure given as an example
        //the skip 3 just remove the column names and the line separator "-----"
        //each remaining line should be a rule of 2 or 5 fields:
        // itemName, unitPrice, itemQuantity, "for" string, specialPrice
        
        var splitRules = pricingRules
            .Split("\n")
            .Skip(3)
            .ToList();

        return splitRules.Select(sr => sr.DeserializeAndCreateItemPrice()).ToList();
    }
    
    private static ItemPrice DeserializeAndCreateItemPrice(this string rule)
    {
        var ruleArray = rule
            .Split(" ")
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .ToArray();

        if (ruleArray.Length is not (2 or 5))
        {
            throw new ArgumentException($"The pricing rule {rule} has an invalid number of parameters.");
        }
        
        var itemName = ruleArray[0];
        
        if (!double.TryParse(ruleArray[1], out var itemPrice))
        {
            throw new ArgumentException($"{ruleArray[1]} is not a valid price");
        };
        
        if(ruleArray.Length == 2)
        {
            return new ItemPrice(itemName, itemPrice);
        }
        
        if (!int.TryParse(ruleArray[2], out var specialPriceQuantity))
        {
            throw new ArgumentException($"{ruleArray[2]} is not a valid item quantity");
        };
        
        if (!double.TryParse(ruleArray[4], out var specialPrice))
        {
            throw new ArgumentException($"{ruleArray[4]} is not a valid (special) price");
        };
        
        return new ItemPrice(itemName, itemPrice, specialPriceQuantity, specialPrice);
    }
}