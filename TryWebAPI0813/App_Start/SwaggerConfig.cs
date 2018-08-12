using System.Web.Http;
using WebActivatorEx;
using TryWebAPI0813;
using Swashbuckle.Application;
using System;
using System.Xml.XPath;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace TryWebAPI0813
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            GlobalConfiguration.Configuration
                .EnableSwagger(c => { c.SingleApiVersion("v1", "TryWebAPI0813"); })
                .EnableSwaggerUi(c => { });
        }

    }
}
