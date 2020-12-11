using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Caching
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveSeconds;

        public CachedAttribute(int timeToLiveSeconds)
        {
            _timeToLiveSeconds = timeToLiveSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheSettings = context.HttpContext.RequestServices.GetRequiredService<CacheSettings>();
            if (!cacheSettings.IsEnabled)
            {
                await next();
                return;
            }
            string key = (string)context.HttpContext.Items["userId"];
            var cachedService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
            string cachedResponse = await cachedService.GetCachedResponseAsync(key);

            if(!string.IsNullOrEmpty(cachedResponse))
            {
                var contentResult = new ContentResult
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = contentResult;
                return;
            }
            

            var executedContextResult = await next();

            if(executedContextResult.Result is OkObjectResult okObjectResult)
            {
                await cachedService.CacheResponseAsync(key, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLiveSeconds));
            }
         }
    }
}
