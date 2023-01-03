using MessageConsumer.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;

namespace MessageConsumer.Application.Services
{
    public class LogArchiveService : ILogArchiveService
    {
        private readonly IConfiguration _configuration;
        public LogArchiveService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SaveLog(string Log, string Exception)
        {

            try
            {

                var data = DateTime.Today.ToString("dd-MM-yyyy");
                var urlBase = _configuration.GetSection("UrlLogException").Get<string>();
                string path = @urlBase + data;
                var information = Log + Environment.NewLine + Exception;

                if (Directory.Exists(path))
                {
                    var pasta = Path.Combine(path, Log + ".txt");

                    using (StreamWriter file = File.CreateText(pasta))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, information);
                    }
                }
                else
                {
                    Directory.CreateDirectory(path);
                    var pasta = Path.Combine(path, Log + ".txt");
                    using (StreamWriter file = File.CreateText(pasta))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, information);
                    }
                }

            }
            catch (Exception ex)
            {
              //SAVE SOME LOG.   
            }
        }
    }
}
