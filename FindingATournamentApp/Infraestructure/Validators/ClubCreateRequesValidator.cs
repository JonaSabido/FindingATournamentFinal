using System;
using FluentValidation;
using FindingATournamentApp.Domain.Dtos.Requests;
using FindingATournamentApp.Domain.Interfaces;

namespace FindingATournamentApp.Infraestructure.Validators
{
    public class ClubCreateRequestValidator : AbstractValidator<ClubCreateRequest>
    {
        private readonly IClubRepository _repository;

        public ClubCreateRequestValidator(IClubRepository repository)
        {
            this._repository = repository;

            RuleFor(x => x.Name).NotNull();
            RuleFor(x => x.Address).NotNull();
            RuleFor(x => x.ContactNumber).NotNull();
            RuleFor(x => x.Latitude).NotNull();
            RuleFor(x => x.Lenght).NotNull();
            RuleFor(x => x.Schedule).NotNull();
        }

    }
}