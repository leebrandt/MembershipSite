using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using Machine.Specifications;
using MembershipSite.Web.UI.Controllers;
using MvcContrib.TestHelper;
using Web.UI;

namespace MembershipSite.Specifications.HomePage
{
    public class When_a_user_wishes_to_use_the_application : With_the_main_site_routes_registered
    {
        It should_navigate_to_the_home_page = () =>
            "~/".ShouldMapTo<HomeController>(ctrl => ctrl.Index());
    }
}
