using ContactMaintenance.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactMaintenance.Atrributes
{
    public class LoggerAttributes : IActionFilter
    {
        private ILogger<ControllerBase> _logger;

        public LoggerAttributes(ILogger<ControllerBase> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var timestamp = DateTime.Now;
            var actionName = context.ActionDescriptor.DisplayName;
            _logger.LogInformation($"The operation \"{ actionName}\" ended at {timestamp.ToShortDateString()}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var timestamp = DateTime.Now;
            var actionName = context.ActionDescriptor.DisplayName;
            _logger.LogInformation($"The operation \"{ actionName}\" started at {timestamp.ToShortDateString()}");
        }
    }
}
