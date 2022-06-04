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
  public class KeepsController : Controller
  {
    private readonly KeepService _ks;
    public KeepsController(KeepService ks)
    {
      _ks = ks;
    }
    // METHODS
    // GET ALL
    [HttpGet]
    public ActionResult<List<Keep>> Get()
    {
      try
      {
        List<Keep> keep = _ks.Get();
        return Ok(keep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // GET BY ID
    [HttpGet("{id}")]
    public ActionResult<Keep> Get(int id)
    {
      try
      {
        Keep keep = _ks.Get(id);
        return Ok(keep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // CREATE
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Keep>> Create([FromBody] Keep keepdata)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        keepdata.CreatorId = userInfo.Id;
        Keep keep = _ks.Create(keepdata);
        keep.Creator = userInfo;
        return Ok(keep);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // EDIT
    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Keep>> Edit(int id, [FromBody] Keep keepdata)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        keepdata.CreatorId = userInfo.Id;
        keepdata.Id = id;
        Keep keep = _ks.Edit(keepdata);
        return Ok(keep);
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
        _ks.Delete(id, userInfo.Id);
        return Ok("Keep Deleted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}