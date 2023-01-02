using System;
using System.Collections.Generic;
using System.Text;

namespace MessageConsumer.Application.Models
{
    public class Email
    {
        public Email(string to, string title, string body)
        {
            To = to;
            Title = title;
            Body = body;
        }

        public string To { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public static Email Create(string to, string title, string body)
            => new Email(to, title, body);
    }
}
