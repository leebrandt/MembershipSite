using System.Web.Mvc;
using MembershipSite.Web.UI.Data;
using MembershipSite.Web.UI.Models;
using MembershipSite.Web.UI.Services;
using MembershipSite.Web.UI.ViewModels;

namespace MembershipSite.Web.UI.Controllers
{
    public class AccountController : Controller
    {
        readonly IMemberRepository _memberRepository;
        readonly IAuthenticationService _authenticationService;

        public AccountController(IMemberRepository memberRepository, 
            IAuthenticationService authenticationService)
        {
            _memberRepository = memberRepository;
            _authenticationService = authenticationService;
        }

        public ActionResult Registration()
        {
            return View(new RegistrationViewModel());
        }

        [HttpPost]
        public ActionResult Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                _memberRepository.Register(new MemberEntity
                                               {
                                                   FirstName = model.FirstName,
                                                   LastName = model.LastName,
                                                   Username = model.Username,
                                                   Password = model.Password
                                               });
                _authenticationService.Login(model.Username);
                return RedirectToAction("Info");
            }
            ViewData["error"] = "There was a problem with you registration.";
            return View(model);
        }
    }
}