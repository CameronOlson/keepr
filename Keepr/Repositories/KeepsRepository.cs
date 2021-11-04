using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Keep> GetKeeps()
    {
       string sql = @"
      SELECT 
      k.* ,
      a.*
      FROM keeps k
      JOIN accounts a on k.creatorId = a.id;";
      
      return _db.Query<Keep, Profile, Keep>(sql, (k, a) =>
      {
        k.Creator = a;
        return k;
      }).ToList();
    }

    internal Keep GetKeepById(int id)
    {
      string sql = @"
      SELECT 
      k.* ,
      a.*
      FROM keeps k
      JOIN accounts a on k.creatorId = a.id
      WHERE k.id = @id;";
      return _db.Query<Keep, Profile, Keep>(sql, (k, a) =>
      {
        k.Creator = a;
        return k;
      }, new{ id }).FirstOrDefault();
    }

    internal Keep UpdateCount(int keepId)
    {
      string sql =@"
      UPDATE keeps
      SET keeps = keeps + 1
      WHERE id = @keepId
      ;";
      return _db.Query<Keep>(sql, new { keepId }).FirstOrDefault();
   }


    // UPDATe keeps
    // SET
    // keeps= keeps + 1
    // WHERE id = @id

    internal Keep CreateKeep(Keep data)
    {
      string sql = @"
      INSERT INTO keeps(name, description, views, shares, keeps, creatorId, img)
      VALUES(@Name, @Description, @Views, @Shares, @Keeps, @CreatorId, @Img);
      SELECT LAST_INSERT_ID();
      ";
      var id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data;
    }

    public Keep UpdateKeep(Keep updatedKeep)
    {
      string sql =@"
      UPDATE keeps 
      SET
      name = @Name,
      description = @Description,
      views = @Views,
      shares = @Shares,
      keeps = @Keeps,
      img = @Img
      WHERE id = @Id LIMIT 1;
      ";
      var rowsAffected = _db.Execute(sql, updatedKeep);
      if(rowsAffected == 0)
      {
        throw new Exception("Update Failed");
      }
      return updatedKeep;
    }

    internal List<Keep> GetKeepsByProfileId(string profileId)
    {
      string sql =@"
      SELECT * FROM keeps WHERE creatorId = @profileId
      ;";
      return _db.Query<Keep>(sql, new { profileId }).ToList();
    }

    internal void Delete(int keepId)
    {
      string sql = "DELETE FROM keeps WHERE id = @keepId LIMIT 1;";
      var rowsAffected = _db.Execute(sql, new { keepId });
      if(rowsAffected == 0)
      {
        throw new Exception("Delete Failed");
      }
    }
  }
}