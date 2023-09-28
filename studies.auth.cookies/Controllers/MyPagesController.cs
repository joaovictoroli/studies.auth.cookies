using Microsoft.AspNetCore.Mvc;
using studies.auth.cookies.ViewModels;

namespace studies.auth.cookies.Controllers
{
    public class MyPagesController : Controller
    {
        public async Task<IActionResult> Index()
        {            
            var jwtToken = CookieManager.GetUserToken(Request);
            var isUserLoggedIn = !string.IsNullOrEmpty(jwtToken);

            if (isUserLoggedIn)
            {
                ViewBag.IsUserLoggedIn = isUserLoggedIn;
                var userViewModel = await ServicoAutenticacao.GetUserAsync(CookieManager.GetUserCookie(Request));

                if (userViewModel != null)
                {
                    return View(userViewModel);
                }
            }


            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserCookie userCookie = await ServicoAutenticacao.LoginAsync(model);

                if (userCookie is not null)
                {
                    CookieManager.SetUserCookie(Response, userCookie, 1);
                    Console.WriteLine(userCookie.Token);
                    return RedirectToAction("Index", "MyPages");
                }
                else
                {                   
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View();
                }
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            CookieManager.RemoveUserCookie(Response);
            return RedirectToAction("Index", "MyPages");
        }

    }



}
