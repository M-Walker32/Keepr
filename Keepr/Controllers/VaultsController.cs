using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [Route("api/[controller]")]
  public class VaultsController : Controller
  {
    private readonly VaultService _vs;
    private readonly VaultKeepsService _vks;
    public VaultsController(VaultService vs, VaultKeepsService vks)
    {
      _vs = vs;
      _vks = vks;
    }
    // METHODS
    // GET VAULTKEEPS ('vaults/id/keeps')
    [HttpGet("{id}/keeps")]
    public ActionResult<List<Keep>> GetVaultKeeps(int id)
    {
      try
      {
        List<Keep> keeps = _vks.GetVaultKeeps(id);
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // GET BY ID
    [HttpGet("{id}")]
    public ActionResult<Vault> Get(int id)
    {
      try
      {
        Vault vault = _vs.Get(id);
        return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // CREATE
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault vaultdata)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        vaultdata.CreatorId = userInfo.Id;
        Vault vault = _vs.Create(vaultdata);
        vault.Creator = userInfo;
        return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // EDIT
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Edit(int id, [FromBody] Vault vaultdata)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        vaultdata.CreatorId = userInfo.Id;
        vaultdata.Id = id;
        Vault vault = _vs.Edit(vaultdata);
        return Ok(vault);
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
        _vs.Delete(id, userInfo.Id);
        return Ok("Vault Deleted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}