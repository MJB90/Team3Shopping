using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team3Shopping.Models;
using Microsoft.AspNetCore.Http;
namespace Team3Shopping
{
    public class Utility
    {
        private myDBContext dBContext;
        private const int SessionTimeout = 15;
        public Utility(myDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public Session GetSession(HttpRequest Request )
        {
            if (Request.Cookies["SessionId"] == null)
            {
                return null;
            }

            Guid sessionId = Guid.Parse(Request.Cookies["SessionId"]);
            long currTimeStamp = DateTimeOffset.Now.ToUnixTimeSeconds();
            Session session = dBContext.Sessions.FirstOrDefault(x =>
                x.Id == sessionId && currTimeStamp - x.Timestamp <= (SessionTimeout*60)
            );

            return session;
        }
    }
}
