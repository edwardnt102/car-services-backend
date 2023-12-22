using System;

namespace Logging
{
    public class Log
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string LogType { get; set; }
        public DateTime CreateDate { get; set; }
        public string Ip { get; set; }
        public string Browser { get; set; }
        public string MethodName { get; set; }
    }
}
