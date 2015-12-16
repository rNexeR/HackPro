using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HackPro
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //RegisterCustomRoutes(RouteTable.Routes);
        }


        //void RegisterCustomRoutes(RouteCollection routes)
        //{
        //    routes.MapRoute( // this route must be declared first, before the one below it
        //        "CrearEvento",
        //        "Admin/CrearEvento",
        //        new
        //        {
        //            controller = "Admin",
        //            action = "CrearEvento",
        //        });

        //        routes.MapPageRoute(
        //         "ProductsByCategoryRoute",
        //         "Category/{categoryName}",
        //         "~/ProductList.aspx"
        //       );

        //    routes.MapRoute(
        //         "CreateEvento",
        //         "Admin/CrearEvento/latitud={lati}&longitud={longi}",
        //         new
        //         {
        //             controller = "Admin",
        //             action = "CreateEvento",
        //             lati = UrlParameter.Optional,
        //             longi = UrlParameter.Optional
        //         });
        //}

    }
}
