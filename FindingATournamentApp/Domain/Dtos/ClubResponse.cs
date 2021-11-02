using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindingATournamentApp.Domain.Dtos
{
    public record ClubResponse (string ClubName, string ClubAddress, string ClubContactNumber, string ClubSchedule);
}