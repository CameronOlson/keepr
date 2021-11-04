using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _vaultsRepository;
    private readonly VaultKeepsRepository _vaultKeepsRepository;

    public VaultsService(VaultsRepository vaultsRepository, VaultKeepsRepository vaultKeepsRepository)
    {
      _vaultsRepository = vaultsRepository;
      _vaultKeepsRepository = vaultKeepsRepository;
    }

    internal Vault CreateVault(Vault newVault)
    {
      return _vaultsRepository.CreateVault(newVault);
    }

    internal Vault GetVaultById(int vaultId)
    {
      Vault foundVault = _vaultsRepository.GetVaultById(vaultId);
      if(foundVault == null)
      {
        throw new Exception("Unable to find this vault Vault");
      }
      return foundVault;
    }

     internal Vault GetAuthVaultById(int vaultId, string userId)
    {
      Vault foundVault = _vaultsRepository.GetVaultById(vaultId);
      if(foundVault == null)
      {
        throw new Exception("Unable to find this vault Vault");
      }
      else if(foundVault.CreatorId != userId && foundVault.IsPrivate == true)
      {
        throw new Exception("Unable to find this vault Vault");
      }
      return foundVault;
    }


   internal Vault UpdateVault(Vault updatedVault)
    {
      Vault vault = GetVaultById(updatedVault.Id);
      if(vault.CreatorId != updatedVault.CreatorId)
      {
        throw new Exception("this isn't yours");
      }
      vault.Name = updatedVault.Name ?? vault.Name;
      vault.Description = updatedVault.Description ?? vault.Description;
      vault.IsPrivate = updatedVault.IsPrivate;

      return _vaultsRepository.UpdateVault(vault);

    }

    internal void Delete(int vaultId, string userId)
    {
      Vault foundVault = GetVaultById(vaultId);
      if (foundVault.CreatorId != userId)
      {
        throw new Exception("You aren't allowed to do that");
      }
      _vaultsRepository.Delete(vaultId);
    }

    internal List<Vault> GetVaultsByProfileId(string profileId, string accountId)
    {
      if(profileId == accountId)
      {
      return _vaultsRepository.GetVaultsByProfileId(profileId);
      }
      return _vaultsRepository.GetPublicVaultsByProfileId(profileId);
    }

     public List<VaultKeepViewModel> GetKeepsByVaultId(int vaultId, string userId)
    {
     Vault vault = GetVaultById(vaultId);
      if(vault.CreatorId != userId)
      {
        if(vault.IsPrivate == true)
        {
        throw new Exception("This Vault is private!!");
        }
      } 
      return _vaultKeepsRepository.GetKeepsByVaultId(vaultId);
     
    }
  }
}