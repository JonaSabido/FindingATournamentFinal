using System;
using System.Collections.Generic;

#nullable disable

namespace FindingATournamentApp.Domain.Entities
{
    public partial class Clube
    {
        public int Id { get; set; }
        public string ClubName { get; set; }
        public string ClubAddress { get; set; }
        public string ClubContactNumber { get; set; }
        public double ClubLatitude { get; set; }
        public double ClubLength { get; set; }
        public string ClubSchedule { get; set; }
    }
}
