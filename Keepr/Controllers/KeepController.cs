using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [Route("api/[controller]")]
  public class KeepController : Controller
  {
    private readonly KeepService _ks;
    public KeepController(KeepService ks)
    {
      _ks = ks;
    }
    // METHODS
  }
}