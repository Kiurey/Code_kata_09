using CheckoutProj;

Console.WriteLine("Do you want to use the default prices? (Y/N)");
var rulesFlag = "";
while (rulesFlag != "Y" && rulesFlag != "N")
{
    rulesFlag = Console.ReadLine();
    if (rulesFlag != "Y" && rulesFlag != "N")
    {
        Console.WriteLine("Invalid input. Please enter Y or N.");
    }
}

string? rules = null;
if (rulesFlag == "Y")
{
    rules = "";
    var lastLine = "not_empty_string";
    Console.WriteLine("The first three lines of the rules will be ignore");
    Console.WriteLine("Please enter the rules. Enter an empty line to finish.");
    while (!string.IsNullOrWhiteSpace(lastLine))
    {
        lastLine = Console.ReadLine();
        
        rules += lastLine + "\n";
    }

    rules = rules[..^2];
}
Checkout register = new(rules);
Console.WriteLine("Please enter the items you want to scan. Enter an empty line to finish.");
var items = "not_empty_string";
while (!string.IsNullOrWhiteSpace(items))
{
    items = Console.ReadLine();
    foreach (var item in items)
    {
        register.ScanItem(item.ToString());
    }
}
Console.WriteLine(register.GetTotal());