using System;
using System.Collections.Generic;

#nullable disable

namespace FindingATournamentApp.Domain.Entities
{
    public partial class Discipline
    {
        public Discipline()
        {
            ServiceClubs = new HashSet<ServiceClub>();
            Tournaments = new HashSet<Tournament>();
        }

        public int Id { get; set; }
        public string DiscName { get; set; }

        public virtual ICollection<ServiceClub> ServiceClubs { get; set; }
        public virtual ICollection<Tournament> Tournaments { get; set; }
    }
}
