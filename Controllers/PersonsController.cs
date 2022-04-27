using System.Threading.Tasks;
using Lesson3.Controllers.Models;
using Lesson3.Requests;
using Lesson3.Requests.PersonRequests;
using Lesson3.Services;
using Lesson3.Validation;
using Lesson3.Validation.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Lesson3.Controllers
{
    [Route("api/persons")]
    [ApiController]
    
    public class PersonsController : ControllerBase
    {
        private readonly IService<PersonDto> _service;
        private readonly IPersonByIdValidator _personByIdValidator;
        private readonly IPersonByNameValidator _personByNameValidator;
        private readonly IPersonValidator _personValidator;
        private readonly ISelectValidator _selectValidator;

        public PersonsController(IService<PersonDto> service,
            IPersonByIdValidator personByIdValidator,
            IPersonByNameValidator personByNameValidator,
            IPersonValidator personValidator,
            ISelectValidator selectValidator)
        {
            _service = service;
            _personByIdValidator = personByIdValidator;
            _personByNameValidator = personByNameValidator;
            _personValidator = personValidator;
            _selectValidator = selectValidator;
        }

        // GET /persons/{id} — получение человека по идентификатору
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var request = new PersonByIdRequest(){Id = id};
            
            var validation = new OperationResult<PersonByIdRequest>(_personByIdValidator.ValidateEntity(request));

            if (!validation.Succeed)
            {
                return BadRequest(validation);
            }

            return Ok(await _service.Get(id));
        }

        // GET /persons/search?searchTerm={term} — поиск человека по имени
        [HttpGet("search")]
        public async Task<IActionResult> GetByName([FromQuery] string searchTerm)
        {
            var request = new PersonByNameRequest() {Name = searchTerm};
            var validation = new OperationResult<PersonByNameRequest>(_personByNameValidator.ValidateEntity(request));
            if (!validation.Succeed)
            {
                return BadRequest(validation);
            }
            return Ok(await _service.GetByName(searchTerm));
        }

        // GET /persons/?skip={5}&take={10} — получение списка людей с пагинацией
        [HttpGet]
        public async Task<IActionResult> Select([FromQuery] int skip, [FromQuery] int take)
        {
            var request = new SelectionRequest() {Skip = skip, Take = take};

            var validation = new OperationResult<SelectionRequest>(_selectValidator.ValidateEntity(request));
            if (!validation.Succeed)
            {
                return BadRequest(validation);

            }
            return Ok(await _service.Select(skip, take));
        }


        // POST /persons — добавление новой персоны в коллекцию
        [HttpPost]
        public async Task<IActionResult> AddPerson([FromBody]PersonDto person)
        {
            var validation = new OperationResult<PersonDto>(_personValidator.ValidateEntity(person));
            if (!validation.Succeed)
            {
                return BadRequest(validation);
            }
            await _service.Create(person);
            return Ok();
        }
        
        
        // PUT /persons — обновление существующей персоны в коллекции
        [HttpPut]
        public async Task<IActionResult> UpdatePerson([FromBody] PersonDto person)
        {
            var validation = new OperationResult<PersonDto>(_personValidator.ValidateEntity(person));
            if (!validation.Succeed)
            {
                return BadRequest(validation);
            }
            await _service.Update(person);
            return Ok();
        }
        
        
        // DELETE /persons/{id} — удаление персоны из коллекции
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson([FromRoute] int id)
        {
            var request = new PersonByIdRequest(){Id = id};
            
            var validation = new OperationResult<PersonByIdRequest>(_personByIdValidator.ValidateEntity(request));

            if (!validation.Succeed)
            {
                return BadRequest(validation);
            }

            await _service.Delete(id);
            return Ok();
        }

    }
}