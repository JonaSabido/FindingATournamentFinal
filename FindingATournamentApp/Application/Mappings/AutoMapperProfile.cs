using System.Reflection.Metadata.Ecma335;
using System;
using AutoMapper;
using FindingATournamentApp.Domain.Dtos.Responses;
using FindingATournamentApp.Domain.Dtos.Requests;
using FindingATournamentApp.Domain.Entities;

namespace FindingATournamentApp.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile(){
            CreateMap<Clube, ClubResponse>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ClubName))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.ClubAddress))
            .ForMember(dest => dest.ContactNumber, opt => opt.MapFrom(src => src.ClubContactNumber))
            .ForMember(dest => dest.Schedule, opt => opt.MapFrom(src => src.ClubSchedule));

            CreateMap<ClubCreateRequest, Clube>()
            .ForPath(dest => dest.ClubName, opt => opt.MapFrom(src => src.Name))
            .ForPath(dest => dest.ClubAddress, opt => opt.MapFrom(src => src.Address))
            .ForPath(dest => dest.ClubContactNumber, opt => opt.MapFrom(src => src.ContactNumber))
            .ForPath(dest => dest.ClubLatitude, opt => opt.MapFrom(src => src.Latitude))
            .ForPath(dest => dest.ClubLength, opt => opt.MapFrom(src => src.Lenght))
            .ForPath(dest => dest.ClubSchedule, opt => opt.MapFrom(src => src.Schedule));
        }
    }
}