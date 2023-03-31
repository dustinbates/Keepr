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

    public Vault Create(Vault data)
    {
      throw new NotImplementedException();
    }

    public List<Vault> Get()
    {
      throw new NotImplementedException();
    }

    public Vault GetOne(int id)
    {
      throw new NotImplementedException();
    }

    public int Remove(int id)
    {
      throw new NotImplementedException();
    }

    public int Update(Vault updateData)
    {
      throw new NotImplementedException();
    }
  }
}