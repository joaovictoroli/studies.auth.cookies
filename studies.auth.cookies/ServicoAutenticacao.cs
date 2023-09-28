using studies.auth.cookies.ViewModels;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace studies.auth.cookies
{
    public static class ServicoAutenticacao
    {
        private static readonly HttpClient httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7011/api/")         
        };

        public static async Task<UserCookie> LoginAsync(LoginViewModel loginViewModel)
        {
            var json = JsonSerializer.Serialize(loginViewModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");


            var response = await httpClient.PostAsync("Account/login", data);
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                var userCookie = JsonSerializer.Deserialize<UserCookie>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return userCookie;
            }
            return null;
        }

        public static async Task<UserViewModel> GetUserAsync(UserCookie userCookie)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userCookie.Token);

                var response = await httpClient.GetAsync($"/api/Users/{userCookie.UserName}");

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("halo");
                    var content = await response.Content.ReadAsStringAsync();
                    var user = JsonSerializer.Deserialize<UserViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    return user;
                }
                else
                {                   
                    Console.WriteLine($"Error: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
