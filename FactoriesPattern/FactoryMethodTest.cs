using NUnit.Framework;

namespace FactoriesPattern
{
  public class Notebook
  {
    public string Manufacturer { get; set; }
    public bool HasHdmi { get; set; }
  }

  public abstract class NotebookFactory
  {
    protected abstract Notebook CreateSpecificNotebook();

    public Notebook CreateNotebook()
    {
      var notebook = CreateSpecificNotebook();
      notebook.HasHdmi = true;

      return notebook;
    }
  }

  public class HpNotebookFactory: NotebookFactory
  {
    protected override Notebook CreateSpecificNotebook()
    {
      return new Notebook {Manufacturer = "HP"};
    }
  }

  public class DellNotebookFactory: NotebookFactory
  {
    protected override Notebook CreateSpecificNotebook()
    {
      return new Notebook {Manufacturer = "Dell"};
    }
  }

  [TestFixture]
  public class FactoryMethodTest
  {
    [Test]
    public void DelegatesObjectCreationToHpNotebookFactory()
    {
      NotebookFactory factory = new HpNotebookFactory();
      var notebook = factory.CreateNotebook();

      Assert.That(notebook.Manufacturer, Is.EqualTo("HP"));
      Assert.That(notebook.HasHdmi, Is.True);
    }

    [Test]
    public void DelegatesObjectCreationToDellNotebookFactory()
    {
      NotebookFactory factory = new DellNotebookFactory();
      var notebook = factory.CreateNotebook();

      Assert.That(notebook.Manufacturer, Is.EqualTo("Dell"));
      Assert.That(notebook.HasHdmi, Is.True);
    }
  }
}
