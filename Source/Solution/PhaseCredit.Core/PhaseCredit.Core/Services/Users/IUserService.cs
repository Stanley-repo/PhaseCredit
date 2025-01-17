﻿using PhaseCredit.Core.DTOs.Authentications;
using PhaseCredit.Data.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PhaseCredit.Core.Services.Users
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> FindUserAsync(string username);
        Task<UserLoginResponse> GenerateUserTokenAsync(Claim[] claims);
    }
}
