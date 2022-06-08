using System;
using System.Collections.Generic;
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

    internal Vault Get(int id, string userId)
    {
      Vault vault = _repo.Get(id);
      if (vault == null)
      {
        throw new Exception("Invalid Id");
      }
      if (vault.IsPrivate && vault.CreatorId == userId)
      {
        return vault;
      }
      Vault publicVault = _repo.GetPublic(id);
      if (publicVault == null)
      {
        throw new Exception("Invalid Id");
      }
      return publicVault;
    }
    //  GET PROFILE VAULTS
    internal List<Vault> GetVaults(string id)
    {
      List<Vault> vaults = _repo.GetVaults(id);
      List<Vault> filteredVaults = vaults.FindAll(v => v.IsPrivate == false);
      return filteredVaults;
    }
    // Get Account Vaults
    internal List<Vault> GetAccountVaults(string id, string userId)
    {
      List<Vault> vaults = _repo.GetVaults(id);
      return vaults;
    }
    // CREATE
    internal Vault Create(Vault vaultdata)
    {
      return _repo.Create(vaultdata);
    }
    // EDIT
    internal Vault Edit(Vault vaultdata)
    {
      Vault original = Get(vaultdata.Id, vaultdata.CreatorId);
      if (original.CreatorId != vaultdata.CreatorId)
      {
        throw new Exception("This is not your to edit");
      }
      original.IsPrivate = vaultdata.IsPrivate;
      original.Name = vaultdata.Name ?? original.Name;
      original.Description = vaultdata.Description ?? original.Description;
      _repo.Edit(original);
      return Get(original.Id, vaultdata.CreatorId);
    }
    // DELETE
    internal void Delete(int id, string userId)
    {
      Vault vault = Get(id, userId);
      if (vault.CreatorId != userId)
      {
        throw new Exception("You cannot delete this");
      }
      _repo.Delete(id);
    }
  }
}