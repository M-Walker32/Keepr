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
    public async Task<ActionResult<List<Keep>>> GetVaultKeeps(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Vault found = _vs.Get(id, userInfo?.Id);
        List<Keep> keeps = _vks.GetVaultKeeps(id);
        if (found.IsPrivate && userInfo.Id != found.CreatorId)
        {
          return BadRequest("Private Keeps");
        }
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // // GET VAULTKEEPS BY ID ('vaults/id/keeps')
    // [HttpGet("{id}/keeps/{id}")]
    // public async Task<ActionResult<Keep>> GetVaultKeepById(int vaultid, int keepid)
    // {
    //   try
    //   {
    //     Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
    //     // Vault found = _vs.Get(vaultid, userInfo?.Id);
    //     // List<Keep> keeps = _vks.GetVaultKeeps(id);
    //     Keep keep = _vks.Get()
    //     return Ok(keep);
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }
    // GET BY ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Vault>> Get(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Vault vault = _vs.Get(id, userInfo?.Id);
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
        Convert.ToBoolean(vaultdata.IsPrivate);
        // vaultdata.IsPrivate = false;
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