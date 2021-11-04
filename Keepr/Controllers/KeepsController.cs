using System.Collections.Generic;
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
  public class KeepsController : ControllerBase
  {
    private readonly KeepsService _keepsService;

    public KeepsController(KeepsService keepsService)
    {
      _keepsService = keepsService;
    }

    [HttpGet]
    public ActionResult<List<Keep>> GetKeeps()
    {
      try
      {
       List<Keep> keeps = _keepsService.GetKeeps();
       return Ok(keeps);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{keepId}")]
    public ActionResult<Keep> GetKeepById(int keepId)
    {
      try
      {
        Keep keep = _keepsService.GetKeepById(keepId);
        return Ok(keep);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep data)
    {
      try
      {
       var userInfo = await HttpContext.GetUserInfoAsync<Account>();
      //  NOTE this might need to go to the service layer
       data.CreatorId = userInfo.Id;
       data.Creator = userInfo;
       Keep keep = _keepsService.CreateKeep(data);
       return Ok(keep);
      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut("{keepId}")]
    [Authorize]

    public async Task<ActionResult<Keep>> UpdateKeep(int keepId, [FromBody] Keep updatedKeep)
    {
      try
      {
      Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
      updatedKeep.CreatorId = userInfo.Id;
      updatedKeep.Creator = userInfo;
      updatedKeep.Id = keepId;
      return Ok (_keepsService.UpdateKeep(updatedKeep));

      }
      catch (System.Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpDelete("{keepId}")]
    [Authorize]
    public async Task<ActionResult<Keep>> RemoveKeep(int keepId)
    {
      try
      {
       Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
       _keepsService.Delete(keepId, userInfo.Id);
       return Ok("this was deleted");
      }
      catch (System.Exception e)
      {
          
          return BadRequest(e.Message);
      }
    }
  }
}