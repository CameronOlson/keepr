using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _profilesRepository;

    public ProfilesService(ProfilesRepository profilesRepository)
    {
      _profilesRepository = profilesRepository;
    }

    internal Profile GetById(string profileId)
    {
      return _profilesRepository.GetById(profileId);
    }

  }
}