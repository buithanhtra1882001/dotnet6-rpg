using AutoMapper;
using dotnet_rpg.Dtos.Character;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
         private static List<Character> characters = new List<Character> {
             new Character(),
             new Character{ Id = 1, Name = "Sam" }
         };
        private readonly IMapper mapper;

        public CharacterService(IMapper mapper)
         {
            this.mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character character = this.mapper.Map<Character>(newCharacter);
            character.Id = characters.Max(c => c.Id) + 1;
            characters.Add(character);
            serviceResponse.Data = characters.Select(c => this.mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
             return new ServiceResponse<List<GetCharacterDto>>
             { 
                Data = characters.Select(c => this.mapper.Map<GetCharacterDto>(c)).ToList() 
             };
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var character = characters.FirstOrDefault(c => c.Id == id);
            serviceResponse.Data = this.mapper.Map<GetCharacterDto>(character);
            return serviceResponse;
        }
    }
}