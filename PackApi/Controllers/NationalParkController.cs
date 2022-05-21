using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PackApi.Models;
using PackApi.Models.DTOs;
using PackApi.repository.Interfaces;
using System.Collections.Generic;

namespace PackApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalParksController : Controller
    {
        private readonly IMapper _mapper;
        private readonly INationalParkRepository _nationalParkRepository;

        public NationalParksController(IMapper mapper, INationalParkRepository nationalParkRepository)
        {
            _mapper = mapper;
            _nationalParkRepository = nationalParkRepository;
        }


        [HttpGet(nameof(GetNationalParks))]
        public IActionResult GetNationalParks()
        {
            var objList = _nationalParkRepository.GetNationalParks();

            var objDTO = new List<NationalParkDtos>();

            foreach(var obj in objList)
            {
                objDTO.Add(_mapper.Map<NationalParkDtos>(obj));
            }

            return Ok(objDTO);
        }

        [HttpGet(nameof(GetNationalParkById))]
        public IActionResult GetNationalParkById(int id)
        {
            var obj = _nationalParkRepository.GetNationalPack(id);
            if(obj == null) return NotFound();

            var objDTO = _mapper.Map<NationalParkDtos>(obj);
            return Ok(objDTO);
        }

        [HttpPost]
        public IActionResult CreateNationalPark([FromBody] NationalParkDtos nationalParkDtos)
        {
            if (nationalParkDtos == null) return BadRequest(ModelState);

            if(_nationalParkRepository.NationalParkExist(nationalParkDtos.Name))
            {
                ModelState.AddModelError("", "National Park Exists!");
                return StatusCode(404, ModelState);
            }

            var NationalParckObj = _mapper.Map<NationalPack>(nationalParkDtos);
            if(!_nationalParkRepository.CreateNationalPark(NationalParckObj))
            {
                ModelState.AddModelError("", $"Something went Wrong when saving the record {NationalParckObj.Name}");
                return StatusCode(500, ModelState);  
            }
            return Ok("Sucessfully Created");
        }

        [HttpPatch("{nationalParkId:int}", Name = "UpdateNationalPark")]
        public IActionResult UpdateNationalPark(int NationalParkId, [FromBody] NationalParkDtos nationalParkDtos)
        {
            if (nationalParkDtos == null || NationalParkId != nationalParkDtos.Id)
            {
                return BadRequest();
            }

            var nationalParckObj = _mapper.Map<NationalPack>(nationalParkDtos);
            if (!_nationalParkRepository.UpdateNationalPark(nationalParckObj))
            {
                ModelState.AddModelError("", $"Something went Wrong when saving the record {nationalParckObj.Name}");
                return StatusCode(500, ModelState);

            }
            return NoContent();

        }
    }
}
