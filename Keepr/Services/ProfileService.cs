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
  }
}