using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5.AspNet.MVC.Infrastructure.Helper.Audit
{
    public class AuditLogDto
    {
        public int AuditLogId { get; set; }
        public DateTime Time { get; set; }
        public string UserName { get; set; }
        public string Service { get; set; }
        public string Action { get; set; }
        public long Duration { get; set; }
        public string Ipaddress { get; set; }
        public string Browser { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public string Error { get; set; }
    }
}
