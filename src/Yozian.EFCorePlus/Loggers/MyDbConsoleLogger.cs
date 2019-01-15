using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Yozian.EFCorePlus.Loggers
{
    internal class MyDbConsoleLogger : ILoggerProvider
    {
        private static Dictionary<string, MyLogger> cache = new Dictionary<string, MyLogger>();

        public ILogger CreateLogger(string categoryName)
        {
            if (!cache.ContainsKey(categoryName))
            {
                cache[categoryName] = new MyLogger(categoryName);
            }

            return cache[categoryName];
        }

        public void Dispose()
        {

        }

        private class MyLogger : ILogger
        {

            private MyLogger() { }

            public string CategoryName { get; set; }

            public MyLogger(string categoryName)
            {
                this.CategoryName = categoryName;
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return logLevel == LogLevel.Debug;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                if (string.IsNullOrEmpty(eventId.Name))
                {
                    return;
                }

                if (eventId.Name.Equals("Microsoft.EntityFrameworkCore.Database.Command.CommandExecuting"))
                {
                    var msg = formatter(state, exception);
                    System.Diagnostics.Debug.WriteLine(msg);
                    Console.WriteLine(msg);
                }
            }

            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }
        }
    }


}
