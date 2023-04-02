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

        internal VaultKeep Create(VaultKeep vaultKeepData, string userId)
        {
            VaultKeep vaultKeep = _repo.Create(vaultKeepData);
            Vault vault = _vaultsService.GetOne(vaultKeep.VaultId, userId);
            if(vault.CreatorId != userId) throw new Exception("You cannot do this to me");
            return vaultKeep;
        }

        internal List<VaultKeepViewModel> GetKeeps(int vaultId)
        {
            List<VaultKeepViewModel> keeps = _repo.GetKeepsInVault(vaultId);
            if(keeps == null) throw new Exception("Keep does not exist");
            return keeps;
        }

        internal string Remove(int id, string userId)
        {
            VaultKeep vaultKeep = _repo.GetOne(id);
            if(vaultKeep == null) throw new Exception("No VaultKeep found");
            if(vaultKeep.CreatorId != userId) throw new Exception("Not your VaultKeep");
            if(vaultKeep != null) _repo.Remove(id);
            Console.WriteLine(vaultKeep);
            return "VaultKeep removed";
        }
  }
}