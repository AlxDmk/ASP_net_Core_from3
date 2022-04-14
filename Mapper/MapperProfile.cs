using AutoMapper;
using Lesson3.Controllers.Models;
using Lesson3.DAL.Entities;

namespace Lesson3.Mapper
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<PersonEntity, PersonDto>();
            CreateMap<PersonDto, PersonEntity>();
        }
    }
}