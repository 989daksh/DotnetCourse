using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotnetCourse.Services.CharacterService;
using DotnetCourse.DTOs.Characters;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using DotnetCourse.DTOs;

namespace DotnetCourse.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        // private static Character King = new Character();
        // private static List<Character> characters = new List<Character> {
        //     new Character(),
        //     new Character { Id = 1, Name = "Sumo" }
        // };
        private readonly ICharacterService _characterService;
        public CharacterController(ICharacterService characterService){
            _characterService = characterService;            
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get(){
            // int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value); 
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> DeleteCharacter(int id){
            var response = await _characterService.DeleteCharacter(id);
            if(response.Data == null){
            return NotFound(response);
          }
            return Ok(response);
        }
        

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingleCharacter(int id){
            return Ok(await _characterService.GetCharacterbyId(id));
        }
        
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<AddCharacterDto>>>> AddCharacter(AddCharacterDto NewCharacter){
            return Ok(await _characterService.AddNewCharacter(NewCharacter));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter(UpdateCharacterDto updatedCharacter){
          var response = await _characterService.UpdateCharacter(updatedCharacter); //added to show error and not OK(200)
          if(response.Data == null){
            return NotFound(response);
            
          }
            return Ok(response);
        }

        [HttpPost("Skill")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddCharacterSkill(AddCharacterSkillDto newcharacterSkill)
        {
            return Ok(await _characterService.AddCharacterSkill(newcharacterSkill));
        }
    }
}