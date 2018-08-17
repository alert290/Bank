using Bank.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Bank
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            using (var context = new BankContext())
            {
                context.Database.CreateIfNotExists();
            }

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
