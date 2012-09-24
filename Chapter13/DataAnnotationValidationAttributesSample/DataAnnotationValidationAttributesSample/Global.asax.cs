using DataAnnotationValidationAttributesSample.Validation;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Validation;
using System.Web.Http.Validation.Providers;
using System.Web.Security;
using System.Web.SessionState;

namespace DataAnnotationValidationAttributesSample {

    public class Global : HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {

            var config = GlobalConfiguration.Configuration;
            config.Routes.MapHttpRoute(
                "DefaultHttpRoute",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );

            config.Services.RemoveAll(
                typeof(ModelValidatorProvider),
                validator => (validator is InvalidModelValidatorProvider));

            // Suppressing the IRequiredMemberSelector for all formatters
            foreach (var formatter in config.Formatters) {
                formatter.RequiredMemberSelector = new SuppressedRequiredMemberSelector();
            }
        }
    }
}