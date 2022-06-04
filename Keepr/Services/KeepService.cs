using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepService
  {
    private readonly KeepRepository _repo;
    public KeepService(KeepRepository repo)
    {
      _repo = repo;
    }
    // METHODS
    // GET ALL
    internal List<Keep> Get()
    {
      return _repo.Get();
    }

    // GET BY ID

    internal Keep Get(int id)
    {
      Keep keep = _repo.Get(id);
      if (keep == null)
      {
        throw new Exception("Invalid Id");
      }
      return keep;
    }
    // GET PROFILE KEEPS
    internal List<Keep> GetKeeps(string id)
    {
      return _repo.GetKeeps(id);
    }
    // CREATE
    internal Keep Create(Keep keepdata)
    {
      return _repo.Create(keepdata);
    }
    // EDIT
    internal Keep Edit(Keep keepdata)
    {
      Keep originial = Get(keepdata.Id);
      if (originial.CreatorId != keepdata.CreatorId)
      {
        throw new Exception("This is not your to edit");
      }
      originial.Img = keepdata.Img ?? originial.Img;
      originial.Name = keepdata.Name ?? originial.Name;
      originial.Description = keepdata.Description ?? originial.Description;
      _repo.Edit(originial);
      return Get(originial.Id);
    }

    // DELETE
    internal void Delete(int id, string userId)
    {
      Keep keep = Get(id);
      if (keep.CreatorId != userId)
      {
        throw new Exception("You cannot delete this");
      }
      _repo.Delete(id);
    }
  }
}