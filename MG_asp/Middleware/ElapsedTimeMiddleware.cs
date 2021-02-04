using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MG_asp.Middleware
{
    public class ElapsedTimeMiddleware
    {
        private RequestDelegate _next;

        public ElapsedTimeMiddleware(RequestDelegate next)
        {
            _next = next;

        }
        public async Task Invoke(HttpContext context)
        {
            var sw = new Stopwatch();
            sw.Start();
            await _next(context);
            Console.WriteLine($"{context.Request.Path} executed in {sw.ElapsedMilliseconds}ms");

        }
    }
}