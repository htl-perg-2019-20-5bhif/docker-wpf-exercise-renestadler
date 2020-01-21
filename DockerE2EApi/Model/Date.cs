using System;
using System.ComponentModel.DataAnnotations;

namespace DockerE2EApi.Model
{
    public class Date
    {

        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }
    }
}
