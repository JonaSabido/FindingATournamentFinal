using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO.Pipelines;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FindingATournamentApp.Domain.Entities;
using FindingATournamentApp.Domain.Dtos.Requests;
using FindingATournamentApp.Domain.Dtos.Responses;
using FindingATournamentApp.Infraestructure.Repositories;
using FindingATournamentApp.Domain.Interfaces;
using FluentValidation;
using AutoMapper;
using Microsoft.Extensions.Options;
using FluentValidation.Results;

// Universidad Tecnológica Metropolitana
// Aplicaciones Web Orientada a Servicios
// Maestro: Joel Ivan Chuc UC
// Actividad: Solucion Tecnica del Proyecto
// Integrantes del equipo: Balam Rosas Christian Jesús, Herrera Caro Abraham Enrique, Sabido Reynoso Jonathan Missael
// 4C
// Parcial 2
// Entrega: 02/11/2021

namespace FindingATournamentApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubesController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IClubRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<ClubCreateRequest> _createValidator;
        
        public ClubesController(IHttpContextAccessor httpContext, IClubRepository repository, IMapper mapper, IValidator<ClubCreateRequest> createValidator){
            this._httpContext = httpContext;
            _repository = repository;
            this._mapper = mapper;
            this._createValidator = createValidator;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var clubes = await _repository.GetAll();
            var respuesta = _mapper.Map<IEnumerable<Clube>, IEnumerable<ClubResponse>>(clubes);


            return Ok(respuesta);
        }

        [HttpGet]
        [Route("Filters")]
        public async Task<IActionResult> GetByFilter(Clube club)
        {
            var clubes = await _repository.GetByFilter(club);
            var respuesta = _mapper.Map<IEnumerable<Clube>,IEnumerable<ClubResponse>>(clubes);
            return Ok(respuesta);
        }

        /*
        [HttpGet]
        [Route("Find")]
        public IActionResult GetByFiltersClub([FromBody]ClubRequest club)
        {
            var repository = new ClubRepository();
            var club1 = CreateObjectFromDto(club);
            var clubes = repository.GetByFiltersClub(club1);
            var respuesta = clubes.Select(x =>CreateDtoFromObject(x));

            return Ok(respuesta);
        }
        */


        [HttpPost]
        public async Task<IActionResult> Create([FromBody]ClubCreateRequest club){
            
            var validationResult = await _createValidator.ValidateAsync(club);
            if(!validationResult.IsValid){
                return UnprocessableEntity(validationResult.Errors.Select(x => $"Error: {x.ErrorMessage}"));
            }
            var entity = _mapper.Map<ClubCreateRequest, Clube>(club);
            var id = await _repository.Create(entity);
            if(id <= 0){
                return Conflict("No se puede realizar el registro...");
            }
            var urlresult = $"https://{_httpContext.HttpContext.Request.Host.Value}/api/club/{id}";
            return Created(urlresult, id);
        }
    }
}