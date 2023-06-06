using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CoreLibrary.Models.Api.Utils
{
    public class CustomSqlLogger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (eventId.Id == RelationalEventId.CommandExecuted.Id)
            {
                Console.WriteLine(formatter(state, exception));
            }
        }
    }
}

