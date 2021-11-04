using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vaultKeepsService;

    public VaultKeepsController(VaultKeepsService vaultKeepsService)
    {
      _vaultKeepsService = vaultKeepsService;
    }

    [HttpPost]
    [Authorize]

    public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep newVaultKeep)
    {
      try
      {
       Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
       VaultKeep createdVaultKeep = _vaultKeepsService.CreateVaultKeep(newVaultKeep, userInfo.Id);
       return Ok(newVaultKeep);
      }
      catch (System.Exception e)
      {
       return BadRequest(e.Message);
      }
    }
    [HttpDelete("{vaultKeepId}")]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> DeleteVaultKeep(int vaultKeepId)
    {
      try
      {
       Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
       _vaultKeepsService.DeleteVaultKeep(userInfo.Id, vaultKeepId);
       return Ok("this was deleted");
      }
      catch (System.Exception e)
      {
       return BadRequest(e.Message);
      }
    }
    // [HttpPut("{vaultKeepId}")]
    // [Authorize]
    // public async Task<ActionResult<VaultKeep>> EditVaultKeep(int vaultKeepId)

  
  }
}