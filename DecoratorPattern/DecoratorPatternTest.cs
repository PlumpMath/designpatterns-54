using NUnit.Framework;

namespace DecoratorPattern
{
    public class FlightSeat
    {
      public virtual int GetCost()
      {
        return 100;
      }
    }

    public abstract class FlightSeatDecorator: FlightSeat
    {
      protected readonly FlightSeat Seat;

      protected FlightSeatDecorator(FlightSeat seat)
      {
        Seat = seat;
      }
    }

    public class WiFi: FlightSeatDecorator
    {
      public WiFi(FlightSeat flightSeat):base(flightSeat)
      {
        
      }

      public override int GetCost()
      {
        return Seat.GetCost() + 15;
      }
    }


    public class ExtraMeal : FlightSeatDecorator
    {
      public ExtraMeal(FlightSeat seat):base(seat)
      {
      }

      public override int GetCost()
      {
        return Seat.GetCost() + 30;
      }
    }

    [TestFixture]
    public class DecoratorPatternTest
    {
      [Test]
      public void OrdinaryFlightSeatCostIs100()
      {
        var flightSeat = new FlightSeat();

        Assert.That(flightSeat.GetCost(), Is.EqualTo(100));
      }

      [Test]
      public void CanOrderFlightSeatWithWifi()
      {
        var wiFiFlightSeat = new WiFi(new FlightSeat());
        
        Assert.That(wiFiFlightSeat.GetCost(), Is.EqualTo(115));
      }

      [Test]
      public void CanOrderFlightSeatWithWiFiAndExtraMeal()
      {
        var flightSeat = new ExtraMeal(new WiFi(new FlightSeat()));
        Assert.That(flightSeat.GetCost(), Is.EqualTo(100+15+30));
      }
    }
}
