using System;
using System.Collections.Generic;
using System.Text;

namespace MessageConsumer.Application.Interfaces
{
    public interface ILogArchiveService
    {
        void SaveLog(string Log, string Exception);
    }
}
