global using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>{
            new Character(),
            new Character {Id = 1, Name = "Sam"}
        };
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter)
        {
            var ServiceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            var character = _mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(c => c.Id) + 1;
            characters.Add(character);
            ServiceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            return ServiceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int id)
        {
            var ServiceResponse = new ServiceResponse<List<GetCharacterDTO>>();

            try{
                var character = characters.First(c => c.Id == id);

                if(character is null){
                    throw new Exception($"Character with Id '{id}' not found.");
                }

                characters.Remove(character);

                ServiceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            }

            catch(Exception ex){
                ServiceResponse.Success = false;
                ServiceResponse.Message = ex.Message;
            }

            return ServiceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters()
        {
            var ServiceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            
            ServiceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            return ServiceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> GetCharacterById(int id)
        {
            var ServiceResponse = new ServiceResponse<GetCharacterDTO>();
            var character = characters.FirstOrDefault(c => c.Id == id);
            ServiceResponse.Data = _mapper.Map<GetCharacterDTO>(character);
            return ServiceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO updatedCharacter)
        {
            var ServiceResponse = new ServiceResponse<GetCharacterDTO>();

            try{
                var character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);

                if(character is null){
                    throw new Exception($"Character with Id '{updatedCharacter.Id}' not found.");
                }

                _mapper.Map(updatedCharacter, character);

                character.Name = updatedCharacter.Name;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Strength = updatedCharacter.Strength;
                character.Defense = updatedCharacter.Defense;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Class = updatedCharacter.Class;

                ServiceResponse.Data = _mapper.Map<GetCharacterDTO>(character);
            }

            catch(Exception ex){
                ServiceResponse.Success = false;
                ServiceResponse.Message = ex.Message;
            }

            return ServiceResponse;
        }

        Task<ServiceResponse<GetCharacterDTO>> ICharacterService.AddCharacter(AddCharacterDTO newCharacter)
        {
            throw new NotImplementedException();
        }
    }
}