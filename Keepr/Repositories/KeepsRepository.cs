namespace Keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;
    
    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }
    internal Keep CreateKeep(Keep keepData)
    {
      string sql = @"
      INSERT INTO keep
      (name, description, creatorId, img)
      VALUES
      (@name, @description, @creatorId, @img);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, keepData);
      keepData.Id = id;
      return keepData;
    }

    internal int EditKeep(Keep original)
    {
      string sql = @"
      UPDATE keep
      SET
      name = @name,
      description = @description
      WHERE id = @id;
      ";
      int rows = _db.Execute(sql, original);
      return rows;
    }

    internal List<Keep> GetAllKeeps()
    {
      string sql = @"
      SELECT
      keep.*,
      accounts.*
      FROM keep
      JOIN accounts ON keep.creatorId = accounts.id;
      ";
      List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) => 
      {
        keep.Creator = profile;
        return keep;
      }).ToList();
      return keeps;
    }

    internal Keep GetKeepById(int id)
    {
      string sql = @"
      SELECT
      keep.*,
      accounts.*
      FROM keep
      JOIN accounts ON keep.creatorId = accounts.id
      WHERE keep.id = @id
      ";
      Keep keep = _db.Query<Keep, Profile, Keep>(sql, (keep, profile) =>
      {
        keep.Creator = profile;
        return keep;
      }, new {id}).FirstOrDefault();
      return keep;
    }

    internal void Remove(int id)
    {
      string sql = @"
      DELETE
      FROM keep
      WHERE id = @id;
      ";
      int rows = _db.Execute(sql, new {id});
      return;
    }
  }
}