using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepService
  {
    private readonly KeepRepository _repo;
    public KeepService(KeepRepository repo)
    {
      _repo = repo;
    }
    // METHODS
  }
}