using System.Collections.Generic;

namespace FleetCar.Core.Models
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Reservation> Reservations { get; set; }

        public Department()
        {
            Reservations = new HashSet<Reservation>();
        }
    }
}
