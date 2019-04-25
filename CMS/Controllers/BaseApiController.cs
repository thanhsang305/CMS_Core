using CMS.Models;
using CMS_Library.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CMS.Controllers
{
    public class BaseApiController : ApiController
    {
        public Response res = new Response();
        protected Boolean checkAuth(string token)
        {
            try
            {
                using (CMSEntities _context = new CMSEntities())
                {
                    if (_context.Accounts.Any(x=>x.TokenLogin.Equals(token) && DateTime.Compare(DateTime.UtcNow, (DateTime)x.ExpireTokenLogin) < 0))
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
