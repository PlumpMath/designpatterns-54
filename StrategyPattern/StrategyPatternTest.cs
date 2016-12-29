using NUnit.Framework;

namespace StrategyPattern
{
  [TestFixture]
  public class StrategyPatternTest
  {
    [Test]
    public void NexusCostIs20PercentLowerForExistingCustomers()
    {
      var nexusStore = new NexusStore(new ExistingCustomerStrategy(), 100);
      var actual = nexusStore.GetPrice();
      Assert.That(actual, Is.EqualTo(80));
    }

    [Test]
    public void NexusCostIs10PercentGreaterForNewCustomers()
    {
      var nexusStore = new NexusStore(new NewCustomerStrategy(), 100);
      var actual = nexusStore.GetPrice();
      Assert.That(actual, Is.EqualTo(110));
    }

  }

  public class NexusStore
  {
    private readonly ICustomerStrategy customerStrategy;
    private readonly int basePrice;

    public NexusStore(ICustomerStrategy customerStrategy, int basePrice)
    {
      this.customerStrategy = customerStrategy;
      this.basePrice = basePrice;
    }

    public int GetPrice()
    {
      return customerStrategy.CalculateCost(basePrice);
    }
  }

  public interface ICustomerStrategy
  {
    int CalculateCost(int ordinaryNexusPrice);
  }

  public class ExistingCustomerStrategy: ICustomerStrategy
  {
    public int CalculateCost(int ordinaryNexusPrice)
    {
      return (int)(ordinaryNexusPrice - (ordinaryNexusPrice * 0.2));
    }
  }

  public class NewCustomerStrategy : ICustomerStrategy
  {
    public int CalculateCost(int ordinaryNexusPrice)
    {
      return (int)(ordinaryNexusPrice+(ordinaryNexusPrice * 0.1));
    }
  }


}
