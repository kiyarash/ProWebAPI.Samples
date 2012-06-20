﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace AbortRequestPipelineMessageHandler.MessageHandlers
{
public class EmptyPostBodyMessageHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Check for POST request and empty body.
        var content = request.Content.ReadAsStringAsync().Result;
        if (HttpMethod.Post == request.Method && 0 == content.Length)
        {
            TaskCompletionSource<HttpResponseMessage> tcs = new TaskCompletionSource<HttpResponseMessage>();
            tcs.SetResult(new HttpResponseMessage(HttpStatusCode.BadRequest) 
                { ReasonPhrase = "Empty body not allowed for POST."});
            return tcs.Task;
        }
        return base.SendAsync(request, cancellationToken);
    }
}
}