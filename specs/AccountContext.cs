using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FakeItEasy;
using Machine.Specifications;
using MembershipSite.Web.UI.Controllers;
using MembershipSite.Web.UI.Data;
using MembershipSite.Web.UI.Services;

namespace MembershipSite.Specifications
{
    public class With_an_account_controller
    {
        protected static IMemberRepository _memberRepository;
        protected static IAuthenticationService _authenticationService;
        protected static AccountController _controller;

        Establish that = () =>
            {
                _authenticationService = A.Fake<IAuthenticationService>();
                _memberRepository = A.Fake<IMemberRepository>();
                _controller = new AccountController(_memberRepository, _authenticationService);
            };
    }
}
