using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;
using NLog.Filters;
using NLog.Targets;
using NLog.Targets.Wrappers;
using SimpleInjector;
using LogLevel = NLog.LogLevel;

namespace Euro.TaskManagement.Api.Logging
{
    public static class LoggerExtension
    {
        private const int timeToSleepBetweenBatches = 100;

        public static void AddCustomLogger(this ILoggingBuilder configureLogging, IConfiguration configuration)
        {
            var config = new LoggingConfiguration();
            configureLogging.AddConfiguration(configuration.GetSection("Logging"));
            // Create targets and add them to the configuration 
            var fileTarget = GetFileTarget();
            config.AddTarget(fileTarget);

            // Define logging rules
            LoggingRule logRule = new LoggingRule("*", LogLevel.Trace, fileTarget);

            // Define filter to ignore logs from Microsoft DLLs
            var filterToIgnoreAllMicrosoftLogs = new ConditionBasedFilter
            {
                Condition = "starts-with(logger, 'Microsoft.')",
                Action = FilterResult.Ignore
            };

            logRule.Filters.Add(filterToIgnoreAllMicrosoftLogs);

            // Define wrappers
            var asyncWrapper = GetAsyncTargetWrapper();
            asyncWrapper.WrappedTarget = fileTarget;
            SimpleConfigurator.ConfigureForTargetLogging(asyncWrapper, LogLevel.Trace);

            // Activate the configuration
            config.LoggingRules.Add(logRule);
            LogManager.Configuration = config;

            //Add nlog by default
            configureLogging.AddNLog(config);          

        }

        private static FileTarget GetFileTarget()
        {
            return new FileTarget
            {
                // Set target properties 
                Layout = @"${date:format=HH\:mm\:ss} | ${logger} | ${level} | ${message} ${exception} ${stacktrace} | ${aspnet-TraceIdentifier} | ${aspnet-Request-IP} | ${aspnet-Item}",
                FileName = "${basedir}/Logs/Log-${shortdate}.txt", // ${basedir} -> bin\debug folder
                Name = "FileTarget",
                KeepFileOpen = false,
                CreateDirs = true,
                ConcurrentWrites = true,
                ArchiveOldFileOnStartup = true,
                //ArchiveAboveSize = WIP : Need to have a max file size
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static AsyncTargetWrapper GetAsyncTargetWrapper()
        {
            return new AsyncTargetWrapper
            {
                TimeToSleepBetweenBatches = timeToSleepBetweenBatches, // WIP : Need to confirm on this
                OverflowAction = AsyncTargetWrapperOverflowAction.Grow
            };
        }

        public static void InitiliazeLogger(Container container)
        {
            var logger = container.GetInstance<ILogger<Logger>>();
            Logger.RegisterLogger(logger);
        }

    }
}
