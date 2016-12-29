using NUnit.Framework;
using Rhino.Mocks;

namespace CommandPattern
{
  public class CommandInvoker
  {
    private ICommand command;

    public void SetCommand(ICommand command)
    {
      this.command = command;
    }

    public void Execute()
    {
      command.Execute();
    }
  }

  public class Receiver
  {
    public int CommandResult { get; set; }
  }

  public interface ICommand
  {
    void Execute();
  }

  public class MultiplyNumberCommand: ICommand
  {
    private readonly int number;
    private readonly Receiver receiver;

    public MultiplyNumberCommand(int number, Receiver receiver)
    {
      this.number = number;
      this.receiver = receiver;
    }

    public void Execute()
    {
      receiver.CommandResult = number*number;
    }
  }


  [TestFixture]
  public class CommandPatternTest
  {
    [Test]
    public void CommandIsSetAndExecuted()
    {
      var invoker = new CommandInvoker();
      var command = MockRepository.GenerateMock<ICommand>();
      invoker.SetCommand(command);

      invoker.Execute();
      command.AssertWasCalled(x => x.Execute());
    }

    [Test]
    public void CommandPassesTheResultToReceiver()
    {
      var invoker = new CommandInvoker();
      var receiver = new Receiver();
      var command = new MultiplyNumberCommand(2, receiver);
      invoker.SetCommand(command);
      invoker.Execute();
      Assert.That(receiver.CommandResult, Is.EqualTo(4));
    }
  }


}
