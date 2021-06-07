using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Euro.TaskManagement.Api.Logging
{
    public class Logger
    {
        private static ILogger logger;

        protected Logger()
        {
        }

        public static void RegisterLogger(ILogger<Logger> _logger)
        {
            logger = _logger;
        }

        /// <summary>
        /// Logs exception events
        /// </summary>
        /// <param name="exception"></param>
        public static void LogException(Exception exception)
        {
            logger.LogError(exception, exception.Message);
        }

        /// <summary>
        /// Logs Error events
        /// </summary>
        /// <param name="message"></param>
        public static void LogError(string message)
        {
            logger.LogError(message);
        }

        /// <summary>
        /// Logs Warning events
        /// </summary>
        /// <param name="message"></param>
        public static void LogWarn(string message)
        {
            logger.LogWarning(message);
        }

        /// <summary>
        /// Logs Info events
        /// </summary>
        /// <param name="message"></param>
        public static void LogInfo(string message)
        {
            logger.LogInformation(message);
        }

        /// <summary>
        /// Logs Debug events
        /// </summary>
        /// <param name="message"></param>
        public static void LogDebug(string message)
        {
            logger.LogDebug(message);
        }
    }
}