using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericCore.Models
{
    public class AppSettings
    {
        public string SQLServerConnectionString { get; set; }
        public JWTSettings JWTSettings { get; set; }
        public SMTPConfiguration LoggingSMTPConfiguration { get; set; }
        public List<string> DevelopersEmails { get; set; }
    }

    public class JWTSettings
    {
        public string Secret { get; set; }
        public int ExpireAfterHours { get; set; }
    }

    public class SMTPConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
    }
}
