using Keepr.Interfaces;
namespace Keepr.Repositories
{
  public class VaultKeepsRepository : IRepository<VaultKeep, int>
  {

    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
        _db = db;
    }
    
    public VaultKeep Create(VaultKeep vaultKeepData)
    {
      string sql = @"
      INSERT INTO vaultKeep
      (creatorId, vaultId, keepId)
      VALUES
      (@creatorId, @vaultId, @keepId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, vaultKeepData);
      vaultKeepData.Id = id;
      return vaultKeepData;
    }

    internal List<VaultKeepViewModel> GetKeepsInVault(int vaultId)
    {
      string sql = @"
      SELECT
      keep.*,
      vaultKeep.id AS VaultKeepId,
      accounts.*
      FROM vaultKeep
      JOIN keep ON vaultKeep.keepId = keep.id
      JOIN accounts ON keep.creatorId = accounts.id
      WHERE vaultId = @vaultId;
      ";
      List<VaultKeepViewModel> keeps = _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, ( vaultKeepViewModel, profile) => 
      {
        vaultKeepViewModel.Creator = profile;
        return vaultKeepViewModel;
      }, new {vaultId}).ToList();
      return keeps;
    }

    public VaultKeep GetOne(int id)
    {
      string sql = @"
      SELECT
      *
      FROM vaultKeep
      WHERE id = @id;
      ";
      VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new{id}).FirstOrDefault();
      return vaultKeep;
    }

    public int Remove(int id)
    {
      string sql = @"
      DELETE
      FROM vaultKeep
      WHERE id = @id;
      ";
      int rows = _db.Execute(sql, new{id});
      return rows;
    }

    public List<VaultKeep> Get()
    {
      throw new NotImplementedException();
    }


    public int Update(VaultKeep updateData)
    {
      throw new NotImplementedException();
    }
    
  }
}