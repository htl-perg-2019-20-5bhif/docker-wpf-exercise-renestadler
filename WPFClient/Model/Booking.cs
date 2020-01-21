using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DockerE2EApi.Model
{
    public class Booking
    {
        [JsonPropertyName("bookingId")]
        public int BookingId { get; set; }

        [JsonPropertyName("bookingDate")]
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; }
    }
}