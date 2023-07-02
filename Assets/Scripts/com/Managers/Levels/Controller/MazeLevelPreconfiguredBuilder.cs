using System;
using com.MazeGame.Model;
using com.Settings;

namespace com.Managers.Levels.Controller
{
  public class MazeLevelPreconfiguredBuilder: ILevelBuilder<LevelModel>
  {
    public bool TryBuildLevel(object levelSettings, out LevelModel level) {
      level = null;
      LevelPreconfiguredSettings settings = levelSettings as LevelPreconfiguredSettings;
      if (settings == null) {
        return false;
      }
      CellModel[,] fieldModel = CreateMaze(settings.Cells);
      level = new LevelModel(settings.Id, fieldModel, settings.Duration);
      return true;
    }

    private CellModel[,] CreateMaze(CellSettings[] cells)
    {
      int count = cells?.GetLength(0) ?? 0;
      if (count == 0) {
        return null;
      }

      int maxX = 0;
      int maxY = 0;
      CellModel[] models = new CellModel[count];
      CellSettings cell;
      for (int i = 0; i < count; i++) {
        cell = cells[i];
        models[i] = new CellModel(cell.index.x, cell.index.y, cell.directions, cell.gates);
        maxX = Math.Max(maxX, cell.index.x);
        maxY = Math.Max(maxY, cell.index.y);
      }

      CellModel[,] result = new CellModel[maxX + 1, maxY + 1];
      foreach (CellModel model in models)
      {
        result[model.Index.x, model.Index.y] = model;
      }

      return result;
    }
  }
}