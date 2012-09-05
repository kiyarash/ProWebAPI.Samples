﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;
using TimeoutHandlerSample.MessageHandlers;

namespace TimeoutHandlerSample {

    public class Global : System.Web.HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {

            var config = GlobalConfiguration.Configuration;
            config.Routes.MapHttpRoute(
                "DefaultHttpRoute",
                "api/{controller}"
            );
            config.MessageHandlers.Add(new LoggerHandler());
            //config.MessageHandlers.Add(new TimeoutHandler());
            config.MessageHandlers.Add(new DotNet4TimeoutHandler(300));
        }
    }
}