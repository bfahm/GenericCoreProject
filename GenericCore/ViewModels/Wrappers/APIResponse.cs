using System.Collections.Generic;

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
