namespace CheckoutProj;

/*
 * Given the vague requirement of the task I added some extra "features"
 * - validation for the numeric values => all should be > 0
 * since it doesn't make sense for a price or quantity to be 0 or negative
 * - all the properties but the name are private since the "checkout" doesn't
 *  care about them (this point is how i interpreted the footnote of the task)

 */
public class ItemPrice
{
    public ItemPrice(string itemName, double itemPrice)
    {
        if (string.IsNullOrEmpty(itemName))
        {
            throw new ArgumentException("The item name cannot be null or empty.");
        }
               
        if (itemPrice <= 0)
        {
            throw new ArgumentException("The item price cannot be less or equal to zero.");
        }
        
        Name = itemName;
        UnitPrice = itemPrice;
    }
    
    public ItemPrice(string itemName, double itemPrice, int specialPriceQuantity, double specialPrice)
        : this(itemName, itemPrice)
    {
        if (specialPriceQuantity <= 0)
        {
            throw new ArgumentException("The special price quantity cannot be less or equal to zero.");
        }
        
        if (specialPrice <= 0)
        {
            throw new ArgumentException("The special price cannot be less or equal to zero.");
        }
        
        SpecialPrice = specialPrice;
        SpecialPriceQuantity = specialPriceQuantity;
    }
    
    /// <summary>
    /// The name of the item
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// The standard unit price for the item
    /// </summary>
    private double UnitPrice { get; set; }

    /// <summary>
    /// The special price of the item
    /// </summary>
    private double? SpecialPrice { get; set; }
    
    /// <summary>
    /// The quantity required for the special price
    /// </summary>
    private int? SpecialPriceQuantity { get; set; }
    
    /// <summary>
    /// Returns true if the item has a special price and special price quantity assigned
    /// </summary>
    private bool HasSpecialPrice => SpecialPriceQuantity.HasValue && SpecialPrice.HasValue;
    
    /*
     * Some notes:
     *  since we are already validating both SpecialPriceQuantity and SpecialPrice
     *  in the constructor and the values cannot be changed outside the constructor
     *  I was able to write the function is a clean
     *  (like skipping the > 0 check and checking only for the quantity on GetUnitPrice)
     */
    
    /// <summary>
    /// Given the quantity of the item, returns the total price with discounts included 
    /// </summary>
    /// <param name="quantity">The quantity of the item</param>
    /// <returns>The total price of the item for the given quantity</returns>
    public double GetPrice(int quantity)
        => GetUnitPrice(quantity) + GetSpecialPrice(quantity);
    
    private double GetUnitPrice(int quantity)
        => UnitPrice 
            * (HasSpecialPrice
                ? quantity % SpecialPriceQuantity!.Value
                : quantity);

    private double GetSpecialPrice(int quantity)
        => HasSpecialPrice
            ? (SpecialPrice * (quantity / SpecialPriceQuantity!.Value))!.Value
            : 0;
}