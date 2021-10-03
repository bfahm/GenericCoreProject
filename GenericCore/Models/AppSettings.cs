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
    }

    public class JWTSettings
    {
        public string Secret { get; set; }
        public int ExpireAfterHours { get; set; }
    }
}
