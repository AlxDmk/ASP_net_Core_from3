using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lesson3.DAL.Entities;
using Lesson3.DAL.Repository.DataBase;
using Lesson3.DAL.Repository.Interfaces;

namespace Lesson3.DAL.Repository
{
    public class PersonsRepository: IRepository<PersonEntity>
    {

        private readonly List<PersonEntity> _dataBase ;
        public PersonsRepository()
        {
            _dataBase = PersonsDb.PersonsDataBase;
        }

        public Task<PersonEntity> Get(int id)
        {
            var result = _dataBase.Find((x) => x.Id == id);
            if (result == null || result.IsDelete == true)
            {
                throw new ArgumentException("Такого человека не найдено");
            }
            return Task.FromResult(result);
        }

        public Task Add(PersonEntity item)
        {
            _dataBase.Add(item);
            return Task.CompletedTask;
        }
        

        public Task Update(PersonEntity item)
        {
            var result = _dataBase.Find((x) => x.Id == item.Id);
          
            foreach (var property in  typeof(PersonEntity).GetProperties())
            {
                property.SetValue(result, property.GetValue(item));
                
            }
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            var result = _dataBase.Find((x) => x.Id == id);
            result?.Delete();
            return Task.CompletedTask;
        }

        public Task<PersonEntity> GetByName(string name)
        {
            var result = _dataBase.Find((x) => x.FirstName == name);
            return Task.FromResult(result);
        }

        public Task<IEnumerable<PersonEntity>> Select(int skip, int take)
        {
            var result =
                _dataBase.Skip(skip).Take(take).Select(x => x);
            return Task.FromResult(result);

        }
    }

    
}