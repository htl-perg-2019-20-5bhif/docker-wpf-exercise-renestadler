using System;
using System.ComponentModel.DataAnnotations;

namespace DockerE2EApi.Model
{
    public class Booking
    {
        public int BookingId { get; set; }

        [Required]
        public DateTime BookingDate { get; set; }
    }
}