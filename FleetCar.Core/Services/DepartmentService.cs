using FleetCar.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleetCar.Core.Services
{
    public class DepartmentService : IDepartment
    {
        private readonly IMockData _dbContext;

        public DepartmentService(IMockData dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Department> Get()
        {
            return _dbContext.GetDepartments();
        }
    }
}
