namespace BitCoinsWebApp.Utilities
{
    using log4net;
    using log4net.Core;
    using System;
    using System.Collections.Generic;
    using System.Data.EntityClient;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Log4NetLogger : ILogger
    {
        private readonly ILog _logger;

        private Log4NetLogger()
        {
            // shoult not create logger without name
        }

        private string GetLoggerName(string sqlConnectionString)
        {
            var sqlBuilder = new SqlConnectionStringBuilder(sqlConnectionString);
            return sqlBuilder.DataSource + "-" + sqlBuilder.InitialCatalog;
        }

        public Log4NetLogger(string efConnectionString)
        {
            var efBuilder = new EntityConnectionStringBuilder(efConnectionString);
            var loggerName = GetLoggerName(efBuilder.ProviderConnectionString);
            _logger = LogManager.GetLogger(loggerName);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(Exception x)
        {
            Error(LogUtility.BuildExceptionMessage(x));
        }

        public void Error(string message, Exception x)
        {
            _logger.Error(message, x);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(Exception x)
        {
            Fatal(LogUtility.BuildExceptionMessage(x));
        }

        public bool IsEnabledFor(Level level)
        {
            throw new NotImplementedException();
        }

        public void Log(LoggingEvent logEvent)
        {
            throw new NotImplementedException();
        }

        public void Log(Type callerStackBoundaryDeclaringType, Level level, object message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public log4net.Repository.ILoggerRepository Repository
        {
            get { throw new NotImplementedException(); }
        }
    }
}
