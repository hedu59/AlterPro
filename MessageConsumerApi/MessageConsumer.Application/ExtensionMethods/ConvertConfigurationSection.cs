using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageConsumer.Application.ExtensionMethods
{
    public static class ConfigurationSection
    {
        public static T ConvertConfigurationSection<T>(this IConfiguration configuration)
        {
            return configuration.ConvertConfigurationSection<T>();
        }
    }
}
