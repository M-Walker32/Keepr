using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultService
  {
    private readonly VaultRepository _repo;
    public VaultService(VaultRepository repo)
    {
      _repo = repo;
    }
    // METHODS
  }
}