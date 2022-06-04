using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultService
  {
    private readonly VaultRepository _repo;
    public VaultService(VaultRepository repo)
    {
      _repo = repo;
    }
    // METHODS
    // GET BY ID

    internal Vault Get(int id)
    {
      Vault vault = _repo.Get(id);
      if (vault == null)
      {
        throw new Exception("Invalid Id");
      }
      return vault;
    }
    // CREATE
    internal Vault Create(Vault vaultdata)
    {
      return _repo.Create(vaultdata);
    }
    // EDIT
    internal Vault Edit(Vault vaultdata)
    {
      Vault original = Get(vaultdata.Id);
      if (original.CreatorId != vaultdata.CreatorId)
      {
        throw new Exception("This is not your to edit");
      }
      original.IsPrivate = vaultdata.IsPrivate;
      original.Name = vaultdata.Name ?? original.Name;
      original.Description = vaultdata.Description ?? original.Description;
      _repo.Edit(original);
      return Get(original.Id);
    }
    // DELETE
    internal void Delete(int id, string userId)
    {
      Vault vault = Get(id);
      if (vault.CreatorId != userId)
      {
        throw new Exception("You cannot delete this");
      }
      _repo.Delete(id);
    }
  }
}