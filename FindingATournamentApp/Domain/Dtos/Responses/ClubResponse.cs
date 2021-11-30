using System;

namespace FindingATournamentApp.Domain.Dtos.Responses
{
    public class ClubResponse
    {
        public int Id {get; set;}
        public string Name{get; set;}
        public string Address{get; set;}
        public string ContactNumber{get; set;}
        public string Schedule{get; set;}
    }
}