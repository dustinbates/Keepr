namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vaultsService;
        private readonly VaultKeepsService _vaultKeepsService;
        private readonly Auth0Provider _auth;
        
        public VaultsController(VaultsService vaultsService, VaultKeepsService vaultKeepsService, Auth0Provider auth)
        {
            _vaultsService = vaultsService;
            _vaultKeepsService = vaultKeepsService;
            _auth = auth;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Vault>> Create([FromBody] Vault vaultData)
        {
            try 
            {
              Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
              vaultData.CreatorId = userInfo.Id;
              Vault vault = _vaultsService.Create(vaultData);
              vault.Creator = userInfo;
              return Ok(vault);
            }
            catch (Exception e)
            {
              return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vault>> GetOne(int id)
        {
            try 
            {
              Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
              Vault vaults = _vaultsService.GetOne(id, userInfo?.Id);
              return Ok(vaults);
            }
            catch (Exception e)
            {
              return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/keeps")]
        public async Task<ActionResult<List<VaultKeepViewModel>>> GetKeepsInVault(int id)
        {
          try 
          {
            Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
            Vault vault = _vaultsService.GetOne(id, userInfo?.Id);
            List<VaultKeepViewModel> keeps = _vaultKeepsService.GetKeeps(id);
            return Ok(keeps);
          }
          catch (Exception e)
          {
            return BadRequest(e.Message);
          }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Vault>> Update([FromBody] Vault updateData, int id)
        {
            try 
            {
              Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
              updateData.CreatorId = userInfo.Id;
              updateData.Id = id;
              Vault vault = _vaultsService.Update(updateData);
              return Ok(vault);
            }
            catch (Exception e)
            {
              return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> Remove(int id)
        {
            try 
            {
              Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
              string message = _vaultsService.Remove(id, userInfo.Id);
              return Ok(message);
            }
            catch (Exception e)
            {
              return BadRequest(e.Message);
            }
        }




    }
}