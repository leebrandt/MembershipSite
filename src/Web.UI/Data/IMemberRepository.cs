using MembershipSite.Web.UI.Models;

namespace MembershipSite.Web.UI.Data
{
    public interface IMemberRepository
    {
        void Register(MemberEntity memberEntity);
    }
}