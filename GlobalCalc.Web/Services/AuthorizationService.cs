using Microsoft.Extensions.Primitives;
using System.Security.Cryptography;
using System.Text;

namespace GlobalCalc.Web.Services
{
    public class AuthorizationService
    {
        private readonly string _adminToken;

        public AuthorizationService(IConfiguration configuration)
        {
            _adminToken = configuration["Authentication:AdminToken"];
        }

        public bool Validate(HttpContext httpContext, out string? authorizationMethod)
        {
            authorizationMethod = null;
            string? accessToken = null;
            if (httpContext.Request.Headers.TryGetValue("Authorization", out StringValues headerValue))
            {
                accessToken = headerValue.ToString();
                authorizationMethod = "Header";
            }
            else if (httpContext.Request.Cookies.TryGetValue("access_token", out string? cookieValue))
            {
                accessToken = cookieValue;
                authorizationMethod = "Cookie";
            }

            return accessToken == _adminToken;
        }

        public bool Login(string login, string password, IResponseCookies cookies)
        {
            string userToken = GetToken(login, password);
            if (userToken != _adminToken)
                return false;

            SetCookies(cookies, userToken);
            return true;
        }

        private void SetCookies(IResponseCookies cookies, string token)
        {
            cookies.Append("access_token", token, new CookieOptions { Expires = DateTime.Now.AddDays(6) });
        }

        private static string GetToken(string login, string password)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] hashedData = sha256.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(login, password)));
            StringBuilder result = new StringBuilder();
            foreach (byte b in hashedData)
                result.Append(b.ToString("x2"));
            return result.ToString();
        }
    }
}
