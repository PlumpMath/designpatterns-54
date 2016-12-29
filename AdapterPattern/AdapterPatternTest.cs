using NUnit.Framework;
using Rhino.Mocks;

namespace AdapterPattern
{
  public abstract class Target
  {
    public abstract string ConcatinateAndCapitalizeString(string firstName, string lastName);
  }

  public interface IAdaptee
  {
    string CapitalizeString(string input);
  }


  public class Adaptee : IAdaptee
  {
    public string CapitalizeString(string input)
    {
      return input.ToUpper();
    }
  }

  public class Adapter : Target
  {
    private readonly IAdaptee _adaptee;

    public Adapter()
      : this(new Adaptee()) { } 

    public Adapter(IAdaptee adaptee)
    {
      _adaptee = adaptee;
    }

    public override string ConcatinateAndCapitalizeString(string firstName, string lastName)
    {
      var adaptedInput = firstName + " " + lastName;

      return _adaptee.CapitalizeString(adaptedInput);
    }
  }

  [TestFixture]
  public class AdapterPatternTest
  {
    
    [Test]
    public void ConcatinatesStringsAndCapitalizesThem()
    {
      Target adapter = new Adapter();
      var result = adapter.ConcatinateAndCapitalizeString("FirstName", "LastName");
      Assert.That(result, Is.EqualTo("FIRSTNAME LASTNAME"));
    }

    [Test]
    public void AdapterCallsAdapteeMethod()
    {
      var adaptee = MockRepository.GenerateMock<IAdaptee>();
      Target adapter = new Adapter(adaptee);
      adapter.ConcatinateAndCapitalizeString("FirstName", "LastName");
      adaptee.AssertWasCalled(x => x.CapitalizeString(Arg<string>.Is.Equal("FirstName LastName")));
    }
  }
}
