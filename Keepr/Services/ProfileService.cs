using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class ProfileService
  {
    private readonly ProfileRepository _repo;
    public ProfileService(ProfileRepository repo)
    {
      _repo = repo;
    }
    // METHODS
    internal object GetProfile(string id)
    {
      Profile profile = _repo.Get(id);
      if (profile == null)
      {
        throw new Exception("Invalid Id");
      }
      return profile;
    }
  }
}