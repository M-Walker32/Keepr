using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    public VaultKeepsService(VaultKeepsRepository repo)
    {
      _repo = repo;
    }
    // METHODS
    // GET By ID
    internal VaultKeep Get(int id)
    {
      VaultKeep vaultkeep = _repo.Get(id);
      if (vaultkeep == null)
      {
        throw new Exception("Invalid Id");
      }
      return vaultkeep;
    }
    // GET VAULT KEEPS
    internal List<Keep> GetVaultKeeps(int id)
    {
      List<Keep> keeps = _repo.GetVaultKeeps(id);
      return keeps;
    }
    // CREATE
    internal VaultKeep Create(VaultKeep vaultkeepdata)
    {
      return _repo.Create(vaultkeepdata);
    }
    // DELETE
    internal void Delete(int id, string userId)
    {
      VaultKeep vaultkeep = Get(id);
      if (vaultkeep.CreatorId != userId)
      {
        throw new Exception("You cannot delete this");
      }
      _repo.Delete(id);
    }
  }
}