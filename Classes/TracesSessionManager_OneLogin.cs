using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

namespace TDSMAN.Classes
{
    public static class TracesSessionManager_OneLogin
    {
        public static CookieContainer SharedCookieContainer = new CookieContainer();
        public static DateTime LastLoginTime;
        public static bool IsLoggedIn = false;

        // TRACES session timeout is ~15 minutes. Use 12 minutes.
        private const int SESSION_TIMEOUT_MINUTES = 12;

        public static bool IsSessionActive()
        {
            if (!IsLoggedIn) return false;

            if (DateTime.Now.Subtract(LastLoginTime).TotalMinutes > SESSION_TIMEOUT_MINUTES)
                return false;

            return true;
        }

        public static void MarkLoginSuccess()
        {
            LastLoginTime = DateTime.Now;
            IsLoggedIn = true;
        }

        public static void ResetSession()
        {
            SharedCookieContainer = new CookieContainer();
            IsLoggedIn = false;
        }


        public static void ClearSession()
        {
            SharedCookieContainer = null;
            IsLoggedIn = false;
        }
    }
}
