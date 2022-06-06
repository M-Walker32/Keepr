using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [Route("api/[controller]")]
  public class ProfilesController : Controller
  {
    private readonly ProfileService _ps;
    private readonly VaultService _vs;
    private readonly KeepService _ks;
    private readonly AccountService _acts;
    public ProfilesController(ProfileService ps, VaultService vs, KeepService ks, AccountService acts)
    {
      _ps = ps;
      _vs = vs;
      _ks = ks;
      _acts = acts;
    }
    // METHODS
    // Get Profile
    [HttpGet("{id}")]
    public ActionResult<Profile> GetProfile(string id)
    {
      try
      {
        Profile profile = _acts.GetProfileById(id);
        return Ok(profile);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // Get Keeps
    [HttpGet("{id}/keeps")]
    public ActionResult<Keep> GetKeeps(string id)
    {
      try
      {
        return Ok(_ks.GetKeeps(id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // Get Vaults
    [HttpGet("{id}/vaults")]
    public async Task<ActionResult<Vault>> GetVaults(string id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        List<Vault> vaults = _vs.GetVaults(id);
        return Ok(vaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}