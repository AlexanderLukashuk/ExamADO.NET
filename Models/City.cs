using System;

namespace Exam.Models
{
    public class City
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public Guid? CountryId { get; set; }
        public Country Country { get; set; }
        public bool IsCapital { get; set; }
    }
}