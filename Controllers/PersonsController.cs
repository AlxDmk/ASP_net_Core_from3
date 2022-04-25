using System.Threading.Tasks;
using Lesson3.Controllers.Models;
using Lesson3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lesson3.Controllers
{
    [Route("api/persons")]
    [ApiController]
    
    public class PersonsController : ControllerBase
    {
        private readonly IService<PersonDto> _service;

        public PersonsController(IService<PersonDto> service)
        {
            _service = service;
        }

        // GET /persons/{id} — получение человека по идентификатору
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await _service.Get(id));
        }

        // GET /persons/search?searchTerm={term} — поиск человека по имени
        [Authorize]
        [HttpGet("search")]
        public async Task<IActionResult> GetByName([FromQuery] string searchTerm)
        {
            return Ok(await _service.GetByName(searchTerm));
        }

        // GET /persons/?skip={5}&take={10} — получение списка людей с пагинацией
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Select([FromQuery] int skip, [FromQuery] int take)
        {
            return Ok(await _service.Select(skip, take));
        }


        // POST /persons — добавление новой персоны в коллекцию
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPerson([FromBody]PersonDto person)
        {
            await _service.Create(person);
            return Ok();
        }
        
        
        // PUT /persons — обновление существующей персоны в коллекции
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdatePerson([FromBody] PersonDto person)
        {
            await _service.Update(person);
            return Ok();
        }
        
        
        // DELETE /persons/{id} — удаление персоны из коллекции
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson([FromRoute] int id)
        {
            await _service.Delete(id);
            return Ok();
        }

    }
}