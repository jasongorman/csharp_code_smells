namespace MessageChains.Test
{
    [TestFixture]
    public class InvoiceTests {
	
	    [Test]
	    public void ShippingShouldBeAddedIfAddressIsNotInEurope() {
    		
		    Address address = new Address(new Country(false));
		    Customer customer = new Customer(address);
    		
		    Invoice invoice = new Invoice(customer);
		    invoice.AddItem(new InvoiceItem("Product X", 1, 100));
    		
		    Assert.That(invoice .TotalPrice, Is.EqualTo(100 + Invoice.ShippingCostOutsideEu));
	    }

    }
}
