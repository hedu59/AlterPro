using System;
using System.Collections.Generic;
using System.Text;

namespace MessageConsumer.Infra
{
    public class DbConfiguration
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
