using System.Collections.Generic;

namespace FleetCar.Core.Models
{
    public class Car
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Version { get; set; }

        public string Description { get; set; }

        public int? Year { get; set; }

        public decimal ValueHour { get; set; }

        public ICollection<Reservation> Reservations { get; set; }

        public Car()
        {
            Reservations = new HashSet<Reservation>();
        }
    }
}
