using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericCore.ViewModels.Wrappers
{
    public class APIResponseResult<T> : APIResponse
    {
        public T Result { get; set; }
    }
}
