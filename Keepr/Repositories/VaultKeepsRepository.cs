using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal VaultKeep Get(int id)
    {
      string sql = @"
      SELECT
        a.*,
        vk.*
      FROM vaultKeeps vk
      JOIN accounts a ON vk.creatorId = a.Id
      WHERE vk.id = @id
      ";
      return _db.Query<Account, VaultKeep, VaultKeep>(sql, (account, vaultkeep) =>
      {
        return vaultkeep;
      }, new { id }).FirstOrDefault();
    }
    // GET VAULT KEEPS 
    internal List<Keep> GetVaultKeeps(int id)
    {
      string sql = @"
      SELECT
        a.*,
        vk.*,
        k.*
        FROM vaultKeeps vk
        JOIN vaults v ON v.Id = vk.vaultId
        JOIN keeps k ON k.Id = vk.keepId
        JOIN accounts a ON k.creatorId = a.Id
        WHERE v.Id = @id;";
      return _db.Query<Account, VaultKeep, Keep, Keep>(sql, (a, vaultkeep, keep) =>
      {
        keep.Creator = a;
        // keep.vaultKeepId = vaultkeep.Id;
        return keep;
      }, new { id }).ToList();
    }
    // CREATE
    internal VaultKeep Create(VaultKeep vaultkeepdata)
    {
      string sql = @"
      INSERT INTO vaultKeeps
      (vaultId, creatorId, keepId)
      VALUES
      (@VaultId, @CreatorId, @KeepID);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, vaultkeepdata);
      vaultkeepdata.Id = id;
      return vaultkeepdata;
    }

    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM vaultKeeps WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }
  }
}