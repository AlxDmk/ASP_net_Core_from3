using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lesson3.Controllers.Models;
using Lesson3.DAL.Entities;
using Lesson3.DAL.Repository.Interfaces;

namespace Lesson3.Services
{
    public class PersonService:IService<PersonDto>
    {
        private readonly IRepository<PersonEntity> _repository;
        private readonly IMapper _mapper;

        public PersonService(IRepository<PersonEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Create(PersonDto item) =>
            await _repository.Add(_mapper.Map<PersonEntity>(item));


        public async Task Update(PersonDto item) =>
            await _repository.Update(_mapper.Map<PersonEntity>(item));


        public async Task Delete(int id) =>
            await _repository.Delete(id);
        
        public async Task<PersonDto> Get(int id) =>
            _mapper.Map<PersonDto>(await _repository.Get(id));
     

        public async Task<PersonDto> GetByName(string name)=>
            _mapper.Map<PersonDto>(await _repository.GetByName(name));
        

        public async  Task<IList<PersonDto>> Select(int skip, int take)
        {
            var requestPersons = await _repository.Select(skip, take);
            return requestPersons.Select(person => _mapper.Map<PersonDto>(person)).ToList();
        }
    }
}