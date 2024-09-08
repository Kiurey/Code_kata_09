using Xunit;

namespace CheckoutProj;

public class UnitTest
{
    private const string DefaultRules =
        "  Item   Unit      Special\n" +
        "         Price     Price\n" +
        "  --------------------------\n" +
        "    A     50       3 for 130\n" +
        "    B     30       2 for 45\n" +
        "    C     20\n" +
        "    D     15";
    
    [Theory]
    [InlineData(  0, "")]
    [InlineData( 50, "A")]
    [InlineData( 80, "AB")]
    [InlineData(115, "CDBA")]
    [InlineData(100, "AA")]
    [InlineData(130, "AAA")]
    [InlineData(180, "AAAA")]
    [InlineData(230, "AAAAA")]
    [InlineData(260, "AAAAAA")]
    [InlineData(160, "AAAB")]
    [InlineData(175, "AAABB")]
    [InlineData(190, "AAABBD")]
    [InlineData(190, "DABABA")]
    public void TestCorrectTotal(int expectedTotal, string testInput)
    {
        var register = new Checkout(DefaultRules);
        ScanItems(register, testInput);
        Assert.Equal(expectedTotal, register.GetTotal());
    }

    private void ScanItems(Checkout register, string items)
    {
        foreach (var item in items)
        {
            register.ScanItem(item.ToString());
        }
    }

}