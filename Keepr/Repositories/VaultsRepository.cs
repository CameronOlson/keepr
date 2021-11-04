using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    public Vault CreateVault(Vault newVault)
    {
      string sql = @"
      INSERT INTO vaults(creatorId, name, description, isPrivate)
      VALUES(@CreatorId, @Name, @Description, @isPrivate);
      SELECT LAST_INSERT_ID();";
      var id = _db.ExecuteScalar<int>(sql, newVault);
      newVault.Id = id;
      return newVault;
    }

    internal Vault GetVaultById(int vaultId)
    
    {
      string sql = @"
      SELECT
      v.*,
      a.*
      FROM vaults v
      JOIN accounts a on v.creatorId = a.id
      WHERE v.id = @vaultId;";
      return _db.Query<Vault, Profile, Vault>(sql, (v, a) =>
      {
        v.Creator = a;
        return v;
      }, new{ vaultId }).FirstOrDefault();
    }

    public Vault UpdateVault(Vault updatedVault)
    {
      string sql =@"
      UPDATE vaults
      SET
      name = @Name,
      description = @Description,
      isPrivate = @IsPrivate
      WHERE id = @Id LIMIT 1;
      ";
      var rowsAffected = _db.Execute(sql, updatedVault);
      if(rowsAffected == 0)
      {
        throw new Exception("Update Failed");
      }
      return updatedVault;
    }

    internal void Delete(int vaultId)
    {
      string sql = "DELETE FROM vaults WHERE id = @vaultId LIMIT 1;";
      var rowsAffected = _db.Execute(sql, new { vaultId });
      if (rowsAffected == 0)
      {
        throw new Exception("Delete Failed");
      }
    }

    internal List<Vault> GetVaultsByProfileId(string profileId)
    {
      string sql = @"
      SELECT * FROM vaults WHERE creatorId = @profileId
      ;";
      return _db.Query<Vault>(sql, new { profileId }).ToList();
    }

    internal List<Vault> GetPublicVaultsByProfileId(string profileId)
    {
      string sql = @"
      SELECT * FROM vaults WHERE creatorId = @profileId AND isPrivate = 0
      ;";
      return _db.Query<Vault>(sql, new { profileId }).ToList();
    }
  }
}