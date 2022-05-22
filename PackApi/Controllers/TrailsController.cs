using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PackApi.Models;
using PackApi.repository.Interfaces;
using System.Collections.Generic;

namespace PackApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class TrailsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITrailsRepository _TrailRepository;

        public TrailsController(IMapper mapper, ITrailsRepository trailsRepository)
        {
            _mapper = mapper;
            _TrailRepository = trailsRepository;
        }

        [HttpGet(nameof(GetNationalParks))]
        [ProducesResponseType(200, Type = typeof(List<Trails>))]
        [ProducesResponseType(400)]
        public IActionResult GetNationalParks()
        {
            var objList = _TrailRepository.GetNationalParks();

            var objDTO = new List<NationalParkDtos>();

            foreach (var obj in objList)
            {
                objDTO.Add(_mapper.Map<NationalParkDtos>(obj));
            }

            return Ok(objDTO);
        }
        [HttpGet(nameof(GetNationalParkById))]
        [ProducesResponseType(200, Type = typeof(Trails))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetNationalParkById(int id)
        {
            var obj = _TrailRepository.GetNationalPack(id);
            if (obj == null) return NotFound();

            var objDTO = _mapper.Map<NationalParkDtos>(obj);
            return Ok(objDTO);
        }
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Trails))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateNationalPark([FromBody] NationalParkDtos nationalParkDtos)
        {
            if (nationalParkDtos == null) return BadRequest(ModelState);

            if (_TrailRepository.NationalParkExist(nationalParkDtos.Name))
            {
                ModelState.AddModelError("", "National Park Exists!");
                return StatusCode(404, ModelState);
            }

            var nationalParckObj = _mapper.Map<Trails>(nationalParkDtos);
            if (!_TrailRepository.CreateNationalPark(nationalParckObj))
            {
                ModelState.AddModelError("", $"Something w ent Wrong when saving the record {nationalParckObj.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetNationalParkById", new { nationaParkId = nationalParckObj.Id }, nationalParckObj);
        }

        [HttpPatch]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateNationalPark(int NationalParkId, [FromBody] NationalParkDtos nationalParkDtos)
        {
            if (nationalParkDtos == null || NationalParkId != nationalParkDtos.Id)
            {
                return BadRequest(ModelState);
            }
            var nationalParckObj = _mapper.Map<NationalPack>(nationalParkDtos);
            if (!_TrailRepository.UpdateNationalPark(nationalParckObj))
            {
                ModelState.AddModelError("", $"Something went Wrong when updating the record {nationalParckObj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete(nameof(DeleteNationalPark))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteNationalPark(int nationalParkId)
        {
            if (!_TrailRepository.NationalParkExist(nationalParkId))
            {
                return NotFound();
            }

            var nationalParckObj = _TrailRepository.GetNationalPack(nationalParkId);
            if (!_TrailRepository.DeleteNationalPark(nationalParckObj))
            {
                ModelState.AddModelError("", $"Something went Wrong when deleting the record {nationalParckObj.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


    }
}
