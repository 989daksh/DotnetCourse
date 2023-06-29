using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetCourse.DTOs;
using DotnetCourse.DTOs.Characters;

namespace DotnetCourse.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();
        Task<ServiceResponse<GetCharacterDto>> GetCharacterbyId(int id);
        Task<ServiceResponse<List<GetCharacterDto>>> AddNewCharacter(AddCharacterDto Newcharacter);
        Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter);
        Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);
        Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill);

    }
}