using System;
using System.Collections.Generic;

namespace Logging
{
    public interface ILogingServices
    {
        void Log(Log model);
        void Log(Exception ex);
        List<Log> GetResult();
        List<Log> GetResult(string condition);
    }
}