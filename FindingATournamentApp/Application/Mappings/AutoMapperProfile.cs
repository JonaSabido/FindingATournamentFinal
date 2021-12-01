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
            .ForPath(dest => dest.ClubName, opt => opt.MapFrom(src => src.ClubName))
            .ForPath(dest => dest.ClubAddress, opt => opt.MapFrom(src => src.ClubAddress))
            .ForPath(dest => dest.ClubContactNumber, opt => opt.MapFrom(src => src.ClubContactNumber))
            .ForPath(dest => dest.ClubLatitude, opt => opt.MapFrom(src => src.ClubLatitude))
            .ForPath(dest => dest.ClubLength, opt => opt.MapFrom(src => src.ClubLength))
            .ForPath(dest => dest.ClubSchedule, opt => opt.MapFrom(src => src.ClubSchedule));

            CreateMap<Clube, ClubUpdateRequest>()
            .ForPath(dest => dest.ClubName, opt => opt.MapFrom(src => src.ClubName))
            .ForPath(dest => dest.ClubAddress, opt => opt.MapFrom(src => src.ClubAddress))
            .ForPath(dest => dest.ClubContactNumber, opt => opt.MapFrom(src => src.ClubContactNumber))
            .ForPath(dest => dest.ClubLatitude, opt => opt.MapFrom(src => src.ClubLatitude))
            .ForPath(dest => dest.ClubLength, opt => opt.MapFrom(src => src.ClubLength))
            .ForPath(dest => dest.ClubSchedule, opt => opt.MapFrom(src => src.ClubSchedule));
        }
    }
}