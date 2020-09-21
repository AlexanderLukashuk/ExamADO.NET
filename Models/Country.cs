using System;
using System.Collections.Generic;

namespace Exam.Models
{
    public class Country
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public List<City> Cities { get; set; } = new List<City>();
    }
}