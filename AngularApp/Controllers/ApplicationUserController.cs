using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AngularApp.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ApplicationUserController : ControllerBase
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public ApplicationUserController(UserManager<ApplicationUser> userManager,
                                     SignInManager<ApplicationUser> signInManager)
    {
      _userManager = userManager ;
      _signInManager = signInManager;
    }

    [HttpPost]
    [Route("Register")]
    // api/ApplicationUser/Register
    public async Task<object> PostApplicationUser(ApplicationUserModel model)
    {
      ApplicationUser applicationUser = new ApplicationUser()
      {
        UserName = model.UserName,
        Email = model.Email,
        FullName = model.FullName
      };
      try
      {
        IdentityResult result = await _userManager.CreateAsync(applicationUser, model.Password);
        return Ok(result);
      }
      catch(Exception ex)
      {
        throw ex;
      }
    }

  }
}
