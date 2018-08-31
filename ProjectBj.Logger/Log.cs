using NLog;

namespace ProjectBj.Logger
{
    public static class Log
    {
        private static NLog.Logger _logger;

        static Log()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public static void Trace(string message)
        {
            _logger.Trace(message);
        }

        public static void Debug(string message)
        {
            _logger.Debug(message);
        }

        public static void Info(string message)
        {
            _logger.Info(message);
        }

        public static void Warn(string message)
        {
            _logger.Warn(message);
        }

        public static void Error(string message)
        {
            _logger.Error(message);
        }

        public static void Fatal(string message)
        {
            _logger.Fatal(message);
        }
    }
}
