using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _keepsRepository;

    public KeepsService(KeepsRepository keepsRepository)
    {
      _keepsRepository = keepsRepository;
    }

    internal List<Keep> GetKeeps()
    {
      return _keepsRepository.GetKeeps();
    }

    internal Keep GetKeepById(int keepId)
    {
      Keep foundKeep = _keepsRepository.GetKeepById(keepId);
        if(foundKeep == null)
      {
        throw new Exception("Unable to find Keep");
      }
      _keepsRepository.UpdateCount(keepId);
      foundKeep.Keeps++;
      return foundKeep;
    }

    internal Keep CreateKeep(Keep data)
    {
      return _keepsRepository.CreateKeep(data);
    }
    internal Keep UpdateKeep(Keep updatedKeep)
    {
      Keep keep = GetKeepById(updatedKeep.Id);
   
      keep.Name = updatedKeep.Name ?? keep.Name;
      keep.Description = updatedKeep.Description ?? keep.Description;
      keep.Views = updatedKeep.Views;
      keep.Shares = updatedKeep.Shares;
      keep.Keeps = updatedKeep.Keeps;
      keep.Img = updatedKeep.Img;

      return _keepsRepository.UpdateKeep(keep);

    }

    internal List<Keep> GetKeepsByProfileId(string profileId)
    {
      return _keepsRepository.GetKeepsByProfileId(profileId);
    }

    internal void Delete(int keepId, string userId)
    {
      Keep foundKeep = GetKeepById(keepId);
      if(foundKeep.CreatorId != userId)
      {
        throw new Exception("You aren't allowed to do that");
      }
      _keepsRepository.Delete(keepId);
    }
  }
}