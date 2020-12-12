using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ModelLayer;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Caching
{
    public class CacheUpdateAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string id = context.HttpContext.Request.Path.Value;
            var _cachingService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

            if (string.IsNullOrEmpty(id))
            {
                var reader = new StreamReader(context.HttpContext.Request.Body);
                var note = JsonConvert.DeserializeObject<Note>(await reader.ReadToEndAsync());
                var cachedResponse = await _cachingService.GetCachedResponseAsync(note.AccountId.ToString());
                if (string.IsNullOrEmpty(cachedResponse))
                {
                    await next();
                }
                var updatedResponse = cachedResponse + note;
                await _cachingService.CacheResponseAsync(note.AccountId.ToString(), updatedResponse, TimeSpan.FromSeconds(600));
            }
        }

        private void AddOntoCache(string readAsync)
        {
            throw new NotImplementedException();
        }
    }
}
