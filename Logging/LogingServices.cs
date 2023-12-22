using Logging.Helper;
using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logging
{
    public class LogingServices : ILogingServices
    {
        private ElasticClient client = null;
        private string defaultIndex = "";
        public LogingServices(IConfiguration configuration)
        {
            var url = configuration["elasticsearch:url"];
            defaultIndex = configuration["elasticsearch:index"];
            var uri = new Uri(url);
            var settings = new ConnectionSettings(uri);
            client = new ElasticClient(settings);
            settings.DefaultIndex(defaultIndex);
            var checkResult = client.Indices.Exists(defaultIndex);
            if (!checkResult.Exists)
            {
                var createIndexResponse = client.Indices.Create(defaultIndex,
                index => index.Map<Log>(x => x.AutoMap())
                );
            }
        }
        public void Log(Log model)
        {
            try
            {
                client.IndexAsync<Log>(model, null);
            }
            catch (Exception)
            {
            }
        }

        public void Log(Exception ex)
        {
            try
            {
                System.Diagnostics.StackTrace t = new System.Diagnostics.StackTrace();
                var model = new Log
                {
                    Ip = IPAddress.GetLocalIPAddress(),
                    Message = ex.ToString(),
                    LogType = LogType.Error,
                    Id = Guid.NewGuid(),
                    CreateDate = DateTime.Now,
                    Browser = ex.TargetSite.Name,
                    MethodName = t.GetFrame(1).GetMethod().Name
                };
                client.IndexAsync<Log>(model, null);
            }
            catch (Exception)
            {
            }

        }

        public List<Log> GetResult()
        {
            try
            {
                if (client.Indices.Exists(defaultIndex).Exists)
                {
                    var response = client.Search<Log>();
                    return response.Documents.ToList();
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Log> GetResult(string condition)
        {
            try
            {
                if (client.Indices.Exists(defaultIndex).Exists)
                {
                    var query = condition;

                    return client.SearchAsync<Log>(s => s
                    .From(0)
                    .Take(10)
                    .Query(qry => qry
                        .Bool(b => b
                            .Must(m => m
                                .QueryString(qs => qs
                                    .DefaultField("_all")
                                    .Query(query)))))).Result.Documents.ToList();
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
