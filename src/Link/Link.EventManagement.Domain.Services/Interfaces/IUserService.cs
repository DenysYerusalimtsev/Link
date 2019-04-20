﻿using Link.EventManagement.Domain.Model.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Link.EventManagement.Domain.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetUsers(IEnumerable<UserId> expertsId);
    }
}