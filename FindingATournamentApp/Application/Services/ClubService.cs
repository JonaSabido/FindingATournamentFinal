using System;
using FindingATournamentApp.Domain.Entities;
using FindingATournamentApp.Domain.Interfaces;

namespace FindingATournamentApp.Application.Services
{
    public class ClubService
    {
        private readonly IClubRepository _repository;
        public ClubService(IClubRepository repository){
            _repository = repository;
        }
        public bool ValidateUpdate(Clube club)
        {
            string name = Convert.ToString(club.ClubName);

            if(club.Id <= 0)
                return false;
            if(string.IsNullOrEmpty(club.ClubName))
                return false;
            if(string.IsNullOrEmpty(club.ClubAddress))
                return false;
            if(string.IsNullOrEmpty(club.ClubContactNumber)){
                return false;
            }
            else{
                string str = Convert.ToString(club.ClubContactNumber);
                int length = str.Length;
                char[] arreglo = str.ToCharArray();

                if(length != 10){
                    return false;
                }
                
                for(int i = 0; i<length; i++){

                    if(arreglo[i] != '0' && arreglo[i] != '1' && arreglo[i] != '2' && arreglo[i] != '3' && arreglo[i] != '4' && arreglo[i] != '5' && arreglo[i] != '6' && arreglo[i] != '7' && arreglo[i] != '8' && arreglo[i] != '9'){
                        return false;
                    }
                }
            }
            if(double.Equals(null, club.ClubLatitude))
                return false;
            if(double.Equals(null, club.ClubLength))
                return false;
            if(string.IsNullOrEmpty(club.ClubSchedule))
                return false;
            
            return true;
        }
    }
}