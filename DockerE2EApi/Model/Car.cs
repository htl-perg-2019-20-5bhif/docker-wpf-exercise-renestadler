using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DockerE2EApi.Model
{
    public class Car
    {
        public int CarId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        public List<Booking> Bookings { get; set; }
    }
}
