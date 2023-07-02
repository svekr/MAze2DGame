using com.Managers.Levels.Model;

namespace com.MazeGame.Model
{
  public class LevelModel: ILevelModel
  {
    public int Id { get; private set; } = 0;

    public CellModel[,] FieldModel { get; private set; } = null;

    public int Duration { get; private set; } = 0;

    public LevelModel(int id, CellModel[,] fieldModel, int duration) {
      Id = id;
      FieldModel = fieldModel;
      Duration = duration;
    }
  }
}