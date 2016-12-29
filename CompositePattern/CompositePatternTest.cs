using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace CompositePattern
{
  public class Tree : ITreeItem
  {
    public Tree()
    {
      TreeItems = new List<ITreeItem>();
    }

    public string Caption { get; set; }

    public List<string> Render(int level)
    {
      var leftIndent = new string('-',level);

      var output = new List<string> { leftIndent + Caption };

      foreach (var treeItem in TreeItems)
      {
        output.AddRange(treeItem.Render(level + 1));
      }
      return output;
    }

    public List<ITreeItem> TreeItems { get; set; }
  }

  public interface ITreeItem
  {
    string Caption { get; set; }
    List<string> Render(int level);
  }

  [TestFixture]
  public class CompositePatternTest
  {
    [Test]
    public void RenderTreeProducesCorrectOutput()
    {
      var tree = new Tree
        {
          Caption = "Root",
          TreeItems =
            new List<ITreeItem>
              {
                new Tree
                  {
                    Caption = "Group1",
                    TreeItems =
                      new List<ITreeItem>
                        {
                          new Tree {Caption = "Group1Element1"}, 
                          new Tree {Caption = "Group1Element2"}
                        }
                  },
                new Tree
                  {
                    Caption = "ElementAfterGroup1"
                  }
              }
        };

      var actual = tree.Render(0);
        
      Assert.That(actual[0], Is.EqualTo("Root"));
      Assert.That(actual[1], Is.EqualTo("-Group1"));
      Assert.That(actual[2], Is.EqualTo("--Group1Element1"));
      Assert.That(actual[3], Is.EqualTo("--Group1Element2"));
      Assert.That(actual[4], Is.EqualTo("-ElementAfterGroup1"));
    }
  }

}
