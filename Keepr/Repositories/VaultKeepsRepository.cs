using System;
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
      this._db = db;
    }

    internal List<VaultKeepViewModel> GetKeepsByVaultId(int vaultId)
    {
       string sql = @"
      SELECT 
      k.*,
      vk.id as VaultKeepId,
      vk.vaultId as VaultId,
      a.*
      FROM 
      vaultkeeps vk
      JOIN keeps k ON k.id = vk.keepId
      JOIN accounts a ON a.id = k.creatorId
      WHERE vk.vaultId = @vaultId
      ;";
        return _db.Query<VaultKeepViewModel, Account, VaultKeepViewModel>(sql, (vk, a) =>
      {
        vk.Creator = a;
        return vk;
      }, new{ vaultId },splitOn:  "id" ).ToList();
    }

    internal VaultKeep GetByVaultKeepId(int vaultKeepId)
    {
      string sql = @"
      SELECT
      vk.*,
      a.*
      FROM vaultkeeps vk
      JOIN accounts a on vk.creatorId = a.id
      WHERE vk.id = @vaultKeepId ;";
       return _db.Query<VaultKeep, Profile, VaultKeep>(sql, (vk, a) =>
      {
        vk.Creator = a;
        return vk;
      }, new{ vaultKeepId }).FirstOrDefault();
    }

    // SELECT k.*, v

    public void DeleteVaultKeep(string userId, int id)
    {
      string sql = @"
      DELETE from vaultkeeps WHERE id = @id LIMIT 1;
      ";
       var rowsAffected = _db.Execute(sql, new { id });
      if (rowsAffected == 0)
      {
        throw new Exception("Delete failed");
      }
    }

    internal VaultKeep CreateVaultKeep(VaultKeep data)
    {
     string sql = @"
     INSERT INTO vaultkeeps(creatorId, vaultId, keepId)
     VALUES(@creatorId, @vaultId, @keepId);
     SELECT LAST_INSERT_ID();";
     var id = _db.ExecuteScalar<int>(sql, data);
     data.Id = id;
     return data;
    }
  // internal List<VaultKeepViewModel> GetKeepsByVaultId(int vaultId)
  //   {
  //     string sql = @"
  //     SELECT vk.*, k.*, a.*
  //     FROM vaultkeeps vk
  //     JOIN accounts a ON a.id = vk.creatorId
  //     JOIN keeps k ON k.id = vk.vaultId
  //     WHERE vk.vaultId = @vaultId
  //     ;";
  //     return _db.Query<VaultKeepViewModel>(sql, new { vaultId }).ToList();
  //   }

    //  internal List<VaultKeepViewModel> GetKeepsByVaultId(int vaultId)
    // {
    //   string sql = @"
    //   SELECT 
    //   k.*,
    //   v.*
    //   vk.id as vaultKeepId,
    //   vk.vaultId as vaultId,
    //   a.*
    //   FROM vaultkeeps vk
    //   JOIN accounts a ON a.id = vk.creatorId
    //   JOIN keeps k ON k.id = vk.keepId
    //   WHERE vk.vaultId = @vaultId
    //   ;";
    //   return _db.Query<VaultKeepViewModel, Account, VaultKeepViewModel>(sql, (vk, a) =>
    //   {
    //     vk.Creator = a;
    //     return vk;
    //   }, new{ vaultId }).ToList();
    // }


    //   internal List<VaultKeepViewModel> GetKeepsByVaultId(int vaultId)
    // {
    //    string sql = @"
    //   SELECT 
    //   k.*
    //   vk.id as vaultKeepId,
    //   vk.vaultId as vaultId,
    //   vk.keepId as keepId
    //   FROM 
    //   vaultkeeps vk
    //   JOIN vaults v ON v.id = vk.vaultId
    //   JOIN keeps k ON k.id = vk.keepId
    //   WHERE vk.vaultId = @vaultId
    //   ;";
    //   return _db.Query<VaultKeepViewModel>(sql, new { vaultId }).ToList();
    // }
  }
}