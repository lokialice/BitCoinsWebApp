namespace BitCoinsWebApp.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class Logging
    {
        public static void TrackError(string connectionString, Exception ex, String location)
        {
            Log4NetLogger logger2 = new Log4NetLogger(connectionString);
            logger2.Error(location, ex);
        }

        public static void WriteInfo(string connectionString, string Message)
        {
            Log4NetLogger logger2 = new Log4NetLogger(connectionString);
            logger2.Info(Message);
        }        
    }
}
