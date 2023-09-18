﻿using System;
using System.Text;
using Assignment_Bosch.Services;
using Microsoft.EntityFrameworkCore;

namespace Assignment_Bosch.Middlewares
{
	public class AuditLogMiddleware
	{
		private readonly RequestDelegate _next;

		public AuditLogMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
		{
            var originalBodyStream = context.Response.Body;

            //Get CorrelationId from the request headers 
            var request = await GetRequestAsTextAsync(context.Request);
            string correlationId = context.Request.Headers["CorrelationId"].ToString();

            var auditService = serviceProvider.GetRequiredService<AuditService>();
            auditService.SaveAudit(correlationId, request, "Request");

            await using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;   

            await _next(context);

            var response = await GetResponseAsTextAsync(context.Response);
            auditService.SaveAudit(correlationId, request, "Response");

            await responseBody.CopyToAsync(originalBodyStream);
        }

        private async Task<string> GetRequestAsTextAsync(HttpRequest request)
        {
            var body = request.Body;

            request.EnableBuffering();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body = body;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        private async Task<string> GetResponseAsTextAsync(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return text;
        }
    }
}

