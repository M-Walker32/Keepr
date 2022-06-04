using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultRepository
  {
    private readonly IDbConnection _db;
    public VaultRepository(IDbConnection db)
    {
      _db = db;
    }
    // METHODS
    // GET BY ID
    internal Vault Get(int id)
    {
      string sql = @"
      SELECT
        a.*,
        v.*
      FROM vaults v
      JOIN accounts a ON v.creatorId = a.Id
      WHERE v.id = @id
      ";
      return _db.Query<Account, Vault, Vault>(sql, (account, vault) =>
      {
        vault.Creator = account;
        return vault;
      }, new { id }).FirstOrDefault();
    }
    // GET VAULTS BY PROFILE

    internal object GetVaults(string id)
    {
      string sql = @"
      SELECT
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a ON v.creatorId = a.id
      WHERE creatorId = @id
      ";
      return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
      {
        vault.Creator = profile;
        return vault;
      }, new { id }).ToList();
    }
    //  CREATE
    internal Vault Create(Vault vaultdata)
    {
      string sql = @"
      INSERT INTO vaults
      (name, description, isPrivate, creatorId)
      VALUES
      (@Name, @Description, @IsPrivate, @CreatorId);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, vaultdata);
      vaultdata.Id = id;
      return vaultdata;
    }
    // EDIT
    internal void Edit(Vault original)
    {
      string sql = @"
      UPDATE vaults
      SET
       name = @Name,
       description = @Description,
       isPrivate = @IsPrivate
      WHERE id = @Id";
      _db.Execute(sql, original);
    }
    // DELETE
    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM vaults WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }

  }
}