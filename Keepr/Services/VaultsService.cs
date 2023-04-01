namespace Keepr.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _repo;
        
        public VaultsService(VaultsRepository repo)
        {
            _repo = repo;
        }

        internal Vault Create(Vault vaultData)
        {
            Vault vault = _repo.Create(vaultData);
            return vault;
        }

    internal List<Vault> Get()
    {
      List<Vault> vaults = _repo.Get();
      return vaults;
    }

    internal Vault GetOne(int id, string userId)
    {
      Vault vault = _repo.GetOne(id);
      if(vault == null) throw new Exception($"No vault at id: {id}");
      if(vault.CreatorId != userId && vault.IsPrivate == true) throw new Exception("This Vault is private, shhhhhh");
      return vault;
    }

    internal Vault Update(Vault updateData)
    {
      Vault original = this.GetOne(updateData.Id, updateData.CreatorId);
      original.Name = updateData.Name == null ? original.Name : updateData.Name;
      original.IsPrivate = updateData.IsPrivate != null ? updateData.IsPrivate : original.IsPrivate;
      _repo.Update(original);
      return original;
    }

    internal string Remove(int id, string userId)
    {
        Vault vault = this.GetOne(id, userId);
        if(vault.CreatorId != userId) throw new Exception("This is not your vault!");
        _repo.Remove(id);
        return $"The vault {vault.Name} was deleted.";
    }


  }
}