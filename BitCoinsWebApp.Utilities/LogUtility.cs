namespace BitCoinsWebApp.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;

    public class LogUtility
    {
        /// <summary>
        /// Builds the exception message.
        /// </summary>
        /// <param name="logException">The log exception.</param>
        /// <returns>System.String.</returns>
        public static string BuildExceptionMessage(Exception logException)
        {
            if (logException == null)
            {
                return string.Empty;
            }

            string noValue = "NULL";
            StringBuilder strErrorMsg = new StringBuilder();

            var currentHttpContext = System.Web.HttpContext.Current;
            if (currentHttpContext != null)
            {
             
                HttpRequest varCurrentRequest = null;
                try
                {
                    varCurrentRequest = currentHttpContext.Request;
                }
                catch { }
                if (varCurrentRequest != null)
                {
                    strErrorMsg.AppendFormat("{0}Error in Path : {1}", Environment.NewLine, varCurrentRequest.Path ?? noValue);

                    // Get the QueryString along with the Virtual Path
                    strErrorMsg.AppendFormat("{0}Raw Url : {1}", Environment.NewLine, varCurrentRequest.RawUrl ?? noValue);
                }
            }

            // Get the error message
            var message = logException.Message ?? noValue;
            strErrorMsg.AppendFormat("{0}Message : {1}", Environment.NewLine, logException.Message);

            // Source of the message
            var source = logException.Source ?? noValue;
            strErrorMsg.AppendFormat("{0}Source : {1}", Environment.NewLine, source);

            // Stack Trace of the error
            var stackTrace = logException.StackTrace ?? noValue;
            strErrorMsg.AppendFormat("{0}Stack Trace : {1}", Environment.NewLine, stackTrace);

            // Method where the error occurred
            var targetSite = logException.TargetSite == null ? noValue : logException.TargetSite.ToString();
            strErrorMsg.AppendFormat("{0}TargetSite : {1}", Environment.NewLine, targetSite);

            if (logException.InnerException != null)
            {
                var innerException = BuildExceptionMessage(logException.InnerException);
                strErrorMsg.AppendFormat("{0}InnerException : {1}", Environment.NewLine, innerException);
            }

            return strErrorMsg.ToString();
        }
    }
}
