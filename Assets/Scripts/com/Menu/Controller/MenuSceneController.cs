using com.Managers.SceneManagement;

namespace com.Menu.Controller
{
  public class MenuSceneController: SceneController
  {
    override protected void InitLogger()
    {
      if (Main.Logger == null)
      {
        Main.Logger = new UnityLogger();
      }
      Logger = Main.Logger;
    }
  }
}