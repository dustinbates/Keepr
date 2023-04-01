namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;
    private readonly VaultsService _vaultsService;
    
    public KeepsService(KeepsRepository repo, VaultsService vaultsService)
    {
        _repo = repo;
        _vaultsService = vaultsService;
    }
    internal Keep CreateKeep(Keep keepData)
    {
      Keep keep = _repo.CreateKeep(keepData);
      return keep;
    }

    internal Keep EditKeep(Keep updateData, string id)
    {
      Keep original = this.GetKeepById(updateData.Id);
      original.Name = updateData.Name != null ? updateData.Name : original.Name;
      original.Description = updateData.Description != null ? updateData.Description : original.Description;
      int rowsAffected = _repo.EditKeep(original);
      if(rowsAffected == 0) throw new Exception("Could not edit this Keep");
      if(rowsAffected > 1) throw new Exception("Something broke man, check the code or something");
      return original;
    }

    internal List<Keep> GetAllKeeps()
    {
     List<Keep> keeps = _repo.GetAllKeeps();
     return keeps;
    }

    internal Keep GetKeepById(int id)
    {
      Keep keep = _repo.GetKeepById(id);
      if(keep == null) throw new Exception("No keep found");
      return keep;
    }

    internal List<Keep> GetProfileKeeps(string id)
    {
      List<Keep> profileKeeps = _repo.GetProfileKeeps(id);
      return profileKeeps;
    }

    internal string Remove(int id, string userId)
    {
      Keep keep = _repo.GetKeepById(id);
      if (keep == null) throw new Exception("No keep found");
      if(keep.CreatorId != userId) throw new Exception("This is not your Keep!");
      _repo.Remove(id);
      return $"Keep {keep.Name} was deleted";
    }


  }
}