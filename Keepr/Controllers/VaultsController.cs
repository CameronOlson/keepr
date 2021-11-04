using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vaultsService;
    private readonly VaultKeepsService _vaultKeepService;

    public VaultsController(VaultsService vaultsService, VaultKeepsService vaultKeepService)
    {
      _vaultsService = vaultsService;
      _vaultKeepService = vaultKeepService;
    }

    [HttpPost]
    [Authorize]

    public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault newVault)
    {
     try
     {
      Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
      newVault.CreatorId = userInfo.Id;
      newVault.Creator = userInfo;
      Vault createdVault = _vaultsService.CreateVault(newVault);
      
      return Ok(newVault);
     }
     catch (System.Exception e)
     {
       return BadRequest(e.Message);
     }
    }

   
    [HttpGet("{vaultId}")]
    public async Task<ActionResult<Keep>> GetVaultById(int vaultId)
    {
      try
      {
      Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
       Vault vault = _vaultsService.GetAuthVaultById(vaultId, userInfo?.Id);
       return Ok(vault);

      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{vaultId}/keeps")]
    public async Task<ActionResult<List<VaultKeepViewModel>>> GetKeepsByVaultId(int vaultId)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        List<VaultKeepViewModel> vaultKeeps = _vaultsService.GetKeepsByVaultId(vaultId, userInfo?.Id);
        return Ok(vaultKeeps);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpPut("{vaultId}")]
    [Authorize]

    public async Task<ActionResult<Keep>> UpdateVault(int vaultId, [FromBody] Vault updatedVault)
    {
      try
      {
      Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
      updatedVault.CreatorId = userInfo.Id;
      updatedVault.Creator = userInfo;
      updatedVault.Id = vaultId;
      return Ok (_vaultsService.UpdateVault(updatedVault));
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete("{vaultId}")]
    [Authorize]

    public async Task<ActionResult<Vault>> DeleteVault(int vaultId)
    {
      try
      {
       Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
       _vaultsService.Delete(vaultId, userInfo.Id);
       return Ok("This was deleted");
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}