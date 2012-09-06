using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using Machine.Specifications;
using Web.UI;

namespace MembershipSite.Specifications
{
    public class With_the_main_site_routes_registered
    {
        Establish that = () =>
            {
                RouteTable.Routes.Clear();
                MvcApplication.RegisterRoutes(RouteTable.Routes);
            };
    }
}
