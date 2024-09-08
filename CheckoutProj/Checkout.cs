namespace CheckoutProj;

public class Checkout
{
    private readonly Dictionary<string, int> _boughtItems;
    private readonly Dictionary<string, ItemPrice> _itemPrices; 

    public Checkout(string? pricingRules = null)
    {
        _boughtItems = new Dictionary<string, int>();
        _itemPrices = new Dictionary<string, ItemPrice>();
        CompileItemPrices(pricingRules?? DefaultRules);
    }

    public void ScanItem(string itemName)
    {
        if (!_itemPrices.ContainsKey(itemName))
        {
            throw new ArgumentException("the item does not exist");
        }

        if (!_boughtItems.TryAdd(itemName, 1))
        {
            _boughtItems[itemName]++;
        }
    }

    public double GetTotal()
    => _boughtItems
        .Sum(item => _itemPrices[item.Key].GetPrice(item.Value));
    
    private void CompileItemPrices(string pricingRules)
    {
        var itemPrices = ItemPriceFactory.CreateItemPrices(pricingRules);
        
        // for each item price, add it to the dictionary with its name as key
        itemPrices.ForEach(ip => _itemPrices.Add(ip.Name, ip));
    }
    
    private const string DefaultRules =
        "  Item   Unit      Special\n" +
        "         Price     Price\n" +
        "  --------------------------\n" +
        "    A     50       3 for 130\n" +
        "    B     30       2 for 45\n" +
        "    C     20\n" +
        "    D     15";
}