using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Yozian.EFCorePlus.Loggers;

namespace Yozian.EFCorePlus.Extensions
{
    public static class DbContextOptionsBuilderExtension
    {


        public static void AddExecutedCommandConsoleLogger(this DbContextOptionsBuilder @this)
        {
            var loggerProvider = new LoggerFactory();

            loggerProvider.AddProvider(new MyDbConsoleLogger());

            @this.UseLoggerFactory(loggerProvider);
        }
    }
}
