using System;
using com.MazeGame.Model;

namespace com.MazeGame.Controller
{
  public class MazeModelCreator
  {
    private Random _random = new Random();

    public Vertex[,] CreateMaze(int fieldSizeX, int fieldSizeY, int gatesCount = 1)
    {
      Vertex[,] vertexes = CreateVertexes(fieldSizeX, fieldSizeY);
      RandomDFS(vertexes, vertexes[0, 0]);
      return vertexes;
    }

    private Vertex[,] CreateVertexes(int fieldSizeX, int fieldSizeY)
    {
      Vertex[,] result = new Vertex[fieldSizeX, fieldSizeY];
      for (int i = 0; i < fieldSizeX; i++)
      {
        for (int j = 0; j < fieldSizeY; j++)
        {
          result[i, j] = new Vertex(i, j);
        }
      }
      return result;
    }

    private void RandomDFS(Vertex[,] vertexes, Vertex vertex)
    {
      vertex.visited = true;
      Direction direction = GetRandomUnvisitedNeighbourDirection(vertexes, vertex);
      Vertex nextVertex = GetNeighbour(vertexes, vertex, direction);
      while (nextVertex != null)
      {
        ConnectVertexes(vertex, nextVertex, direction);
        RandomDFS(vertexes, nextVertex);
        direction = GetRandomUnvisitedNeighbourDirection(vertexes, vertex);
        nextVertex = GetNeighbour(vertexes, vertex, direction);
      }
    }

    private Direction GetRandomUnvisitedNeighbourDirection(Vertex[,] vertexes, Vertex vertex)
    {
      Array directions = Enum.GetValues(typeof(Direction));
      Shuffle(directions);
      for (int i = 0; i < directions.Length; i++)
      {
        Direction direction = (Direction)directions.GetValue(i);
        Vertex neighbour = GetNeighbour(vertexes, vertex, direction);
        if (neighbour != null && !neighbour.visited)
        {
          return direction;
        }
      }
      return Direction.None;
    }

    private void Shuffle(Array array)
    {
      int n = array.Length;
      while (n > 1)
      {
        n--;
        int k = _random.Next(n + 1);
        object value = array.GetValue(k);
        array.SetValue(array.GetValue(n), k);
        array.SetValue(value, n);
      }
    }

    private Vertex GetNeighbour(Vertex[,] vertexes, Vertex vertex, Direction direction)
    {
      switch (direction)
      {
        case Direction.Up:
          if (vertex.y + 1 < vertexes.GetLength(1))
          {
            return vertexes[vertex.x, vertex.y + 1];
          }
          return null;
        case Direction.Right:
          if (vertex.x + 1 < vertexes.GetLength(0))
          {
            return vertexes[vertex.x + 1, vertex.y];
          }
          return null;
        case Direction.Down:
          if (vertex.y > 0)
          {
            return vertexes[vertex.x, vertex.y - 1];
          }
          return null;
        case Direction.Left:
          if (vertex.x > 0)
          {
            return vertexes[vertex.x - 1, vertex.y];
          }
          return null;
        default:
          return null;
      }
    }

    private void ConnectVertexes(Vertex vertex1, Vertex vertex2, Direction direction)
    {
      vertex1.directions |= direction;
      vertex2.directions |= InverseDirection(direction);
    }

    private Direction InverseDirection(Direction direction)
    {
      switch (direction)
      {
        case Direction.Up:
          return Direction.Down;
        case Direction.Right:
          return Direction.Left;
        case Direction.Down:
          return Direction.Up;
        case Direction.Left:
          return Direction.Right;
        default:
          return direction;
      }
    }
  }
}