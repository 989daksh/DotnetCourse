using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DotnetCourse.DTOs;
using DotnetCourse.DTOs.Characters;

namespace DotnetCourse.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character> {
            new Character(),
            new Character { Id = 1, Name = "Sumo" }
        };
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User
        .FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddNewCharacter(AddCharacterDto Newcharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>(); 
            Character character = _mapper.Map<Character>(Newcharacter);
            character.Users = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            // character.Id = characters.Max(c => c.Id) + 1;
            // characters.Add(character);
            // characters.Add(_mapper.Map<Character>(Newcharacter));
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.Characters.Where(c => c.Users!.Id == GetUserId()).Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> response = new ServiceResponse<List<GetCharacterDto>>();
            
            try{
            // Character character = characters.First(c => c.Id  == id);
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id  == id && c.Users!.Id == GetUserId());
            if(character is null) {
                throw new Exception("This character doesn;t exist");
            }
            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
            response.Data = await _context.Characters.Where(c => c.Users!.Id == GetUserId()).Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            } catch(Exception ex){
                response.Success = false;
                response.Message = ex.Message;
            }
            return response; 
        }

        // public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters(int userId)
        // {
        //     // throw new NotImplementedException();
        //     // return characters;
        //     // return new ServiceResponse<List<GetCharacterDto>> {
        //     //     Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList()
        //     // };
        //     // var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        //     // serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
        //     // return serviceResponse;
        //     var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        //     var dbcharacters = await _context.Characters.Where(c => c.Users!.Id == userId).ToListAsync();
        //     serviceResponse.Data = dbcharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
        //     return serviceResponse;
        // }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            // throw new NotImplementedException();
            // return characters;
            // return new ServiceResponse<List<GetCharacterDto>> {
            //     Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList()
            // };
            // var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            // serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            // return serviceResponse;
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbcharacters = await _context.Characters.Include(c => c.Weapon).Include(c => c.Skills).Where(c => c.Users!.Id == GetUserId()).ToListAsync();
            serviceResponse.Data = dbcharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterbyId(int id)
        {
            // return characters.FirstOrDefault(c => c.Id == id);
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _context.Characters.Include(c => c.Weapon).Include(c => c.Skills)
            .FirstOrDefaultAsync(c => c.Id == id && c.Users!.Id == GetUserId());
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
            
            try{
            // Character character = characters.FirstOrDefault(c => c.Id  == updatedCharacter.Id);
            var character = await _context.Characters.Include(c => c.Users).FirstOrDefaultAsync(c => c.Id  == updatedCharacter.Id);
            if(character is null || character.Users!.Id != GetUserId()) {
                throw new Exception($"Character with ID '{updatedCharacter.Id}' cannot be found");
            }

            _mapper.Map(updatedCharacter, character);
            // character.Name = updatedCharacter.Name;
            // character.Hitpoints = updatedCharacter.Hitpoints;
            // character.Strength = updatedCharacter.Strength;
            // character.Defence = updatedCharacter.Defence;
            // character.Intelligence = updatedCharacter.Intelligence;
            // character.Class = updatedCharacter.Class;
            
            await _context.SaveChangesAsync();
            response.Data = _mapper.Map<GetCharacterDto>(character);
            } catch(Exception ex){
                response.Success = false;
                response.Message = ex.Message;
            }
            return response; 
        }

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = await _context.Characters.Include(c => c.Weapon).Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterId && c.Users!.Id == GetUserId());

                if(character is null){
                    response.Success = false;
                    response.Message = "Character not found";
                    return response;
                }

                var skill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId);
                if(skill is null) {
                    response.Success = false;
                    response.Message = "Skill not found";
                    return response;
                }

                character.Skills!.Add(skill);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}