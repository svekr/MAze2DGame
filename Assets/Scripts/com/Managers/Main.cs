public class Main
{
  static public ILogger Logger { get; set; } = null;

  static public com.Managers.Managers Managers { get; private set; }

  static Main()
  {
    Managers = new com.Managers.Managers();
  }
}