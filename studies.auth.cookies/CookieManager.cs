using studies.auth.cookies;
using System.Text.Json;

namespace studies.auth.cookies;
public static class CookieManager
{
    public static void SetUserCookie(HttpResponse response, UserCookie userCookie, int expiresHours)
    {
        var option = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = DateTime.UtcNow.AddHours(expiresHours)
        };

        var json = JsonSerializer.Serialize(userCookie);
        response.Cookies.Append("UserCookie", json, option);
    }

    public static UserCookie GetUserCookie(HttpRequest request)
    {
        var json = request.Cookies["UserCookie"];
        if (string.IsNullOrEmpty(json)) return null;

        return JsonSerializer.Deserialize<UserCookie>(json);
    }

    public static void RemoveUserCookie(HttpResponse response)
    {
        response.Cookies.Delete("UserCookie");
    }

    public static string GetUserToken(HttpRequest request)
    {
        var userCookie = GetUserCookie(request);
        return userCookie?.Token;
    }

}
