using Keepr.Interfaces;
namespace Keepr.Repositories
{
    public class VaultsRepository : IRepository<Vault, int>
    {
        private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

    public Vault Create(Vault vaultData)
    {
      string sql = @"
      INSERT INTO vault
      (name, description, creatorId, img)
      VALUES 
      (@name, @description, @creatorId, @img);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, vaultData);
      vaultData.Id = id;
      return vaultData;
    }

    public List<Vault> Get()
    {
      string sql = @"
      SELECT
      vault.*,
      accounts.*
      FROM vault
      JOIN accounts ON vault.creatorId = accounts.id;
      ";
      List<Vault> vaults = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) => 
      {
        vault.Creator = profile;
        return vault;
      }).ToList();
      return vaults;
    }

    public Vault GetOne(int id)
    {
      string sql = @"
      SELECT
      vault.*,
      accounts.*
      FROM vault
      JOIN accounts ON vault.creatorId = accounts.id
      WHERE vault.id = @id;
      ";
      Vault vaults = _db.Query<Vault, Profile, Vault>(sql, (vault, profile) => 
      {
        vault.Creator = profile;
        return vault;
      }, new {id}).FirstOrDefault();
      return vaults;
    }

    public int Remove(int id)
    {
      string sql = @"
      DELETE
      FROM vault
      WHERE id = @id;
      ";
      int rows = _db.Execute(sql, new {id});
      return rows;
    }

    public int Update(Vault updateData)
    {
      string sql = @"
      UPDATE vault
      SET
      name = @name,
      description = @description,
      img = @img,
      isPrivate = @isPrivate
      WHERE id = @id;
      ";
      int rows = _db.Execute(sql, updateData);
      return rows;
    }
  }
}