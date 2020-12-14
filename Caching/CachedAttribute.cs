using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ModelLayer;
using ModelLayer.DTOs.NoteDTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caching
{
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveSeconds;
        private string key;
        public CachedAttribute(int timeToLiveSeconds)
        {
            _timeToLiveSeconds = timeToLiveSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheSettings = context.HttpContext.RequestServices.GetRequiredService<CacheSettings>();
            key = (string)context.HttpContext.Items["userId"];
            if (!cacheSettings.IsEnabled)
            {
                await next();
                return;
            }
            var cachedService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
            string cachedResponse = await cachedService.GetCachedResponseAsync(key);

            if (!string.IsNullOrEmpty(cachedResponse) && context.HttpContext.Request.Method == "GET")
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

            if (executedContextResult.Result is OkObjectResult okObjectResult)
            {
                string responseData = JsonConvert.SerializeObject(okObjectResult.Value);
                string[] pathArray = context.HttpContext.Request.Path.Value.Split("/");
                int? pathParam = null;
                string cachedData = await cachedService.GetCachedResponseAsync(key);
                
                List<NoteResponseDto> currentUserData = null;
                if (cachedData != null)
                {
                    currentUserData = JsonConvert.DeserializeObject<List<NoteResponseDto>>(cachedData);
                }
                if (pathArray[pathArray.Length - 1].All(char.IsDigit))
                {
                    pathParam = Convert.ToInt32(pathArray[pathArray.Length - 1]);
                }

                switch (context.HttpContext.Request.Method)
                {
                    case "GET":
                        var responseDataList = JsonConvert.DeserializeObject<Response<List<NoteResponseDto>>>(responseData).Data;
                        await cachedService.CacheResponseAsync(key, responseDataList, TimeSpan.FromSeconds(_timeToLiveSeconds));
                        break;
                    case "POST":
                        if (cachedData == null)
                        {
                            break;
                        }
                        int? noteId = pathParam;
                        if (noteId == null)
                        {
                            var deserializedResponse = JsonConvert.DeserializeObject<Response<NoteResponseDto>>(responseData).Data;
                            currentUserData.Add(deserializedResponse);
                        }
                        else
                        {
                            var deserializedResponse = JsonConvert.DeserializeObject<Response<NoteResponseDto>>(responseData).Data;
                            currentUserData.Where(notes => notes.NoteId == noteId).Select(note => note = deserializedResponse);
                        }
                        await cachedService.CacheResponseAsync(key, currentUserData, TimeSpan.FromSeconds(600));
                        break;
                    case "DELETE":
                        noteId = pathParam;
                        if (cachedData == null)
                        {
                            break;
                        }
                        if (currentUserData.Remove(currentUserData.FirstOrDefault(note => note.NoteId == noteId)))
                        {
                            await cachedService.CacheResponseAsync(key, currentUserData, TimeSpan.FromSeconds(600));
                        }
                        break;
                    default:
                        throw new Exception("Something went wrong");
                }
            }


        }
    }
}
