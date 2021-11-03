using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FindingATournamentApp.Domain.Entities;
using FindingATournamentApp.Domain.Dtos;
using FindingATournamentApp.Infraestructure.Repositories;

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

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var repository = new ClubRepository();
            var clubes = repository.GetAll();
            var query = clubes.Select(c => CreateDtoFromObject(c));

            return Ok(query);
        }

        [HttpGet]
        [Route("GetContains/{word}")]
        public IActionResult GetContains(string word)
        {
            var repository = new ClubRepository();
            var clubes = repository.GetContains(word);
            var query = clubes.Select(c => CreateDtoFromObject(c));

            return Ok(query);
        }

        [HttpGet]
        [Route("GetByPhone/{number}")]

        public IActionResult GetByPhone(string number){

            var repository = new ClubRepository();
            var clubes = repository.GetByPhone(number);
            var query = clubes.Select(c => CreateDtoFromObject(c));

            return Ok(query);
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
        private ClubResponse CreateDtoFromObject(Clube club)
        {
            var dto = new ClubResponse
            (
                ClubName: club.ClubName,
                ClubAddress: club.ClubAddress,
                ClubContactNumber : club.ClubContactNumber,
                ClubSchedule: club.ClubSchedule
            );
            return dto;
        }

        public Clube CreateObjectFromDto (ClubRequest dto)
        {
            var club = new Clube
            {
                Id=0,
                ClubName= string.Empty,
                ClubAddress= string.Empty,
                ClubContactNumber= string.Empty,
                ClubLatitude= 0,
                ClubLength=0,
                ClubSchedule=string.Empty
            };
            return club;
        }
    }
}