using System;
using NUnit.Framework;

namespace ChainOfResponsibility
{
  public abstract class SpamHandler
  {
    protected SpamHandler NextSpamHandler;

    public void RegisterNext(SpamHandler next)
    {
      NextSpamHandler = next;
    }

    public abstract bool CheckIfTextIsSpam(string text);
  }

  public class EmptyTextSpamHandler: SpamHandler
  {
    public override bool CheckIfTextIsSpam(string text)
    {
      return text == String.Empty || NextSpamHandler.CheckIfTextIsSpam(text);
    }
  }

  public class DollarSignSpamHandler: SpamHandler
  {
    public override bool CheckIfTextIsSpam(string text)
    {
      return text.Contains("$") || NextSpamHandler.CheckIfTextIsSpam(text);
    }
  }

  public class EndOfChainSpamHandler: SpamHandler
  {
    public override bool CheckIfTextIsSpam(string text)
    {
      return false;
    }
  }

  public class SpamChecker
  {
    private readonly SpamHandler firstSpamHandler;

    public SpamChecker()
    {
      firstSpamHandler = new DollarSignSpamHandler();
      var secondHandler = new EmptyTextSpamHandler();
      var endOfChainHandler = new EndOfChainSpamHandler();

      firstSpamHandler.RegisterNext(secondHandler);
      secondHandler.RegisterNext(endOfChainHandler);
    }

    public bool CheckIfTextIsSpam(string text)
    {
      return firstSpamHandler.CheckIfTextIsSpam(text);
    }
  }


  [TestFixture]
  public class ChainOfResponsibilityTest
  {
    [Test]
    public void TextWithDollarSignIsSpam()
    {
      var spamChecker = new SpamChecker();
      var actual = spamChecker.CheckIfTextIsSpam("I am spam because I have $ sign.");
      Assert.That(actual, Is.True);

    }

    [Test]
    public void EmptyTextIsSpam()
    {
      var spamChecker = new SpamChecker();
      var actual = spamChecker.CheckIfTextIsSpam("");
      Assert.That(actual, Is.True);
    }

    [Test]
    public void OrdinaryTextIsNotSpam()
    {
      var spamChecker = new SpamChecker();
      var actual = spamChecker.CheckIfTextIsSpam("I am not spam!");
      Assert.That(actual, Is.False);
    }
  }
}
