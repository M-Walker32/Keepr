using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [Route("api/[controller]")]
  public class VaultKeepsController : Controller
  {
    private readonly VaultKeepsService _vks;
    private readonly VaultService _vs;
    private readonly KeepService _ks;
    public VaultKeepsController(VaultKeepsService vks, VaultService vs, KeepService ks)
    {
      _vks = vks;
      _vs = vs;
      _ks = ks;
    }
    // METHODS
    // CREATE
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep vaultkeepdata)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        vaultkeepdata.CreatorId = userInfo.Id;
        // Convert.ToInt32(vaultkeepdata.KeepId);
        // Convert.ToInt32(vaultkeepdata.VaultId);
        Vault vault = _vs.Get(vaultkeepdata.VaultId, userInfo.Id);
        VaultKeep vaultkeep = _vks.Create(vaultkeepdata, vault);
        _ks.IncreaseKept(vaultkeep.KeepId);
        vaultkeep.Creator = userInfo;
        return Ok(vaultkeep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // DELETE
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<String>> Delete(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _vks.Delete(id, userInfo.Id);
        return Ok("Vault Deleted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}