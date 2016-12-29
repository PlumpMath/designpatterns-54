using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace MediatorPattern
{
  public class ChatUser
  {
    private readonly string userName;
    private readonly IChatMediator chatMediator;
    public string LastMessage { get; set; }

    public ChatUser(string userName, IChatMediator chatMediator)
    {
      this.userName = userName;
      this.chatMediator = chatMediator;
      LastMessage = String.Empty;
    }

    public void RegisterUser()
    {
      chatMediator.RegisterUser(this);
    }

    public void NotifyNewUserRegistered(ChatUser user)
    {
      LastMessage = string.Format("{0} is registered!", user.userName);
    }
  }

  public class ChatMediator: IChatMediator
  {
    private readonly List<ChatUser> users = new List<ChatUser>(); 

    public void RegisterUser(ChatUser user)
    {
      foreach (var chatUser in users)
      {
        chatUser.NotifyNewUserRegistered(user);
      }
      users.Add(user);
    }
  }

  public interface IChatMediator
  {
     void RegisterUser(ChatUser user);
  }

  [TestFixture]
  public class MediatorPatternTest
  {
    [Test]
    public void WhenUserLoginsChatSaysHelloToAll()
    {
      var chatMediator = new ChatMediator();
      var chatUser = new ChatUser("Alex", chatMediator);
      chatUser.RegisterUser();
      Assert.That(chatUser.LastMessage, Is.Empty);

      var chatUser2 = new ChatUser("Joe", chatMediator);
      chatUser2.RegisterUser();

      Assert.That(chatUser2.LastMessage, Is.Empty);
      Assert.That(chatUser.LastMessage, Is.Not.Empty);
    }
  }
}
