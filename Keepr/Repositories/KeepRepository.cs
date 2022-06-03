using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class KeepRepository
  {
    private readonly IDbConnection _db;
    public KeepRepository(IDbConnection db)
    {
      _db = db;
    }
    // METHODS
    // GET BY ID
    internal Keep Get(int id)
    {
      string sql = @"
      SELECT
        a.*,
        k.*
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.Id
      WHERE k.id = @id
      ";
      return _db.Query<Account, Keep, Keep>(sql, (account, keep) =>
      {
        keep.Creator = account;
        return keep;
      }, new { id }).FirstOrDefault();
    }
    // GET
    internal List<Keep> Get()
    {
      string sql = @"
      SELECT
      k.*,
      a.*
      FROM keeps k
      JOIN accounts a ON k.creatorId = a.id;
      ";
      return _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
      {
        keep.Creator = account;
        return keep;
      }).ToList();
    }
    //  CREATE
    internal Keep Create(Keep keepdata)
    {
      string sql = @"
      INSERT INTO keeps
      (name, description, img, views, kept, creatorId)
      VALUES
      (@Name, @Description, @Img, @Views, @Kept, @CreatorId);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, keepdata);
      keepdata.Id = id;
      return keepdata;
    }
    // EDIT
    internal void Edit(Keep originial)
    {
      string sql = @"
      UPDATE keeps
      SET
       name = @Name,
       description = @Description,
       img = @Img,
       views = @Views,
       kept = @Kept
      WHERE id = @Id";
      _db.Execute(sql, originial);
    }
    // DELETE
    internal void Delete(int id)
    {
      string sql = @"
      DELETE FROM keeps WHERE id = @id LIMIT 1";
      _db.Execute(sql, new { id });
    }

  }
}