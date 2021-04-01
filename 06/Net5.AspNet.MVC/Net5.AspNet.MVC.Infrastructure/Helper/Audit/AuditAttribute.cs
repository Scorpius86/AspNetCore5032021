using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Net5.AspNet.MVC.Infrastructure.Data.Audit.Entities;
using Net5.AspNet.MVC.Infrastructure.Data.Audit.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Net5.AspNet.MVC.Infrastructure.Helper.Audit
{
    public class AuditAttribute : TypeFilterAttribute
    {
        public AuditAttribute():base(typeof(AuditAttributeImpl))
        {

        }

        public class AuditAttributeImpl:IActionFilter
        {
            private readonly AuditUnitOfWork _auditUnitOfWork;
            private readonly Stopwatch _stopwatch;
            private DateTime _time;
            public AuditAttributeImpl(AuditUnitOfWork auditUnitOfWork)
            {
                _stopwatch = new Stopwatch();
                _auditUnitOfWork = auditUnitOfWork;
                _time = new DateTime();
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                _stopwatch.Stop();
                AuditLog auditLog = GetInfo(context);
                SaveInfo(auditLog);
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                _stopwatch.Reset();
                _stopwatch.Start();
                _time = DateTime.Now;
            }

            private AuditLog GetInfo(ActionExecutedContext context)
            {
                var executionTime = _stopwatch.ElapsedMilliseconds;
                var bodyStr = "";
                var headersStr = "";
                var req = context.HttpContext.Request;
                var header = context.HttpContext.Request.Headers;

                using (StreamReader reader = new StreamReader(req.Body, Encoding.UTF8, true, 1024, true)) { bodyStr = reader.ReadToEndAsync().Result; }
                headersStr = string.Join("\n", header.ToList().Select(s => $"{s.Key} => {s.Value}").ToArray());

                AuditLog auditLog = new AuditLog
                {
                    Time = _time,
                    UserName = context.HttpContext.User.Identity.Name,
                    Service = ((Controller)context.Controller).ControllerContext.ActionDescriptor.ControllerTypeInfo.Name,
                    Action = ((ControllerActionDescriptor)context.ActionDescriptor).ActionName,
                    Duration = executionTime,
                    Ipaddress = context.HttpContext.Connection.RemoteIpAddress?.ToString(),
                    Browser = context.HttpContext.Request.Headers["User-Agent"],
                    Request = JsonSerializer.Serialize(new RequestDto
                    {
                        Body = bodyStr,
                        Headers = headersStr,
                        Host = req.Host.Host,
                        Port = req.Host.Port.ToString(),
                        Method = req.Method,
                        Path = req.Path,
                        Protocol = req.Protocol,
                        QueryString = req.QueryString.ToString()
                    }),
                    Response = "",
                    Error = ""
                };

                return auditLog;
            }
            private void SaveInfo(AuditLog auditLog)
            {
                _auditUnitOfWork.AuditLogRepository.Insert(auditLog);
                _auditUnitOfWork.Save();
            }
        }
    }     
}
