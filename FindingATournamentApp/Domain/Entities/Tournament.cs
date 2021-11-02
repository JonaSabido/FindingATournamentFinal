using System;
using System.Collections.Generic;

#nullable disable

namespace FindingATournamentApp.Domain.Entities
{
    public partial class Tournament
    {
        public int Id { get; set; }
        public int IdDiscTournament { get; set; }
        public string TournamentName { get; set; }
        public int NumTeams { get; set; }
        public bool AvailableTeams { get; set; }
        public string RegisCost { get; set; }
        public string TournamentRules { get; set; }
        public int NumberRounds { get; set; }
        public string TournamentType { get; set; }

        public virtual Discipline IdDiscTournamentNavigation { get; set; }
    }
}
