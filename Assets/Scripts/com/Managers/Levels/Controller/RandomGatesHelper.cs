using System;
using System.Collections.Generic;
using com.MazeGame.Model;
using Extensions;
using Utils;

namespace com.Managers.Levels.Controller
{
  public class RandomGatesHelper
  {
    private readonly List<CellModel> _gatesCandidates = new List<CellModel>();
    private int _gatesCount;

    public void Initialize(int gatesCount)
    {
      _gatesCandidates.Clear();
      _gatesCount = gatesCount;
    }

    public void AddGates(CellModel[,] cells)
    {
      if (_gatesCandidates.Count >= _gatesCount)
      {
        _gatesCandidates.Clear();
        return;
      }
    }

    public void TryAddGatesCandidate(CellModel cell, CellModel[,] cells)
    {
      if (_gatesCandidates.Count < _gatesCount) {
        cell.Gates = GetGatesDirection(cell, cells);
        if (cell.Gates != Direction.None) {
          _gatesCandidates.Add(cell);
        }
      }
    }

    private Direction GetGatesDirection(CellModel cell, CellModel[,] cells) {
      Array directions = Enum.GetValues(typeof(Direction));
      directions.Shuffle();
      for (int i = 0; i < directions.Length; i++)
      {
        Direction direction = (Direction)directions.GetValue(i);
        CellModel neighbour = FieldModelUtil.GetNeighbourCell(cell, direction, cells);
        if (neighbour == null && direction != Direction.None)
        {
          return direction;
        }
      }
      return Direction.None;
    }
  }
}