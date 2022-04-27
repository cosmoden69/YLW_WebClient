//MessageInspector Class
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.Collections.Generic;
using System.Net;

namespace YLW_WebClient
{
    public class MessageInspector : IDispatchMessageInspector
    {
        private ServiceEndpoint _serviceEndpoint;
        Dictionary<string, string> requiredHeaders;

        public MessageInspector(ServiceEndpoint serviceEndpoint)
        {
            _serviceEndpoint = serviceEndpoint;
            requiredHeaders = new Dictionary<string, string>();

            requiredHeaders.Add("Access-Control-Allow-Origin", "http://20.194.52.25:8100");
            requiredHeaders.Add("Access-Control-Allow-Credentials", "true");
            requiredHeaders.Add("Access-Control-Allow-Method", "POST,GET,PUT,DELETE,OPTIONS");
            requiredHeaders.Add("Access-Control-Allow-Headers", "X-Requested-With,Content-Type");
        }

        public object AfterReceiveRequest(ref Message request,
                                              IClientChannel channel,
                                              InstanceContext instanceContext)
        {
            var httpRequest = (HttpRequestMessageProperty)request
                       .Properties[HttpRequestMessageProperty.Name];
            return new
            {
                origin = httpRequest.Headers["Origin"],
                handlePreflight = httpRequest.Method.Equals("OPTIONS",
                StringComparison.InvariantCultureIgnoreCase)
            };
        }

        /// <summary>
        /// Called after the operation has returned but before the reply message
        /// is sent.
        /// </summary>
        /// <param name="reply">The reply message. This value is null if the 
        /// operation is one way.</param>
        /// <param name="correlationState">The correlation object returned from
        ///  the method.</param>
        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            var state = (dynamic)correlationState;
            if (state.handlePreflight)
            {
                reply = Message.CreateMessage(MessageVersion.None, "PreflightReturn");

                var httpResponse = new HttpResponseMessageProperty();
                reply.Properties.Add(HttpResponseMessageProperty.Name, httpResponse);

                httpResponse.SuppressEntityBody = true;
                httpResponse.StatusCode = HttpStatusCode.OK;
            }

            var httpHeader = reply.Properties["httpResponse"] as HttpResponseMessageProperty;
            foreach (var item in requiredHeaders)
            {
                httpHeader.Headers.Add(item.Key, item.Value);
            }
        }
    }

    class StateMessage
    {
        public Message Message;
    }
}
