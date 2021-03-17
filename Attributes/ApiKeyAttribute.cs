using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;

namespace ApiKey.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {

        private const string ApiKeyName = "api_key";
        private const string ApiKeyValue = "rodrigo_bento_IlTevUM/z0ey3NwCV/unWg==";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            StringValues extractedApiKey;
            if (!context.HttpContext.Request.Query.TryGetValue(ApiKeyName, out extractedApiKey))
            {
                context.Result = new ContentResult
                {
                    StatusCode = 401,
                    Content = "ApiKey não encontrada"
                };
                return;
            }

            if (!ApiKeyValue.Equals(extractedApiKey))
            {
                context.Result = new ContentResult
                {
                    StatusCode = 403,
                    Content = "Acesso não autorizado"
                };
                return;
            }

            await next();
        }
    }
}
