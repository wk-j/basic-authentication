using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BasicAuthentication.Attributes {
    [AttributeUsage(AttributeTargets.Method)]
    public class BasicAuthorizeAttribute : TypeFilterAttribute {
        public BasicAuthorizeAttribute(Type type) : base(type) {
        }
    }

    public class BasicAuthorizeFilter : IAuthorizationFilter {
        public void OnAuthorization(AuthorizationFilterContext context) {
            string au = context.HttpContext.Request.Headers["Authorization"];
            if (au != null && au.StartsWith("Basic ")) {
                // Get the encoded username and password
                var encodedUsernamePassword = au.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();
                // Decode from Base64 to string
                var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));
                // Split username and password
                var username = decodedUsernamePassword.Split(':', 2)[0];
                var password = decodedUsernamePassword.Split(':', 2)[1];
                // Check if login is correct
                if (IsAuthorized(username, password)) {
                    return;
                }
            }
            // Return authentication type (causes browser to show login dialog)
            context.HttpContext.Response.Headers["WWW-Authenticate"] = "Basic";
            context.Result = new UnauthorizedResult();
        }
        public bool IsAuthorized(string username, string password) {
            // Check that username and password are correct
            return username.Equals("admin", StringComparison.InvariantCultureIgnoreCase)
                   && password.Equals("admin");
        }
    }
}