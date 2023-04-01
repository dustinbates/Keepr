namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly AccountService _accountsService;
        private readonly VaultsService _vaultsService;
        private readonly KeepsService _keepsService;

        public ProfilesController(AccountService accountsService, VaultsService vaultsService, KeepsService keepsService)
        {
            _accountsService = accountsService;
            _vaultsService = vaultsService;
            _keepsService = keepsService;
        }

        [HttpGet("{id}")]
        public ActionResult<Profile> Get(string id)
        {
            try 
            {
              Profile profile = _accountsService.GetById(id);
              return Ok(profile);
            }
            catch (Exception e)
            {
              return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/keeps")]
        public ActionResult<List<Keep>> GetProfileKeeps(string id)
        {
            try 
            {
              List<Keep> keeps = _keepsService.GetProfileKeeps(id);
              return Ok(keeps);
            }
            catch (Exception e)
            {
              return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/vaults")]
        public ActionResult<List<Vault>> GetProfileVaults(string id)
        {
            try 
            {
              List<Vault> vaults = _vaultsService.GetProfileVaults(id);
              return Ok(vaults);
            }
            catch (Exception e)
            {
              return BadRequest(e.Message);
            }
        }


    }
}