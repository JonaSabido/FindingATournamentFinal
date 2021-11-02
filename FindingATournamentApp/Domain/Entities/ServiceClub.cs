using System;
using System.Collections.Generic;

#nullable disable

namespace FindingATournamentApp.Domain.Entities
{
    public partial class ServiceClub
    {
        public int Id { get; set; }
        public int IdDiscServices { get; set; }
        public string ServicesName { get; set; }
        public string ServiceSchedule { get; set; }
        public int NumPeople { get; set; }
        public bool SpeciEqReq { get; set; }
        public string SpeciEquip { get; set; }
        public bool DiferentCapacity { get; set; }

        public virtual Discipline IdDiscServicesNavigation { get; set; }
    }
}
