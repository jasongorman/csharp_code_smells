namespace Demo.Test;

public class CarpetQuoteTests
{
    [Test]
    public void QuoteForFittedCarpetWithNoAreaRounding()
    {
        CarpetQuote quote = new CarpetQuote();
        
        Assert.That(
            quote.Calculate(5.25, 4.5, 10.0, false), 
            Is.EqualTo(236.25));
    }
    
    [Test]
    public void QuoteForFittedCarpetWithAreaRounding()
    {
        CarpetQuote quote = new CarpetQuote();
        
        Assert.That(
            quote.Calculate(5.25, 4.5, 10.0, true), 
            Is.EqualTo(240.0));
    }
}