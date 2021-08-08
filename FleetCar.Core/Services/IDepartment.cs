using FleetCar.Core.Models;
using System.Collections.Generic;

namespace FleetCar.Core.Services
{
    public interface IDepartment
    {
        IEnumerable<Department> Get();
    }
}
