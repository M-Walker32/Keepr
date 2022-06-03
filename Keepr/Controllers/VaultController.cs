using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [Route("api/[controller]")]
  public class VaultController : Controller
  {
    private readonly VaultService _vs;
    public VaultController(VaultService vs)
    {
      _vs = vs;
    }
    // METHODS
  }
}