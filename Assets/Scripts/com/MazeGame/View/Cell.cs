using UnityEngine;
using com.MazeGame.Model;

namespace com.MazeGame.View
{
  public class Cell : MonoBehaviour
  {
    [SerializeField]
    private GameObject _topWall;

    [SerializeField]
    private GameObject _rightWall;

    [SerializeField]
    private GameObject _bottomWall;

    [SerializeField]
    private GameObject _leftWall;

    public void Set(Direction directions)
    {
      _topWall.SetActive((directions & Direction.Up) != Direction.Up);
      _rightWall.SetActive((directions & Direction.Right) != Direction.Right);
      _bottomWall.SetActive((directions & Direction.Down) != Direction.Down);
      _leftWall.SetActive((directions & Direction.Left) != Direction.Left);
    }
  }
}