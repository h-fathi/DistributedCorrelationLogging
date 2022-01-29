using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logging
{
    public class LogHeaderMiddleware
    {
        private readonly RequestDelegate _next;
		private readonly ILogger<LogHeaderMiddleware> _logger;
		public LogHeaderMiddleware(RequestDelegate next, ILogger<LogHeaderMiddleware> logger)
        {
            this._next = next;
			_logger = logger;
		}

        public async Task InvokeAsync(HttpContext context)
        {
            var correlationId = "";
            var header = context.Request.Headers["CorrelationId"];
            if (header.Count > 0)
            {
                correlationId = header[0];
            }
            else
            {
                correlationId = Guid.NewGuid().ToString();
                context.Items["CorrelationId"] = correlationId;
            }


			// Set all the common properties available for every request
			//LogContext.PushProperty("Host", context.Request.Host, destructureObjects: true);
			//LogContext.PushProperty("Protocol", context.Request.Protocol, destructureObjects: true);
			//LogContext.PushProperty("Scheme", context.Request.Scheme, destructureObjects: true);

			//// Only set it if available. You're not sending sensitive data in a querystring right?!
			//if (context.Request.QueryString.HasValue)
			//{
			//	LogContext.PushProperty("QueryString", context.Request.QueryString.Value, destructureObjects: true);
			//}

			//// Read and log request body data
			//string requestBodyPayload = await ReadRequestBody(context.Request);
   //         if (!string.IsNullOrEmpty(requestBodyPayload))
   //         {
			//	LogContext.PushProperty("RequestBody", requestBodyPayload, destructureObjects: true);
			//}
			

			//// Retrieve the IEndpointFeature selected for the request
			//var endpoint = context.GetEndpoint();
			//if (endpoint is object) // endpoint != null
			//{
			//	LogContext.PushProperty("EndpointName", endpoint.DisplayName, destructureObjects: true);
			//}

			// Read and log response body data
			// Copy a pointer to the original response body stream
			//var originalResponseBodyStream = context.Response.Body;

			// Create a new memory stream...

				// Continue down the Middleware pipeline, eventually returning to this class
				using (_logger.BeginScope("{@CorrelationId}", correlationId))
				{
					await this._next(context);
				}

				// Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
				//await responseBody.CopyToAsync(originalResponseBodyStream);

			//string responseBodyPayload = await ReadResponseBody(context.Response);
			//if (!string.IsNullOrEmpty(responseBodyPayload))
			//{
			//	LogContext.PushProperty("ResponseBody", responseBodyPayload, destructureObjects: true);
			//}

			
        }


		private async Task<string> ReadRequestBody(HttpRequest request)
		{
			HttpRequestRewindExtensions.EnableBuffering(request);
			string requestBody = "";
			if (request.Body.CanRead)
			{
				var body = request.Body;
				var buffer = new byte[Convert.ToInt32(request.ContentLength)];
				await request.Body.ReadAsync(buffer, 0, buffer.Length);
			    requestBody = Encoding.UTF8.GetString(buffer);
				body.Seek(0, SeekOrigin.Begin);
				request.Body = body;
			}
			return $"{requestBody}";
		}

		private static async Task<string> ReadResponseBody(HttpResponse response)
		{
			string responseBody = "";
			if (response.Body.CanRead)
			{
				response.Body.Seek(0, SeekOrigin.Begin);
			    responseBody = await new StreamReader(response.Body).ReadToEndAsync();
				response.Body.Seek(0, SeekOrigin.Begin);
			}
			return $"{responseBody}";
		}
	}
}
