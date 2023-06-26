namespace com.MazeGame.Model
{
  public class Vertex
  {
    public Vertex() { }

    public Vertex(int x, int y) : this()
    {
      this.x = x;
      this.y = y;
    }

    public bool visited = false;
    public int x = 0;
    public int y = 0;
    public Direction directions = Direction.None;
  }
}