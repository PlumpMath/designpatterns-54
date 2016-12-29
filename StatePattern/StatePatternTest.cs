using NUnit.Framework;

namespace StatePattern
{
  public abstract class CarGearState
  {
    public abstract bool CanGearUp();

    public abstract bool CanReverse();
  }

  public class NeutralGearState : CarGearState
  {
    public override bool CanGearUp()
    {
      return true;
    }

    public override bool CanReverse()
    {
      return true;
    }
  }

  public class FirstGearState : CarGearState
  {
    public override bool CanGearUp()
    {
      return false;
    }

    public override bool CanReverse()
    {
      return false;
    }
  }

  public class ReverseGearState : CarGearState
  {
    public override bool CanGearUp()
    {
      return true;
    }

    public override bool CanReverse()
    {
      return false;
    }
  }

  public class Car
  {
    private CarGearState carGearState;

    public Car()
    {
      this.carGearState = new NeutralGearState();
    }

    public bool CanGearUp()
    {
      return carGearState.CanGearUp();
    }

    public bool CanReverse()
    {
      return carGearState.CanReverse();
    }

    public void SetState(CarGearState gearState)
    {
      carGearState = gearState;
    }
  }


  [TestFixture]
  public class NeutralStateTests
  {
    [Test]
    public void CarCanGearUpFromNeutralState()
    {
      var car = new Car();
      Assert.That(car.CanGearUp(), Is.True);
    }

    [Test]
    public void CarCanGearDownFromNeutralState()
    {
      var car = new Car();
      Assert.That(car.CanReverse(), Is.True);
    }
  }

  [TestFixture]
  public class FirstGearStateTests
  {
    [Test]
    public void CarCannotGearUpFromFirstGear()
    {
      var car = new Car();
      car.SetState(new FirstGearState());
      Assert.That(car.CanGearUp(), Is.False);
    }

    [Test]
    public void CarCanGearGoReverseFromFirstGear()
    {
      var car = new Car();
      car.SetState(new FirstGearState());
      Assert.That(car.CanReverse(), Is.False);
    }
  }

  [TestFixture]
  public class ReverseGearStateTests
  {
    [Test]
    public void CarCanGearUpFromReverseGear()
    {
      var car = new Car();
      car.SetState(new ReverseGearState());
      Assert.That(car.CanGearUp(), Is.True);
    }

    [Test]
    public void CarCannotGearGoReverseFromReverseGear()
    {
      var car = new Car();
      car.SetState(new ReverseGearState());
      Assert.That(car.CanReverse(), Is.False);
    }
  }

}
