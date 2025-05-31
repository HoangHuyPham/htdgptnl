using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using be.Models;

namespace be.Repos.Interfaces
{
    public interface IRoleScheduleRepository : IRepository<RoleSchedule>
    {
        Task<List<RoleSchedule>> FindAllByRoleId(Guid id);
    }
}