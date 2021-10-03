using GenericCore.Models;
using GenericCore.Models.Exceptions;
using Microsoft.AspNetCore.Connections;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.Email;
using System.Net;

namespace GenericCore.Helpers
{
    public static class SerilogInstaller
    {
        private static AppSettings _appSettings;
        private static LoggerConfiguration _loggerConfiguration;

        public static Logger CreateLogger(AppSettings appSettings)
        {
            _appSettings = appSettings;

            ConfigureExeclusions();
            ConfigureEmailLogging("Generic App Exception");
            ConfigureFileLogging("generic_app");

            return _loggerConfiguration.CreateLogger();
        }

        private static void ConfigureExeclusions()
        {
            _loggerConfiguration = new LoggerConfiguration()
                            .MinimumLevel.Information()
                            .Filter.ByExcluding(logEvent =>
                            {
                                return logEvent.Exception != null &&
                                (
                                    // Execluded exception types
                                    logEvent.Exception.GetType() == typeof(BusinessException) ||
                                    logEvent.Exception.GetType() == typeof(ConnectionResetException)
                                );
                            });
        }

        private static void ConfigureFileLogging(string filePreName)
        {
            _loggerConfiguration.WriteTo.Console()
                                           .WriteTo.File($"Logs/{filePreName}-.txt", rollingInterval: RollingInterval.Day);
        }

        private static void ConfigureEmailLogging(string emailSubject)
        {
            if (_appSettings.DevelopersEmails != null &&
                            _appSettings.DevelopersEmails.Count > 0 &&
                            _appSettings.LoggingSMTPConfiguration != null)
            {
                _loggerConfiguration.WriteTo.Email(
                    new EmailConnectionInfo
                    {
                        FromEmail = _appSettings.LoggingSMTPConfiguration.Mail,
                        ToEmail = string.Join(",", _appSettings.DevelopersEmails),
                        MailServer = _appSettings.LoggingSMTPConfiguration.Host,
                        NetworkCredentials = new NetworkCredential
                        {
                            UserName = _appSettings.LoggingSMTPConfiguration.Mail,
                            Password = _appSettings.LoggingSMTPConfiguration.Password,
                        },
                        EnableSsl = false,
                        Port = _appSettings.LoggingSMTPConfiguration.Port,
                        EmailSubject = emailSubject
                    },
                    restrictedToMinimumLevel: LogEventLevel.Error, batchPostingLimit: 5
                );
            }
        }
    }
}
