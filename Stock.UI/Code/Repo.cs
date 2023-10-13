namespace Stock.UI.Code
{
    public class Repo
    {
        public static class Session
        {
            public static string? UserId
            {
                get
                {
                    string userId = (new HttpContextAccessor()).HttpContext.Session.GetString("UserId");
                    return userId;
                }
                set
                {
                    (new HttpContextAccessor()).HttpContext.Session.SetString("UserId", value ?? "");
                }

            }
            public static string? UserFullName
            {
                get
                {
                    string userFullName = (new HttpContextAccessor()).HttpContext.Session.GetString("UserFullName");
                    return userFullName;
                }
                set
                {
                    (new HttpContextAccessor()).HttpContext.Session.SetString("UserFullName", value ?? "");
                }

            }
            public static string? UserEmail
            {
                get
                {
                    string userEmail = (new HttpContextAccessor()).HttpContext.Session.GetString("UserEmail");
                    return userEmail;
                }
                set
                {
                    (new HttpContextAccessor()).HttpContext.Session.SetString("UserEmail", value ?? "");
                }
            }
            public static string? Token
            {
                get
                {
                    string token = (new HttpContextAccessor()).HttpContext.Session.GetString("Token");
                    return token;
                }
                set
                {
                    (new HttpContextAccessor()).HttpContext.Session.SetString("Token", value ?? "");
                }
            }
            public static string? Role
            {
                get
                {
                    string role = (new HttpContextAccessor()).HttpContext.Session.GetString("Role");
                    return role;
                }
                set
                {
                    (new HttpContextAccessor()).HttpContext.Session.SetString("Role", value ?? "");
                }
            }
        }
    }
}
