using BlazorApp1.Models;
using Blazored.LocalStorage;
using BlazorApp1.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace BlazorApp1.Services
{
    public class AuthService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly JwtAuthenticationStateProvider authenticationStateProvider;

        public AuthService(HttpClient httpClient,
                           ILocalStorageService localStorage,
                           AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
            this.authenticationStateProvider = (JwtAuthenticationStateProvider)authenticationStateProvider;
        }

        public async Task<string> Login(LoginModel loginModel)
        {
            var result = await httpClient.PostAsJsonAsync("api/Account/Login", loginModel);

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadFromJsonAsync<LoginResponse>();
                if (response != null && !string.IsNullOrEmpty(response.Token))
                {
                    await localStorage.SetItemAsStringAsync("authToken", response.Token);
                    authenticationStateProvider.MarkUserAsAuthenticated(response.Token);
                    return "Success";
                }
            }

            var errorContent = await result.Content.ReadAsStringAsync();
            return string.IsNullOrEmpty(errorContent) ? "Невірний email або пароль." : errorContent;
        }

        public async Task Logout()
        {
            await localStorage.RemoveItemAsync("authToken");
            authenticationStateProvider.MarkUserAsLoggedOut();
        }
    }
}
