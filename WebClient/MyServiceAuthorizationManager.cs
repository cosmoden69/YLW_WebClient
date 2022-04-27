using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.Collections.Generic;
using System.Net;

namespace YLW_WebClient
{
    public class MyServiceAuthorizationManager : ServiceAuthorizationManager
    {
        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            HttpResponseMessageProperty prop = new HttpResponseMessageProperty();
            prop.Headers.Add("Access-Control-Allow-Origin", "http://20.194.52.25:8100");
            prop.Headers.Add("Access-Control-Allow-Credentials", "true");
            prop.Headers.Add("Access-Control-Allow-Method", "POST,GET,PUT,DELETE,OPTIONS");
            prop.Headers.Add("Content-Type", "text/plain");
            operationContext.OutgoingMessageProperties.Add(HttpResponseMessageProperty.Name, prop);

            return true;
        }
    }
}
