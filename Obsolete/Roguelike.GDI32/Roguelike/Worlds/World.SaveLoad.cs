using System;

namespace Roguelike.Worlds
{
  partial class World
  {
    /// <summary>
    /// Загрузка с диска.
    /// </summary>
    /// <param name="fileName">имя файла</param>
    public static World Load(string fileName)
    {
      throw new NotImplementedException();
      /*var snapshot = XmlHelper.Deserialize<SnapshotWorld>(fileName);
      return new World(snapshot);*/
    }

    /// <summary>
    /// Сохранение на диск.
    /// </summary>
    /// <param name="fileName">имя файла</param>
    public void Save(string fileName)
    {
      throw new NotImplementedException();
      /*var snapshot = getSnapshot();
      XmlHelper.SerializeFile(snapshot, fileName);*/
    }
  }
}
