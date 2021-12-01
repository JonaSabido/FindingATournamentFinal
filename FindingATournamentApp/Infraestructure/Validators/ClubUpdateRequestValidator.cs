using System;
using FluentValidation;
using FindingATournamentApp.Domain.Dtos.Requests;
using FindingATournamentApp.Domain.Interfaces;

namespace FindingATournamentApp.Infraestructure.Validators
{
    public class ClubUpdateRequestValidator : AbstractValidator<ClubUpdateRequest>
    {
        private readonly IClubRepository _repository;

        public ClubUpdateRequestValidator(IClubRepository repository)
        {
            this._repository = repository;

            RuleFor(x => x.ClubName).NotNull();;
            RuleFor(x => x.ClubAddress).NotNull();
            RuleFor(x => x.ClubContactNumber).NotNull()
                    .Must(ContactNumerSinLetras).WithMessage("El formato de número telefónico no es válido") 
                    .Length(10,10).WithMessage("El número telefónico debe ser de 10 digitos");
            RuleFor(x => x.ClubLatitude).NotNull();
            RuleFor(x => x.ClubLength).NotNull();
            RuleFor(x => x.ClubSchedule).NotNull();
        }

        public bool ContactNumerSinLetras(string number){

            string str = Convert.ToString(number);
            if(number != null){
                int length = str.Length;
                char[] arreglo = str.ToCharArray();
                
                for(int i = 0; i<length; i++){

                    if(arreglo[i] != '0' && arreglo[i] != '1' && arreglo[i] != '2' && arreglo[i] != '3' && arreglo[i] != '4' && arreglo[i] != '5' && arreglo[i] != '6' && arreglo[i] != '7' && arreglo[i] != '8' && arreglo[i] != '9'){
                        return false;
                    }
                }

                return true;
            }
            
            return false;

        }

    }
}