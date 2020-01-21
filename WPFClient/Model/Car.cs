using DockerE2EApi.Model;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WPFClient.Model
{
    public class Car
    {
        [JsonPropertyName("carId")]
        public int CarId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("brand")]
        public string Brand { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("bookings")]
        public List<Booking> Bookings { get; set; }

        public DateTime Current { get; set; }

        public bool CanBook()
        {
            return Bookings.Find(b => b.BookingDate == Current) == null;
        }
    }
}
