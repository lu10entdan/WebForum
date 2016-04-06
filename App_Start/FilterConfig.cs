using System.Web;
using System.Web.Mvc;

namespace WebForum
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // This is a global filter that requires authentication for every page
            // To allow access to unathenticated users, apply [AllowAnonymous]
            filters.Add(new System.Web.Mvc.AuthorizeAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
