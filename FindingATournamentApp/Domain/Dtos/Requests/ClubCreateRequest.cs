using System;

namespace FindingATournamentApp.Domain.Dtos.Requests
{
    public class ClubCreateRequest
    {
        public string Name{get; set;}
        public string Address {get; set;}
        public string ContactNumber {get; set;}
        public float? Latitude {get; set;}
        public float? Lenght {get; set;}
        public string Schedule {get; set;}
    }
}