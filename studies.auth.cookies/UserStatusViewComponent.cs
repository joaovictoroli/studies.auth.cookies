using Microsoft.AspNetCore.Mvc;

namespace studies.auth.cookies
{
    public class UserStatusViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var userCookie = CookieManager.GetUserCookie(HttpContext.Request);
            return View(userCookie);
        }
    }
}
