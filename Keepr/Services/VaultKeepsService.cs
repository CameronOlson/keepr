using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vaultKeepsRepository;
    private readonly VaultsService _vaultsService;

    public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository, VaultsService vaultsService)
    {
      _vaultKeepsRepository = vaultKeepsRepository;
      _vaultsService = vaultsService;
    }

    internal VaultKeep GetById(int vaultKeepId)
    {
      return _vaultKeepsRepository.GetByVaultKeepId(vaultKeepId);
    }

    internal VaultKeep CreateVaultKeep(VaultKeep newVaultKeep, string userId)
    {
      Vault vault = _vaultsService.GetVaultById(newVaultKeep.VaultId);
      if(vault.CreatorId == userId)
      {
        newVaultKeep.CreatorId = userId;
      return _vaultKeepsRepository.CreateVaultKeep(newVaultKeep);
      }
      throw new Exception("You aren't allowed to do that");
    }

    public void DeleteVaultKeep(string id, int vaultKeepId)
    {
      VaultKeep vaultkeep = GetById(vaultKeepId);
    
      if(vaultkeep.CreatorId != id)
      {
      throw new Exception("This isn't yours");
      }
      _vaultKeepsRepository.DeleteVaultKeep(id, vaultKeepId);

    }
  }
}