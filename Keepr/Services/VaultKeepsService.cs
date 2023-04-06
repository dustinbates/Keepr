namespace Keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;
        
        private readonly KeepsRepository _keepsRepo;
        private readonly VaultsService _vaultsService;
        private readonly KeepsService _keepsService;

        public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService, KeepsService keepsService, KeepsRepository keepsRepository)
        {
            _repo = repo;
            _vaultsService = vaultsService;
            _keepsService = keepsService;
            _keepsRepo = keepsRepository;
        }

        internal VaultKeep Create(VaultKeep vaultKeepData, string userId)
        {
            VaultKeep vaultKeep = _repo.Create(vaultKeepData);
            Vault vault = _vaultsService.GetOne(vaultKeep.VaultId, userId);
            if(vault.CreatorId != userId) throw new Exception("You cannot do this to me");
            // TODO throw to your keeps repo to call a function that will increase your kepts 
            _keepsRepo.KeptCount(vaultKeepData.KeepId);
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