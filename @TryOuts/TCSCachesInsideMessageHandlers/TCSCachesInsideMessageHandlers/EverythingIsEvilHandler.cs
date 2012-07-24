﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace TCSCachesInsideMessageHandlers {

    public class EverythingIsEvilHandler : DelegatingHandler {

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {

            var badRequestResponse = CachedResponseMessageItemStore.BadRequestResponseMessage;
            return badRequestResponse;
        }

        private static class CachedResponseMessageItemStore {

            internal static Task<HttpResponseMessage> BadRequestResponseMessage = GetResponseMessage(HttpStatusCode.BadRequest);

            private static Task<HttpResponseMessage> GetResponseMessage(HttpStatusCode status) {

                var tcs = new TaskCompletionSource<HttpResponseMessage>();
                tcs.SetResult(new HttpResponseMessage(status));
                return tcs.Task;
            }
        }
    }
}