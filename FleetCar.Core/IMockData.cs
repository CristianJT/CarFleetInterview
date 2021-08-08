using FleetCar.Core.Models;
using System.Collections.Generic;

namespace FleetCar.Core
{
    public interface IMockData
    {
        IEnumerable<Car> GetCars();

        IEnumerable<Department> GetDepartments();

        IEnumerable<Department> GetDepartments(IEnumerable<int> ids);
    }
}
