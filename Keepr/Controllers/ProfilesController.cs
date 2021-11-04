using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _profilesService;
    private readonly KeepsService _keepsService;
    private readonly VaultsService _vaultsService;
    public ProfilesController(ProfilesService profilesService, KeepsService keepsService, VaultsService vaultsService)
    {
      _profilesService = profilesService;
      _keepsService = keepsService;
      _vaultsService = vaultsService;
    }



    [HttpGet("{profileId}")]

    public ActionResult<Profile> GetById(string profileId)
    {
      try
      {
       Profile profile = _profilesService.GetById(profileId);
       return profile;
      }
      catch (System.Exception e)
      {
          
          return BadRequest(e.Message);
      }
    }

    [HttpGet("{profileId}/keeps")]

    public ActionResult<List<Keep>> GetKeepsByProfileId(string profileId)
    {
      try
      {
       List<Keep> keeps = _keepsService.GetKeepsByProfileId(profileId);
       return keeps;
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpGet("{profileId}/vaults")]

    public async Task<ActionResult<List<Vault>>> GetVaultsByProfileId(string profileId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        List<Vault> vaults = _vaultsService.GetVaultsByProfileId(profileId, userInfo?.Id);
       return vaults;
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}