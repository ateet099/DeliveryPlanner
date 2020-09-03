using System;
using System.Runtime.InteropServices;

namespace DeliveryPlanner.Helper
{
    class Logger
    {
       public static void LogInfo(double duration, string message, [Optional] string payload)
        {
            Log(LogLevel.INFO, GlobalContext.UserName, GlobalContext.ClientRequestId, duration, message, payload);
        }

        public static void LogError(double duration, string message, [Optional] string payload)
        {
            Log(LogLevel.ERROR, GlobalContext.UserName, GlobalContext.ClientRequestId, duration, message, payload);
        }

        private static void Log(
            LogLevel logLevel
            , string UserName
            , string ClientRequestId
            , double Duration
            , string Message
            , string Payload = ""
        )
        {
                Console.WriteLine($"{{\"timestamp\":\"{DateTime.UtcNow.ToString("o")}\"" +
                                  $", \"level\":\"{logLevel.ToString()}\"" +
                                  $", \"user\":\"{UserName}\"" +
                                  $", \"client_request_id\":\"{ClientRequestId}\"" +
                                  $", \"duration\":\"{Duration}\"" +
                                  $", \"message\":\"{Message}\"" +
                                  $", \"payload\":\"{Payload}\"}}");
        }

        private enum LogLevel
        {
            ERROR,
            WARN,
            INFO
        }
    }
}
