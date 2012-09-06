using System.Web.Mvc;
using System.Web.Routing;
using FakeItEasy;
using Machine.Specifications;
using Machine.Specifications.Mvc;
using MembershipSite.Web.UI.Controllers;
using MembershipSite.Web.UI.Data;
using MembershipSite.Web.UI.Models;
using MembershipSite.Web.UI.Services;
using MembershipSite.Web.UI.ViewModels;
using MvcContrib.TestHelper;
using Web.UI;


namespace MembershipSite.Specifications.Registration
{
    public class When_a_user_wishes_to_register : With_the_main_site_routes_registered
    {
        It should_navigate_to_the_registration_page = () =>
            "~/account/registration".ShouldMapTo<AccountController>(ctrl => ctrl.Registration());
    }

    public class When_navigating_to_the_registration_page : With_an_account_controller
    {
        Because of = () => _result = _controller.Registration();

        It should_load_the_registration_page = () =>
            _result.ShouldBeAView().And().ShouldUseDefaultView();

        It should_load_an_empty_registration_form = () =>
            _result.ShouldBeAView().And().ShouldHaveModelOfType<RegistrationViewModel>();

        static ActionResult _result;
    }

    public class When_registering_with_valid_and_complete_registration_information : With_an_account_controller
    {
        Establish that = () =>
            {
                _model = new RegistrationViewModel{FirstName = "Jim", LastName = "Beam"};
                _controller.ModelState.Clear(); // VALID MODEL
            };

        Because of = () => _result = _controller.Registration(_model);

        It should_create_a_new_member = () =>
            A.CallTo(() => _memberRepository.Register(A<MemberEntity>.That.Matches(x=>
                x.FirstName =="Jim" && x.LastName == "Beam"))).MustHaveHappened();

        It should_log_the_new_user_into_the_system = () =>
            A.CallTo(() => _authenticationService.Login(_model.Username)).MustHaveHappened();

        It should_navigate_them_to_member_info_page = () =>
            _result.ShouldBeARedirectToRoute().And().ActionName().ShouldBe("Info");

        static ActionResult _result;
        static RegistrationViewModel _model;
    }

    public class When_registering_with_invalid_or_incomplete_registration_information : With_an_account_controller
    {
        Establish that = () =>
        {
            _model = new RegistrationViewModel { FirstName = "Jim", LastName = "Beam" };
            _controller.ModelState.AddModelError("", "Invalid"); // INVALID MODEL
        };

        Because of = () => _result = _controller.Registration(_model);

        It should_not_create_a_new_member =()=>
            A.CallTo(() => _memberRepository.Register(A<MemberEntity>._)).MustNotHaveHappened();


        It should_return_the_user_to_the_registration_page = () =>
            _result.ShouldBeAView().And().ShouldUseDefaultView();

        It should_inform_the_user_of_the_error = () =>
            _controller.ViewData["error"].ShouldNotBeNull();

        static RegistrationViewModel _model;
        static ActionResult _result;
    }
}
