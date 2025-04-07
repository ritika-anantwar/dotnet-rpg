using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.DTOs.Fight;
using dotnet_rpg.DTOs.Skill;
using dotnet_rpg.DTOs.Weapon;

namespace dotnet_rpg
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDTO>();
            CreateMap<AddCharacterDTO, Character>();
            CreateMap<UpdateCharacterDTO, Character>();
            CreateMap<Weapon, GetWeaponDTO>();
            CreateMap<Skill, GetSkillDTO>();
            CreateMap<Character, HighscoreDTO>();
        }
    }
}