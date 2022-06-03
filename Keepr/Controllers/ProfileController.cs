using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
  [Route("api/[controller]")]
  public class ProfileController : Controller
  {
    private readonly ProfileService _ps;
    public ProfileController(ProfileService ps)
    {
      _ps = ps;
    }
    // METHODS
  }
}