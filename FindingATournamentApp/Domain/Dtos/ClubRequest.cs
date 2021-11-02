using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindingATournamentApp.Domain.Dtos
{
    public record ClubRequest (string ClubName, string ClubContactNumber);
}