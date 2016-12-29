using NUnit.Framework;
using Rhino.Mocks;

namespace BridgePattern
{
  public interface ITiresChangerImplementator
  {
    void ChangeTiresImplementatorOperation(VehicleAbstraction vehicleAbstraction);
  }

  public abstract class VehicleAbstraction
  {
    public ITiresChangerImplementator TiresChangerImplementator { get; set; }

    public abstract void ChangeTires();
  }

  public class Sedan : VehicleAbstraction
  {
    public override void ChangeTires()
    {
      TiresChangerImplementator.ChangeTiresImplementatorOperation(this);
    }
  }

  [TestFixture]
  public class BridgeTest
  {
    [Test]
    public void BridgeInvokesImplementatorOperation()
    {
      VehicleAbstraction sedan = new Sedan();
      var implementator = MockRepository.GenerateMock<ITiresChangerImplementator>();
      sedan.TiresChangerImplementator = implementator;
      sedan.ChangeTires();
      implementator.AssertWasCalled(x => x.ChangeTiresImplementatorOperation(Arg<VehicleAbstraction>.Is.Equal(sedan)));
    }
  }


}
