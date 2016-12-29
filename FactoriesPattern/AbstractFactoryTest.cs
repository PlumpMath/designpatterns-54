using NUnit.Framework;

namespace FactoriesPattern
{
  public class Bottle
  {
    public double AlcoholStrenght { get; set; }
  }

  public abstract class AlcoholFactory
  {
    public abstract Bottle CreateAlcohol();
  }

  public class BeerFactory : AlcoholFactory
  {
    public override Bottle CreateAlcohol()
    {
      return new Bottle {AlcoholStrenght = 4.5};
    }
  }

  public class WineFactory: AlcoholFactory
  {
    public override Bottle CreateAlcohol()
    {
      return new Bottle {AlcoholStrenght = 20.0};
    }
  }

  [TestFixture]
  public class AbstractFactoryTest
  {
    [Test]
    public void WineFactoryCreatesABottleOfWine()
    {
      AlcoholFactory factory = new WineFactory();

      var actual = factory.CreateAlcohol();

      Assert.That(actual.AlcoholStrenght, Is.EqualTo(20.0));
    }

    [Test]
    public void BeerFactoryCreatesABottleOfBeer()
    {
      AlcoholFactory factory = new BeerFactory();

      var actual = factory.CreateAlcohol();

      Assert.That(actual.AlcoholStrenght, Is.EqualTo(4.5));
    }
  }
}
