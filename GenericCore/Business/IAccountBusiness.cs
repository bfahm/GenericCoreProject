﻿using GenericCore.Helpers;
using GenericCore.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericCore.Services
{
    public interface IAccountBusiness
    {
        Task<APIResponse<string>> RegisterAsync(RegistrationRequestViewModel request);
        Task<APIResponse<string>> LoginAsync(LoginRequestViewModel request);
    }
}
