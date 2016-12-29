using NUnit.Framework;

namespace BuilderPattern
{
  public class Car
  {
    public bool HasAwd { get; set; }

    public int MaxSpeed { get; set; }
  }

  public abstract class CarBuilder
  {
    protected Car Car;

    public void BuildCar()
    {
      Car = new Car();
      ConfigureAwd();
      ConfigureMaxSpeed();
    }

    protected abstract void ConfigureMaxSpeed();

    protected abstract void ConfigureAwd();

    public Car GetCar()
    {
      return Car;
    }
  }

  public class SuvCarBuilder : CarBuilder
  {

    protected override void ConfigureMaxSpeed()
    {
      Car.MaxSpeed = 190;
    }

    protected override void ConfigureAwd()
    {
      Car.HasAwd = true;
    }

  }

  public class HatchbackCarBuilder : CarBuilder
  {
    protected override void ConfigureMaxSpeed()
    {
      Car.MaxSpeed = 205;
    }

    protected override void ConfigureAwd()
    {
      Car.HasAwd = false;
    }
  }

  public class CarShop
  {
    public Car CreateCar(CarBuilder carBuilder)
    {
      carBuilder.BuildCar();
      return carBuilder.GetCar();
    }

  }


  [TestFixture]
  public class BuilderPatterTest
  {
    [Test]
    public void WillCreateSuv()
    {
      var shop = new CarShop();

      var car = shop.CreateCar(new SuvCarBuilder());

      Assert.That(car.HasAwd, Is.True);
      Assert.That(car.MaxSpeed, Is.EqualTo(190));
    }

    [Test]
    public void WillCreateHatchback()
    {
      var shop = new CarShop();

      var car = shop.CreateCar(new HatchbackCarBuilder());

      Assert.That(car.HasAwd, Is.False);
      Assert.That(car.MaxSpeed, Is.EqualTo(205));
    }
  }
 
}
