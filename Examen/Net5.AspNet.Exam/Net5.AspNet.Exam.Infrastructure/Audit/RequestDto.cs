using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net5.AspNet.Exam.Infrastructure.Audit
{
    public class RequestDto
    {
        public string Body { get; set; }
        public string Headers { get; set; }
        public string QueryString { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Path { get; set; }
        public string Method { get; set; }
        public string Protocol { get; set; }
    }
}
