namespace Keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;
        private readonly VaultsService _vaultsService;

        public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService)
        {
            _repo = repo;
            _vaultsService = vaultsService;
        }

        internal VaultKeep Create(VaultKeep vaultKeepData)
        {
            VaultKeep vaultKeep = _repo.Create(vaultKeepData);
            return vaultKeep;
        }

        internal List<VaultKeepViewModel> GetKeeps(int vaultId)
        {
            List<VaultKeepViewModel> keeps = _repo.GetKeepsInVault(vaultId);
            return keeps;
        }

        internal string Remove(int id, string userId)
        {
            VaultKeep vaultKeep = _repo.GetOne(id);
            if(vaultKeep == null) throw new Exception("No VaultKeep found");
            if(vaultKeep.CreatorId != userId) throw new Exception("Not your VaultKeep");
            _repo.Remove(id);
            return "VaultKeep removed";

        }
  }
}