using System;

namespace FindingATournamentApp.Domain.Dtos.Requests
{
    public class ClubUpdateRequest
    {
        public string ClubName{get; set;}
        public string ClubAddress {get; set;}
        public string ClubContactNumber {get; set;}
        public double? ClubLatitude {get; set;}
        public double? ClubLength {get; set;}
        public string ClubSchedule {get; set;}
    }
}