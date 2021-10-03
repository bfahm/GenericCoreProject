using GenericCore.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericCore.ViewModels.Wrappers
{
    public class APIResponse
    {
        public bool Status { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
