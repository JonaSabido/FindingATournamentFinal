using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO.Pipelines;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FindingATournamentApp.Application.Services;
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
// Parcial 3
// Entrega: 01/12/2021

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
        private readonly IValidator<ClubUpdateRequest> _updateValidator;
        
        public ClubesController(IHttpContextAccessor httpContext, IClubRepository repository, IMapper mapper, IValidator<ClubCreateRequest> createValidator, IValidator<ClubUpdateRequest> updateValidator){
            this._httpContext = httpContext;
            _repository = repository;
            this._mapper = mapper;
            this._createValidator = createValidator;
            this._updateValidator = updateValidator;
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
            if(respuesta.Count() == 0){
                return NoContent();
                //return NotFound("No fue posible encontrar un club con los filtros proporcionados");
            }            
            return Ok(respuesta);
        }

        [HttpGet]
        [Route("Id/{id:int}")]
        public async Task<IActionResult> GetById(int id){
            
            var club = await _repository.GetById(id);
            var respuesta = _mapper.Map<Clube,ClubResponse>(club);

            if(respuesta == null){
                return NoContent();
                //return NotFound("No fue posible encontrar un club con el id proporcionado");
            }

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

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody]Clube club)
        {
            if(id <= 0 || !_repository.Exist(i => i.Id == id))
                return NotFound("El registro no fué encontrado, veifica tu información...");

            club.Id = id;

            ClubUpdateRequest clubRequest = new ClubUpdateRequest();
            clubRequest.ClubName = club.ClubName;
            clubRequest.ClubAddress = club.ClubAddress ;
            clubRequest.ClubContactNumber = club.ClubContactNumber;
            clubRequest.ClubLatitude = club.ClubLatitude;
            clubRequest.ClubLength = club.ClubLength;
            clubRequest.ClubSchedule = club.ClubSchedule;

            var validationResult2 = await _updateValidator.ValidateAsync(clubRequest);
            
            if(!validationResult2.IsValid){
                return UnprocessableEntity(validationResult2.Errors.Select(x => $"Error: {x.ErrorMessage}"));
            }

            ClubService service = new ClubService(_repository);
            var validate = service.ValidateUpdate(club);
            
            if(!validate)
                return UnprocessableEntity("No es posible realizar la modificación, verifica tu información...");

            var update = await _repository.Update(id, club);

            if(!update)
                return Conflict("Ocurrió un fallo al intentar realizar la modificación...");

            return Ok("Se han actualizado los datos correctamente...");
            
        }
    }
}