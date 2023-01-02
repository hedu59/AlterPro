using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageConsumer.Application.Models
{
    public class EmailConfiguration
    {
        public EmailConfiguration()
        {

        }

        public string SmtpClient { get; set; }
        public bool EnableSsl { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public string From { get; set; }
    }
}
