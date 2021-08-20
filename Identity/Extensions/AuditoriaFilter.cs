using KissLog;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;


namespace Identity.Extensions
{
    public class AuditoriaFilter : IActionFilter
    {
        private readonly ILogger _logger;
        public AuditoriaFilter(ILogger logger)
        {
            _logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context) // antes execução
        {
        }

        public void OnActionExecuting(ActionExecutingContext context) // apos execução
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var menssage = context.HttpContext.User.Identity.Name + "Acessou: " +
                               context.HttpContext.Request.GetDisplayUrl();
                _logger.Info(menssage);
            }
        }
    }
}
